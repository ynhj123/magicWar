using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PackageCell : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public PageEquipModel equip;
    Button button;
    Image icon;
    Text num;
    Image buttonImg;
    TipPanel tipPanel;
    RectTransform rectTransform;

    private void Awake()
    {
        icon = transform.Find("Button/Icon").GetComponent<Image>();
        num = transform.Find("Button/Num").GetComponent<Text>();
        button = transform.Find("Button").GetComponent<Button>();
        buttonImg = button.GetComponent<Image>();
        tipPanel = TipPanel.instance;
        rectTransform = GetComponent<RectTransform>();
        

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
        tmpColor = Color.green;
        PanelManger.Get<EquipPanel>().selectedEquip(this);
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
        tmpColor = Color.white;
    }

    Color tmpColor;

    public void OnPointerEnter(PointerEventData eventData)
    {
        tmpColor = buttonImg.color; 
        buttonImg.color = Color.gray;
        if (this.equip != null)
        {
            tipPanel.transform.position = new Vector3(transform.position.x - rectTransform.rect.width / 2 - 15, transform.position.y - rectTransform.rect.height / 2 + 15, transform.position.z); ;
            tipPanel.Show(equip.Id);
           
        }
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonImg.color = tmpColor;
        tipPanel.Hide();
    }
}
