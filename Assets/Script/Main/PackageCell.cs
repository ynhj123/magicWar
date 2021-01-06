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
    

    // Start is called before the first frame update
    void Start()
    {
        button = transform.Find("Button").GetComponent<Button>();
        button.onClick.AddListener(Selected);
       
    }

    private void Selected()
    {
        if(equip == null)
        {
            return;
        }
        EquipPanel panel =  PanelManger.Get<EquipPanel>();
        button.GetComponent<Image>().color = Color.green;
        panel.selectedEquip(this);
    }

    public void FlushView(PageEquipModel equip)
    {
        icon = transform.Find("Button/Icon").GetComponent<Image>();
        num = transform.Find("Button/Num").GetComponent<Text>();
        if (equip == null)
        {
            this.equip = null;
            icon.color = new Color(1, 1, 1, 0);
            num.color  = new Color(1, 1, 1, 0);
        }
        else
        {
            
            this.equip = equip;
            icon.color = new Color(1, 1, 1, 1);
            string[] strings = equip.UiPath.Split('_');
            icon.overrideSprite = ResManger.LoadSprites(strings[0])[int.Parse(strings[1])];
            num.text = equip.Num.ToString();
            num.color = new Color(1, 1, 1, 1);
        }
       
    }

    public void CancelSelected()
    {
        button.GetComponent<Image>().color = Color.white;
    }
}
