using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegistryPanel : BasePanel
{
    InputField username;
    InputField password;
    InputField rePassword;
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
        registryBtn = skin.transform.Find("Btn/Button").GetComponent<Button>();
        registryBtn.onClick.AddListener(OnRegistry);
    }

    private void OnRegistry()
    {
        Debug.Log("registry");
        PanelManger.Open<SystemTipPanel>(" Close();");
        PanelManger.Open<Buttons>();
        Close();
    }

    public override void OnClose()
    {
    }
}
