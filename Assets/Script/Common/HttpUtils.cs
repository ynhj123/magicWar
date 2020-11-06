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
        instance = this;
    }
    public  void Login(string username,string password)
    {
        //StartCoroutine()
        //DownloadHandlerTexture.GetContent();
    }
    public void Get(string method ,Action<string> action)
    {
        StartCoroutine(_Get(method, action));
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
    IEnumerator _Get<T>(string method, Action<T> callback)
    {
        UnityWebRequest www = UnityWebRequest.Get(SetUrl(method));
        //www.SetRequestHeader("Content-Type", "application/json;charset=utf-8");
        SetHeaders(www);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            // 将结果显示为文本
            //Debug.Log(www.downloadHandler.text);

            // 或者以二进制数据格式检索结果
            //byte[] results = www.downloadHandler.data;
            string result = www.downloadHandler.text;
            T t = JsonConvert.DeserializeObject<T>(result);
            callback(t);
        }
    }
    IEnumerator _Get(string method, Action<string> callback)
    {
   
        UnityWebRequest www = UnityWebRequest.Get(SetUrl(method));
        //www.SetRequestHeader("Content-Type", "application/json;charset=utf-8");
        SetHeaders(www);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            // 将结果显示为文本
            //Debug.Log(www.downloadHandler.text);

            // 或者以二进制数据格式检索结果
            //byte[] results = www.downloadHandler.data;
            string result = www.downloadHandler.text;
            callback(result);
        }
    }
    //"http://www.my-server.com"
    IEnumerator _Delete(string url, Action<bool> callback)
    {
        UnityWebRequest www = UnityWebRequest.Delete(url);
        SetHeaders(www);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
            callback(false);
        }
        else
        {
            callback(true);

            // 将结果显示为文本
            Debug.Log(www.downloadHandler.text);
      
        }
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
    IEnumerator _Post(string url , List<IMultipartFormSection> formData)
    {
      /*  List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("field1=foo&field2=bar"));
        formData.Add(new MultipartFormFileSection("my file data", "myfile.txt"));*/
       
        UnityWebRequest www = UnityWebRequest.Post(url, formData);
        SetHeaders(www);
        yield return www.SendWebRequest();
        
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
        }
    }
    //"http://www.my-server.com/upload"
    IEnumerator _Put(string url, Dictionary<string, string> formFields)
    {
        /*byte[] myData = System.Text.Encoding.UTF8.GetBytes("This is some test data");*/
        byte[] myData = UnityWebRequest.SerializeSimpleForm(formFields);
        UnityWebRequest www = UnityWebRequest.Put(url, myData);
        SetHeaders(www);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Upload complete!");
        }
    }


}
