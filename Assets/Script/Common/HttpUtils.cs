using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Networking;

public class HttpUtils: MonoBehaviour {
    public string baseUrl = "";
    public Dictionary<string, string> headers = new Dictionary<string, string>();
    // Start is called before the first frame update
    public static HttpUtils instance;

    void Awake()
    {
        headers.Add("Content-Type", "application/json;charset=utf-8");
        instance = this;

    }
   
    public void Get(string method ,Action<string> action)
    {
        StartCoroutine(_Get(SetUrl(method), action));
    }
    public void Delete(string method, Action<bool> action)
    {
        StartCoroutine(_Delete(SetUrl(method), action));
    }
    public void Put(string method, Dictionary<string, string> formFields, Action<string> action)
    {
        StartCoroutine(_Put(SetUrl(method), formFields,action));
    }
    public void Post(string method, Dictionary<string, string> formFields, Action<string> action)
    {
        StartCoroutine(_Post(SetUrl(method), formFields, action));
    }
  



    private string SetUrl(string method)
    {
        return baseUrl + method;
    }
    private void SetHeaders(UnityWebRequest unityWebRequest)
    {
        foreach (var item in headers)
        {
            unityWebRequest.SetRequestHeader(item.Key, item.Value);
        }
    }
    //"http://www.my-server.com"
    IEnumerator _Get<T>(string url, Action<T> callback)
    {
        UnityWebRequest www = UnityWebRequest.Get(url);
        //www.SetRequestHeader("Content-Type", "application/json;charset=utf-8");
        SetHeaders(www);
        yield return www.SendWebRequest();

        HandleResult(callback, www);
    }
    IEnumerator _Get(string method, Action<string> callback)
    {
   
        UnityWebRequest www = UnityWebRequest.Get(method);
        //www.SetRequestHeader("Content-Type", "application/json;charset=utf-8");
        SetHeaders(www);
        yield return www.SendWebRequest();

        HandleResult(callback, www);
    }
    //"http://www.my-server.com"
    IEnumerator _Delete(string url, Action<bool> callback)
    {
        UnityWebRequest www = UnityWebRequest.Delete(url);
        SetHeaders(www);
        yield return www.SendWebRequest();
        HandleResult(callback, www);
    }

    //"http://www.my-server.com/image.png"
    IEnumerator _GetTexture(string url,Action<Texture> callback)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        SetHeaders(www);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Texture myTexture = DownloadHandlerTexture.GetContent(www);
            callback(myTexture);
           
        }
    }
    //"http://www.my-server.com/myform"
     IEnumerator UploadFile<T>(string url, byte[] bytes, Action<T> callback)
    {
        UnityWebRequest www = new UnityWebRequest(url, "POST");
        www.uploadHandler = (UploadHandler)new UploadHandlerRaw(bytes);
        www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/octet-stream");
        string token;
        headers.TryGetValue("Authorization", out token);
        www.SetRequestHeader("Authorization", token); //if your server need token
        yield return www.SendWebRequest();
        if (www.isDone)
        {
            HandleResult<T>(callback, www);
        }
    }

    

    IEnumerator _Post<T>(string url, Dictionary<string, string> formFields, Action<T> callback)
    {
        /*byte[] myData = System.Text.Encoding.UTF8.GetBytes("This is some test data");*/
        string postData = JsonConvert.SerializeObject(formFields);
        UnityWebRequest www = new UnityWebRequest(url, UnityWebRequest.kHttpVerbPOST);
        //直接使用post会encode params ，导致后台报错，所以采用上方这种方式解决
        //UnityWebRequest www = UnityWebRequest.Post(url, postData);
        www.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(postData));
        www.downloadHandler = new DownloadHandlerBuffer();
        SetHeaders(www);
        yield return www.SendWebRequest();
        HandleResult(callback, www);
   
    }
    void HandleResult<T>(Action<T> callback, UnityWebRequest www)
    {
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
            PanelManger.Open<SystemTipPanel>("网络异常！");
        }
        else
        {
            string result = www.downloadHandler.text;
            HttpResponse<T> response = JsonConvert.DeserializeObject<HttpResponse<T>>(result);
            if ("200".Equals(response.Code))
            {
                callback(response.Data);
            }
            else
            {
                PanelManger.Open<SystemTipPanel>(response.Mesg);
            }
        }
    }
    
    //"http://www.my-server.com/upload"
    IEnumerator _Put(string url, Dictionary<string, string> formFields, Action<string> callback)
    {
        /*byte[] myData = System.Text.Encoding.UTF8.GetBytes("This is some test data");*/
        byte[] myData = UnityWebRequest.SerializeSimpleForm(formFields);
        UnityWebRequest www = UnityWebRequest.Put(url, myData);
        SetHeaders(www);
        yield return www.SendWebRequest();

        HandleResult(callback, www);
    }

    public class HttpResponse<T>
    {
        string code;
        string mesg;
        string time;
        T data;

        public string Code { get => code; set => code = value; }
        public string Mesg { get => mesg; set => mesg = value; }
        public string Time { get => time; set => time = value; }
        public T Data { get => data; set => data = value; }
    }
}
