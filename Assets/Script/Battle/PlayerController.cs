﻿using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Text qCodeTime;
    Text wCodeTime;
    Text eCodeTime;
    Text rCodeTime;
    Text aCodeTime;
    Text sCodeTime;
    Text dCodeTime;
    Text fCodeTime;

    Image qCodeImg;
    Image wCodeImg;
    Image eCodeImg;
    Image rCodeImg;
    Image aCodeImg;
    Image sCodeImg;
    Image dCodeImg;
    Image fCodeImg;


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

    float maxQRange = 30;
    float maxWRange = 20;
    float maxERange = 10;
    float maxRRange = 30;
    float maxARange = 10;
    float maxSRange = 5;
    float maxDRange = 20;
    float maxFRange = 20;

    bool isShowSkillRange = false;

    GameObject SkillController;
    GameObject SkillRange;
    GameObject SkillTip;
    GameObject SkillTipA;
    public Player player;

    Texture2D mousePoint;
    int skillId = 0;
    // Start is called before the first frame update
    void Start()
    {
        SkillController = GameObject.Find("/Root/Canvas/Controller/SkillController");
        SkillRange = player.transform.Find("SkillRange").gameObject;
        SkillTip = player.transform.Find("SkillTip").gameObject;
        SkillTipA = player.transform.Find("SkillTipAParent").gameObject;
        isShowSkillRange = false;
        mousePoint = ResManger.LoadTexture2D("Ui/mousePoint");
        Transform qCode = SkillController.transform.Find("QCode/Mask");
        qCodeTime = qCode.Find("Text").GetComponent<Text>();
        qCodeImg = qCode.GetComponent<Image>();
        Transform wCode = SkillController.transform.Find("WCode/Mask");
        wCodeTime = wCode.Find("Text").GetComponent<Text>();
        wCodeImg = wCode.GetComponent<Image>();
        Transform eCode = SkillController.transform.Find("ECode/Mask");
        eCodeTime = eCode.Find("Text").GetComponent<Text>();
        eCodeImg = eCode.GetComponent<Image>();
        Transform rCode = SkillController.transform.Find("RCode/Mask");
        rCodeTime = rCode.Find("Text").GetComponent<Text>();
        rCodeImg = rCode.GetComponent<Image>();
        Transform aCode = SkillController.transform.Find("ACode/Mask");
        aCodeTime = aCode.Find("Text").GetComponent<Text>();
        aCodeImg = aCode.GetComponent<Image>();
        Transform sCode = SkillController.transform.Find("SCode/Mask");
        sCodeTime = sCode.Find("Text").GetComponent<Text>();
        sCodeImg = sCode.GetComponent<Image>();
        Transform dCode = SkillController.transform.Find("DCode/Mask");
        dCodeTime = dCode.Find("Text").GetComponent<Text>();
        dCodeImg = dCode.GetComponent<Image>();
        Transform fCode = SkillController.transform.Find("FCode/Mask");
        fCodeTime = fCode.Find("Text").GetComponent<Text>();
        fCodeImg = fCode.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {

        if (player != null && !isShowSkillRange)
        {
            HideSkillRange();
            if (Input.GetMouseButtonDown(1))
            {
                player.UpdateControl();

            }
            if (Input.GetKeyDown(KeyCode.Q) && QCd <= 0)
            {
                isShowSkillRange = true;
                skillId = 1;
            }
            if (Input.GetKeyDown(KeyCode.W) && WCd <= 0)
            {
                isShowSkillRange = true;
                skillId = 2;
            }
            if (Input.GetKeyDown(KeyCode.E) && ECd <= 0)
            {
                isShowSkillRange = true;
                skillId = 3;
            }
            if (Input.GetKeyDown(KeyCode.R) && RCd <= 0)
            {
                isShowSkillRange = true;
                skillId = 4;
            }
            if (Input.GetKeyDown(KeyCode.A) && ACd <= 0)
            {
                isShowSkillRange = true;
                skillId = 5;
            }
            if (Input.GetKeyDown(KeyCode.S) && SCd <= 0)
            {
                isShowSkillRange = true;
                skillId = 6;
            }
            if (Input.GetKeyDown(KeyCode.D) && DCd <= 0)
            {
                isShowSkillRange = true;
                skillId = 7;
            }
            if (Input.GetKeyDown(KeyCode.F) && FCd <= 0)
            {
                isShowSkillRange = true;
                skillId = 8;
            }
        }
        else if (player != null && isShowSkillRange)
        {

            Vector3 pos = new Vector3();
            Vector3 direction = new Vector3();
            HandleRange(skillId, ref pos, ref direction);
            if (Input.GetMouseButtonDown(0))
            {
                player.transform.LookAt(pos);
                player.ResetPlayer(0);
                HandleSkill(pos, direction);
                isShowSkillRange = false;
                skillId = 0;
                HideSkillRange();
                Cursor.SetCursor(mousePoint, Vector2.zero, CursorMode.Auto);

            }
            if (Input.GetMouseButtonDown(1))
            {
                isShowSkillRange = false;
                HideSkillRange();
                Cursor.SetCursor(mousePoint, Vector2.zero, CursorMode.Auto);

            }


        }

    }
    void HideSkillRange()
    {
        SkillRange.SetActive(false);
        SkillTip.SetActive(false);
        SkillTipA.SetActive(false);

    }

    private void HandleSkill(Vector3 pos, Vector3 direct)
    {
        SkillMsg skillMsg = new SkillMsg();
        skillMsg.X = pos.x;
        skillMsg.Y = pos.y;
        skillMsg.Z = pos.z;
        skillMsg.Ex = direct.x;
        skillMsg.Ey = direct.y;
        skillMsg.Ez = direct.z;
        skillMsg.SkillId = skillId;
        NetManager.Send(skillMsg);//广播
        HandleCd(skillId);
        SkillManger.Instance.Handle(player.transform, skillId, pos, direct, player.id);
    }

    private void HandleRange(int skillId, ref Vector3 pos, ref Vector3 direction)
    {
        //跟随鼠标
        //获取屏幕坐标
        Vector3 mousepostion = Input.mousePosition;
        //定义从屏幕
        Ray ray = Camera.main.ScreenPointToRay(mousepostion);
        RaycastHit hitInfo;
        if (!Physics.Raycast(ray, out hitInfo))
        {

            return;

        }
        //获取鼠标在场景中坐标
        Vector3 point = hitInfo.point;
        pos = new Vector3(point.x, 0, point.z);
        direction = pos - player.transform.position;
        SkillRange.SetActive(true);
        if (skillId == 1)
        {
            if (direction.sqrMagnitude > maxQRange * maxQRange)
            {
                pos = player.transform.position + direction.normalized * maxQRange;
            }
            SkillRange.transform.localScale = new Vector3(maxQRange, 0.01f, maxQRange);
            SkillTipA.SetActive(true);
            SkillTipA.transform.localScale = new Vector3(1, 1, 3);

        }
        if (skillId == 2)
        {
            if (direction.sqrMagnitude > maxWRange / 2 * maxWRange / 2)
            {
                pos = player.transform.position + direction.normalized * maxWRange / 2;
            }
            SkillRange.transform.localScale = new Vector3(maxWRange, 0.01f, maxWRange);
            SkillTip.SetActive(true);
            SkillTip.transform.localScale = new Vector3(5, 0.01f, 5);
        }
        if (skillId == 3)
        {
            if (direction.sqrMagnitude > maxERange * maxERange)
            {
                pos = player.transform.position + direction.normalized * maxERange;
            }
            SkillRange.transform.localScale = new Vector3(maxERange, 0.01f, maxERange);
            Cursor.SetCursor(mousePoint, Vector2.zero, CursorMode.Auto);
        }
        if (skillId == 4)
        {
            if (direction.sqrMagnitude > maxRRange * maxRRange)
            {
                pos = player.transform.position + direction.normalized * maxRRange;
            }
            SkillRange.transform.localScale = new Vector3(maxRRange, 0.01f, maxRRange);
            SkillTipA.SetActive(true);
            SkillTipA.transform.localScale = new Vector3(1, 1, 3);
        }
        if (skillId == 5)
        {
            if (direction.sqrMagnitude > maxARange / 2 * maxARange / 2)
            {
                pos = player.transform.position + direction.normalized * maxARange / 2;
            }
            SkillRange.transform.localScale = new Vector3(maxARange, 0.01f, maxARange);
            Cursor.SetCursor(mousePoint, Vector2.zero, CursorMode.Auto);
        }
        if (skillId == 6)
        {
            if (direction.sqrMagnitude > maxSRange / 2 * maxSRange / 2)
            {
                pos = player.transform.position + direction.normalized * maxSRange / 2;
            }
            SkillRange.transform.localScale = new Vector3(maxSRange, 0.01f, maxSRange);
            Cursor.SetCursor(mousePoint, Vector2.zero, CursorMode.Auto);
        }
        if (skillId == 7)
        {
            if (direction.sqrMagnitude > maxDRange / 2 * maxDRange / 2)
            {
                pos = player.transform.position + direction.normalized * maxDRange / 2;
            }
            SkillRange.transform.localScale = new Vector3(maxDRange, 0.01f, maxDRange);
            Cursor.SetCursor(mousePoint, Vector2.zero, CursorMode.Auto);
        }
        if (skillId == 8)
        {
            if (direction.sqrMagnitude > maxFRange / 2 * maxFRange / 2)
            {
                pos = player.transform.position + direction.normalized * maxFRange / 2;
            }
            SkillRange.transform.localScale = new Vector3(maxFRange, 0.01f, maxFRange);
            SkillTip.SetActive(true);
            SkillTip.transform.localScale = new Vector3(5, 0.01f, 5);
        }
        SkillTip.transform.position = pos;
        SkillTipA.transform.LookAt(pos);
    }

    private void HandleCd(int skillId)
    {

        if (skillId == 1)
        {
            QCd = QMCd;
        }
        if (skillId == 2)
        {
            WCd = WMCd;
        }
        if (skillId == 3)
        {
            ECd = EMCd;
        }
        if (skillId == 4)
        {
            RCd = RMCd;
        }
        if (skillId == 5)
        {
            ACd = AMCd;
        }
        if (skillId == 6)
        {
            SCd = SMCd;
        }
        if (skillId == 7)
        {
            DCd = DMCd;
        }
        if (skillId == 8)
        {
            FCd = FMCd;
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
        qCodeImg.fillAmount = QCd / QMCd;
        HandleCDView(qCodeTime, QCd);
       
        wCodeImg.fillAmount = WCd / WMCd;
        HandleCDView(wCodeTime, WCd);
  
        eCodeImg.fillAmount = ECd / EMCd;
        HandleCDView(eCodeTime, ECd);

        rCodeImg.fillAmount = RCd / RMCd;
        HandleCDView(rCodeTime, RCd);

        aCodeImg.fillAmount = ACd / AMCd;
        HandleCDView(aCodeTime, ACd);

        sCodeImg.fillAmount = SCd / SMCd;
        HandleCDView(sCodeTime, SCd);

        dCodeImg.fillAmount = DCd / DMCd;
        HandleCDView(dCodeTime, DCd);

        fCodeImg.fillAmount = FCd / FMCd;
        HandleCDView(fCodeTime, FCd);
    }

    private void HandleCDView(Text codeTime, float cd)
    {
        int cdVal = Convert.ToInt32(cd);
        if (cdVal == 0)
        {
            codeTime.text = "";
        }
        else
        {
            codeTime.text = cdVal.ToString() + "s";

        }
    }
}
