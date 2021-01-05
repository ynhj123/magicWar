using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PackageCell : MonoBehaviour
{
    PageEquipModel equip;
    Button button;
    Image icon;
    Text num;

    // Start is called before the first frame update
    void Start()
    {
        button = transform.Find("Button").GetComponent<Button>();
       
    }

    public void FlushView(PageEquipModel equip)
    {
        icon = transform.Find("Button/Icon").GetComponent<Image>();
        num = transform.Find("Button/Num").GetComponent<Text>();
        if (equip == null)
        {
            icon.color = new Color(1, 1, 1, 0);
            num.color  = new Color(1, 1, 1, 0);
        }
        else
        {
            
            this.equip = equip;
            icon.color = new Color(1, 1, 1, 1);
            Debug.Log(equip.UiPath);
            string[] strings = equip.UiPath.Split('_');
            icon.overrideSprite = ResManger.LoadSprites(strings[0])[int.Parse(strings[1])];
            num.text = equip.Num.ToString();
            num.color = new Color(1, 1, 1, 1);
        }
       
    }


}
