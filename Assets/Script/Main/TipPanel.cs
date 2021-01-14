using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipPanel : MonoBehaviour
{
    public static TipPanel instance;
    
    Image img;
    CanvasGroup canvasGroup;
    Text title;
    Text des;
    EquipManagerApi managerApi;

    private void Awake()
    {
        instance = this;
        img = GetComponent<Image>();
        canvasGroup = GetComponent<CanvasGroup>();
        title = transform.Find("Title").GetComponent<Text>();
        des = transform.Find("Des").GetComponent<Text>();
        Hide();
        managerApi = (EquipManagerApi)ApiFactory.GetFactory(ApiType.EquipApi);
    }

    public void Show(int id)
    {
        EquipDetailModel equipDetailModel = managerApi.GetDetail(id);
        if(equipDetailModel == null)
        {
            return;
        }
        title.text = equipDetailModel.Name;
        des.text = equipDetailModel.Des;
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    public void Hide()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
}
