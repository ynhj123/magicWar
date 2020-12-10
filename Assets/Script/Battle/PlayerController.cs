using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    float QCd = 0;
    float WCd = 0;
    float ECd = 0;

    float QMCd = 4;
    float WMCd = 10;
    float EMCd = 18;

    GameObject SkillController;
    public Player player;
    // Start is called before the first frame update
    void Start()
    {
        player.GetComponent<Player>();
        SkillController  = GameObject.Find("/Root/Canvas/Controller/SkillController");
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            if (Input.GetMouseButtonDown(1))
            {
                player.UpdateControl();

            }
            if (Input.GetKeyDown(KeyCode.Q) && QCd <= 0)
            {
                player.FireQSkill();
                QCd = QMCd;
                
            }
            if (Input.GetKeyDown(KeyCode.W) && WCd <= 0)
            {
                player.FireWSkill();
                WCd = WMCd;


            }
            if (Input.GetKeyDown(KeyCode.E) && ECd <= 0)
            {
                player.FireESkill();

                ECd = EMCd;
            }
        }



    }
    private void LateUpdate()
    {
        if(QCd > 0)
        {
            QCd -= Time.deltaTime;
        }
        else
        {
            QCd = 0;
        }
        if (WCd > 0)
        {
            WCd -= Time.deltaTime;
        }
        else
        {
            WCd = 0;
        }
        if (ECd > 0)
        {
            ECd -= Time.deltaTime;
        }
        else
        {
            ECd = 0;
        }
        UpdateSkillUi();
    }

    private void UpdateSkillUi()
    {
        Transform qCode = SkillController.transform.Find("QCode/Mask");
        qCode.GetComponent<Image>().fillAmount = QCd / QMCd;
        int q = Convert.ToInt32(QCd);
        if (q == 0)
        {
            qCode.Find("Text").GetComponent<Text>().text = "";
        }
        else
        {
            qCode.Find("Text").GetComponent<Text>().text = q.ToString() + "s";

        }
        Transform wCode = SkillController.transform.Find("WCode/Mask");
        wCode.GetComponent<Image>().fillAmount = WCd / WMCd;
        int w = Convert.ToInt32(WCd);
        if (w == 0)
        {
            wCode.Find("Text").GetComponent<Text>().text = "";
        }
        else
        {
            wCode.Find("Text").GetComponent<Text>().text = w.ToString() + "s";

        }
        Transform eCode = SkillController.transform.Find("ECode/Mask");
        eCode.GetComponent<Image>().fillAmount = ECd / EMCd;
        int e = Convert.ToInt32(ECd);
        if (e == 0)
        {
            eCode.Find("Text").GetComponent<Text>().text = "";
        }
        else
        {
            eCode.Find("Text").GetComponent<Text>().text = e.ToString() + "s";

        }
    }
}
