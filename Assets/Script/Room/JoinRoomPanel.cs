
using UnityEngine;
using UnityEngine.UI;

public class JoinRoomPanel : BasePanel
{
    InputField roomId;
    Button closeBtn;
    Button joinBtn;
    // Start is called before the first frame update
    public override void OnInit()
    {
        skinPath = "Panel/JoinRoomPanel";
        layer = PanelManger.Layer.Panel;
    }

    public override void OnShow(params object[] objects)
    {
        roomId = skin.transform.Find("Background/InputField").GetComponent<InputField>();
        joinBtn = skin.transform.Find("Background/Join").GetComponent<Button>();
        closeBtn = skin.transform.Find("Background/Close").GetComponent<Button>();
        joinBtn.onClick.AddListener(OnJoin);
        closeBtn.onClick.AddListener(OnCloseBtn);
    }

    private void OnJoin()
    {
        Debug.Log("Join:" + roomId.text);

        /*  PanelManger.Open<Buttons>();
          PanelManger.Close<LoginPanel>();*/
        EnterRoomMsg msg = new EnterRoomMsg();
        msg.roomId = roomId.text;
        NetManager.Send(msg);
        //SceneManager.LoadScene("RoomScene");
    }
    void OnCloseBtn()
    {
        PanelManger.Close<JoinRoomPanel>();
    }

    public override void OnClose()
    {
    }
}
