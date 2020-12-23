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
    bool isShowSkillRange = false;

    GameObject SkillController;
    GameObject SkillRange;
    GameObject SkillTip;
    GameObject SkillTipA;
    public Player player;
    float maxRange = 10;
    int skillId = 0;
    // Start is called before the first frame update
    void Start()
    {
        player.GetComponent<Player>();
        SkillController = GameObject.Find("/Root/Canvas/Controller/SkillController");
        SkillRange = player.transform.Find("SkillRange").gameObject;
        SkillTip = player.transform.Find("SkillTip").gameObject;
        SkillTipA = player.transform.Find("SkillTipAParent").gameObject;
        isShowSkillRange = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (player != null && !isShowSkillRange)
        {

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
            Vector3 pos = new Vector3(point.x, 0, point.z);
            Vector3 direction = pos - player.transform.position;
            if (direction.sqrMagnitude > maxRange * maxRange)
            {
                pos = player.transform.position + direction.normalized * maxRange;
            }
            SkillTip.transform.position = pos;
            SkillTipA.transform.LookAt(pos);
            if (Input.GetMouseButtonDown(0))
            {
                player.transform.LookAt(pos);
                player.ResetPlayer(0);
                HandleSkill(pos, direction);
                isShowSkillRange = false;
                skillId = 0;
            }
            if (Input.GetMouseButtonDown(1))
            {
                isShowSkillRange = false;
            }
        }
        if (player != null)
        {
            
            SkillRange.SetActive(isShowSkillRange);
            SkillTip.SetActive(isShowSkillRange);
            SkillTipA.SetActive(isShowSkillRange);
        }

    }

    private void HandleSkill(Vector3 pos, Vector3 direct)
    {
        SkillMsg skillMsg = new SkillMsg();
        skillMsg.x = pos.x;
        skillMsg.y = pos.y;
        skillMsg.z = pos.z;
        skillMsg.ex = direct.x;
        skillMsg.ey = direct.y;
        skillMsg.ez = direct.z;
        skillMsg.skillId = skillId;
        NetManager.Send(skillMsg);//广播
        HandleCd(skillId);
        SkillManger.Instance.Handle(player.transform, skillId, pos, direct, player.id);
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
        Transform qCode = SkillController.transform.Find("QCode/Mask");
        qCode.GetComponent<Image>().fillAmount = QCd / QMCd;
        HandleCDView(qCode, QCd);

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

    private void HandleCDView(Transform code, float cd)
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
