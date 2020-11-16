using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SystemTipPanel : BasePanel
{
    Text text;
    Button closeBtn;
    public override void OnInit()
    {
        skinPath = "Panel/SystemTipPanel";
        layer = PanelManger.Layer.Tip;
    }

    public override void OnShow(params object[] objects)
    {
        text = skin.transform.Find("ScrollText/Viewport/Content").GetComponent<Text>();
        closeBtn = skin.transform.Find("Button").GetComponent<Button>();
        closeBtn.onClick.AddListener(OnClosePanel);
        Debug.Log(objects[0]);
        if (objects.Length == 1)
        {
           
            text.text = (string)objects[0];
        }
    }

    private void OnClosePanel()
    {
        Debug.Log("closeTip");
        Close();
    }

    public override void OnClose()
    {
    }
}
