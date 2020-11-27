using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginPanel : BasePanel
{
    InputField username;
    InputField password;
    Button loginBtn;
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
        loginBtn.onClick.AddListener(OnLogin);
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
                SceneManager.LoadScene("MainScene");

            });
        });

    }

    public override void OnClose()
    {
    }
}
