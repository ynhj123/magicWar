using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    float QCd = 0;
    float WCd = 0;
    float ECd = 0;
    float RCd = 0;
    float ACd = 0;
    float SCd = 0;
    float DCd = 0;
    float FCd = 0;

    float QMCd = 4;
    float WMCd = 10;
    float EMCd = 18;
    float RMCd = 12;
    float AMCd = 20;
    float SMCd = 8;
    float DMCd = 5;
    float FMCd = 16;

    GameObject SkillController;
    public Player player;
    // Start is called before the first frame update
    void Start()
    {
        player.GetComponent<Player>();
        SkillController = GameObject.Find("/Root/Canvas/Controller/SkillController");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Time.timeScale = 0;

        }
        if (Input.GetKeyDown(KeyCode.S))
        {

            Time.timeScale = 1;
        }
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
            if (Input.GetKeyDown(KeyCode.R) && RCd <= 0)
            {
                player.FireRSkill();
                RCd = RMCd;

            }
            if (Input.GetKeyDown(KeyCode.A) && ACd <= 0)
            {
                player.FireASkill();
                ACd = AMCd;

            }
            if (Input.GetKeyDown(KeyCode.S) && SCd <= 0)
            {
                player.FireSSkill();
                SCd = SMCd;

            }
            if (Input.GetKeyDown(KeyCode.D) && DCd <= 0)
            {
                player.FireDSkill();
                DCd = DMCd;

            }
            if (Input.GetKeyDown(KeyCode.F) && FCd <= 0)
            {
                player.FireFSkill();
                FCd = FMCd;

            }
        }



    }
    private void LateUpdate()
    {
        if (QCd > 0)
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
        if (RCd > 0)
        {
            RCd -= Time.deltaTime;
        }
        else
        {
            RCd = 0;
        }
        if (ACd > 0)
        {
            ACd -= Time.deltaTime;
        }
        else
        {
            ACd = 0;
        }
        if (SCd > 0)
        {
            SCd -= Time.deltaTime;
        }
        else
        {
            SCd = 0;
        }
        if (DCd > 0)
        {
            DCd -= Time.deltaTime;
        }
        else
        {
            DCd = 0;
        }
        if (FCd > 0)
        {
            FCd -= Time.deltaTime;
        }
        else
        {
            FCd = 0;
        }
        UpdateSkillUi();
    }

    private void UpdateSkillUi()
    {
        Transform qCode = SkillController.transform.Find("QCode/Mask");
        qCode.GetComponent<Image>().fillAmount = QCd / QMCd;
        HandleCDView(qCode,QCd);

        Transform wCode = SkillController.transform.Find("WCode/Mask");
        wCode.GetComponent<Image>().fillAmount = WCd / WMCd;
        HandleCDView(wCode, WCd);

        Transform eCode = SkillController.transform.Find("ECode/Mask");
        eCode.GetComponent<Image>().fillAmount = ECd / EMCd;
        HandleCDView(eCode, ECd);

        Transform rCode = SkillController.transform.Find("RCode/Mask");
        rCode.GetComponent<Image>().fillAmount = RCd / RMCd;
        HandleCDView(rCode, RCd);

        Transform aCode = SkillController.transform.Find("ACode/Mask");
        aCode.GetComponent<Image>().fillAmount = ACd / AMCd;
        HandleCDView(aCode, ACd);

        Transform sCode = SkillController.transform.Find("SCode/Mask");
        sCode.GetComponent<Image>().fillAmount = SCd / SMCd;
        HandleCDView(sCode, SCd);

        Transform dCode = SkillController.transform.Find("DCode/Mask");
        dCode.GetComponent<Image>().fillAmount = DCd / DMCd;
        HandleCDView(dCode, DCd);

        Transform fCode = SkillController.transform.Find("FCode/Mask");
        fCode.GetComponent<Image>().fillAmount = FCd / FMCd;
        HandleCDView(fCode, FCd);
    }

    private void HandleCDView(Transform code, float  cd)
    {
        int cdVal = Convert.ToInt32(cd);
        if (cdVal == 0)
        {
            code.Find("Text").GetComponent<Text>().text = "";
        }
        else
        {
            code.Find("Text").GetComponent<Text>().text = cdVal.ToString() + "s";

        }
    }
}
