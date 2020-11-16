using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegistryPanel : BasePanel
{
    InputField username;
    InputField password;
    InputField rePassword;
    InputField nickName;
   
    Button registryBtn;

    public override void OnInit()
    {
        skinPath = "Panel/RegistoryPanel";
        layer = PanelManger.Layer.Panel;
    }

    public override void OnShow(params object[] objects)
    {
        username = skin.transform.Find("Username/InputField").GetComponent<InputField>();
        password = skin.transform.Find("Password/InputField").GetComponent<InputField>();
        rePassword = skin.transform.Find("RePassword/InputField").GetComponent<InputField>();
        nickName = skin.transform.Find("NickName/InputField").GetComponent<InputField>();

        registryBtn = skin.transform.Find("Btn/Button").GetComponent<Button>();
        registryBtn.onClick.AddListener(OnRegistry);
    }

    private void OnRegistry()
    {
        Debug.Log("registry");
        if(password != rePassword)
        {
            PanelManger.Open<SystemTipPanel>("2次密码不一致");
            return;
        }

        Dictionary<string, string> postParam = new Dictionary<string, string>();
        postParam.Add("username", username.text);
        postParam.Add("password", password.text);
        postParam.Add("nickName", nickName.text);
        HttpUtils.instance.Post("/auth/register",postParam,(result)=>
        {
            PanelManger.Open<SystemTipPanel>("注册成功！");
            PanelManger.Open<Buttons>();
            Close();

        });

        
    }

    public override void OnClose()
    {
    }
}
