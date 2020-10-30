using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginPanel:BasePanel
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
        /*  PanelManger.Open<Buttons>();
          PanelManger.Close<LoginPanel>();*/
        SceneManager.LoadScene("BattleScene");
    }

    public override void OnClose()
    {
    }
}
