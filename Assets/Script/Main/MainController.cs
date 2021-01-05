using System;
using System.Text;
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

    private void CreateEquipTestData()
    {
        string[] equips = new string[1830];
        int id = 1;
        for (int i = 0; i <= 999; i++)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(id).Append("|");
           
            stringBuilder.Append("test" + id).Append("|");
            stringBuilder.Append("Ui/equip0_" + i).Append("|");
            stringBuilder.Append("des" + id).Append("|");
            stringBuilder.Append('a').Append("|");
            equips[id - 1] = stringBuilder.ToString();
            id++;
        }
        for (int i = 0; i <= 829; i++)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(id).Append("|");
            stringBuilder.Append("test" + id).Append("|");
            stringBuilder.Append("Ui/equip1_" + i).Append("|");
            stringBuilder.Append("des" + id).Append("|");
            stringBuilder.Append('b').Append("|");
            equips[id - 1] = stringBuilder.ToString();
            id++;

        }
        FileUtils.WriteLines(Application.streamingAssetsPath + "/equipData", equips);
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
