using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PackageCell : MonoBehaviour
{
    public PageEquipModel equip;
    Button button;
    Image icon;
    Text num;
    Image buttonImg;
    EquipPanel panel;

    private void Awake()
    {
        icon = transform.Find("Button/Icon").GetComponent<Image>();
        num = transform.Find("Button/Num").GetComponent<Text>();
        button = transform.Find("Button").GetComponent<Button>();
        buttonImg = button.GetComponent<Image>();
        panel = PanelManger.Get<EquipPanel>();

    }
    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(Selected);  
    }

    private void Selected()
    {
        if(equip == null)
        {
            return;
        }       
        buttonImg.color = Color.green;
        panel.selectedEquip(this);
    }

    public void FlushView(PageEquipModel equip)
    {
        
        if (equip == null)
        {
            Color hide = Color.clear;
            this.equip = null;
            icon.color = hide;
            num.color  = hide;
        }
        else
        {
            Color show = Color.white;
            this.equip = equip;
            icon.color = show;
            string[] strings = equip.UiPath.Split('_');
            icon.overrideSprite = ResManger.LoadSprites(strings[0])[int.Parse(strings[1])];
            num.text = equip.Num.ToString();
            num.color = show;
        }
       
    }

    public void CancelSelected()
    {
        buttonImg.color = Color.white;
    }
}
