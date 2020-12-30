using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainController : MonoBehaviour
{
    public static UserInfo user;
    public Button JoinRoomBtn;
    Button OpenGoodsBtn;
    Button OpenUserDetailBtn;
    Button OpenSkillBtn;
    Button OpenEquipBtn;
    // Start is called before the first frame update
    void Start()
    {
        PanelManger.Init();
        //按键
        // JoinRoomBtn = transform.Find("Canvas/Top/JoinBtn").GetComponent<Button>();
        JoinRoomBtn.onClick.AddListener(HrefRoomList);

        OpenGoodsBtn = transform.Find("Canvas/Top/StoreBtn").GetComponent<Button>();
        OpenGoodsBtn.onClick.AddListener(OpenGoodsPanel);
        OpenSkillBtn = transform.Find("Canvas/Top/SkillBtn").GetComponent<Button>();
        OpenSkillBtn.onClick.AddListener(OpenSkillPanel);
        OpenEquipBtn = transform.Find("Canvas/Top/EquipBtn").GetComponent<Button>();
        OpenEquipBtn.onClick.AddListener(OpenEquipPanel);
        

        OpenUserDetailBtn = transform.Find("Canvas/Top/Header/UserDetailBtn").GetComponent<Button>();
        OpenUserDetailBtn.onClick.AddListener(OpenUserDetailPanel);
        
    }

    private void OpenEquipPanel()
    {
        PanelManger.Open<EquipPanel>();
    }


    private void OpenSkillPanel()
    {
        PanelManger.Open<SkillPanel>();
    }

    private void OpenUserDetailPanel()
    {
        PanelManger.Open<UserDetailPanel>();
    }

    private void OpenGoodsPanel()
    {
        PanelManger.Open<GoodsPanel>();
    }

    

    void HrefRoomList()
    {
         SceneManager.LoadScene("RoomScene");
    }
    private void Update()
    {
        NetManager.Update();
    }
}
