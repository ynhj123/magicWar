using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buttons : BasePanel
{
   
    Button loginBtn;
    Button RegistryBtn;
    public override void OnInit()
    {
        skinPath = "Panel/Buttons";
        layer = PanelManger.Layer.Panel;
    }

    public override void OnShow(params object[] objects)
    {
      
        loginBtn = skin.transform.Find("Login").GetComponent<Button>();
        loginBtn.onClick.AddListener(OnLogin);
        RegistryBtn = skin.transform.Find("Registory").GetComponent<Button>();
        RegistryBtn.onClick.AddListener(OnRegistory);
    }

    private void OnRegistory()
    {
        Debug.Log("openre");
        PanelManger.Open<RegistryPanel>();
        Close();
    }

    private void OnLogin()
    {
        Debug.Log("openlogin");
        PanelManger.Open<LoginPanel>();
        Close();
    }

    public override void OnClose()
    {
      
    }
}
