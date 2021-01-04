using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginPanel : BasePanel
{
    InputField username;
    InputField password;
    Button loginBtn;
    Button quitBtn;
    public override void OnInit()
    {
        skinPath = "Panel/LoginPanel";
        layer = PanelManger.Layer.Panel;
    }

    public override void OnShow(params object[] objects)
    {
        username = skin.transform.Find("Username/InputField").GetComponent<InputField>();
        password = skin.transform.Find("Password/InputField").GetComponent<InputField>();
        loginBtn = skin.transform.Find("Btn/Button").GetComponent<Button>();
        quitBtn = skin.transform.Find("Quit").GetComponent<Button>();
        loginBtn.onClick.AddListener(OnLogin);
        quitBtn.onClick.AddListener(OnQuit);
    }

    private void OnQuit()
    {
        PanelManger.Open<Buttons>();
        Close();
    }

    private void OnLogin()
    {
        Debug.Log("login");

        Dictionary<string, string> postParam = new Dictionary<string, string>();
        postParam.Add("username", username.text);
        postParam.Add("password", password.text);
        HttpUtils.instance.Post("/auth/login", postParam, (result) =>
        {

            HttpUtils.instance.headers.Add("Authorization", result);
            HttpUtils.instance.Get<UserInfo>("/user/info", (response) =>
            {

                MainController.user = response;
                MainController.user.Token = result;
                NetManager.Connect("39.105.64.77", 8222);

            });
        });

    }

    public override void OnClose()
    {
        loginBtn.onClick.RemoveAllListeners();
        quitBtn.onClick.RemoveAllListeners();
    }
}
