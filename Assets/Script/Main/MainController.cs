using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainController : MonoBehaviour
{
    public static UserInfo user;
    public Button JoinRoomBtn;
    // Start is called before the first frame update
    void Start()
    {
        //网络协议监听
        /* NetManager.AddMsgListener("EnterMsg", OnMsgEnter);
         //room
         NetManager.AddMsgListener("MsgGetRoomList", NetRoomHandler.OnMsgGetRoomList);
         NetManager.AddMsgListener("MsgCreateRoom", NetRoomHandler.OnMsgCreateRoom);
         NetManager.AddMsgListener("MsgEnterRoom", NetRoomHandler.OnMsgEnterRoom);
         NetManager.AddMsgListener("MsgGetRoomInfo", NetRoomHandler.OnMsgGetRoomInfo);
         NetManager.AddMsgListener("MsgLeaveRoom", NetRoomHandler.OnMsgLeaveRoom);
         NetManager.AddMsgListener("MsgLeaveGame", NetRoomHandler.OnMsgLeaveGame);*/
        //网络事件监听
        NetManager.AddEventListener(NetManager.NetEvent.ConnectSucc, OnConnectSucc);
        NetManager.AddEventListener(NetManager.NetEvent.ConnectFail, OnConnectFail);
        NetManager.AddEventListener(NetManager.NetEvent.Close, OnConnectClose);
        NetManager.Connect("192.168.1.105", 8888);
        NetManager.AddMsgListener("EnterMsg", (msgBase) =>
        {
            EnterMsg msg = (EnterMsg)msgBase;
            if (msg.code == "200")
            {
                SceneManager.LoadScene("RoomScene");
            }
            else
            {
                PanelManger.Open<SystemTipPanel>(msg.msg);
            }
        });

        JoinRoomBtn.onClick.AddListener(HrefRoomList);
        NetManager.AddMsgListener("MsgKick", OnMsgKick);
    }

    private void OnConnectFail(string err)
    {
        Debug.Log("连接失败");
    }

    private void OnConnectSucc(string err)
    {
        Debug.Log("连接成功");
    }

    private void OnMsgKick(MsgBase msgBase)
    {
        Debug.Log("下线");
    }

    private void OnConnectClose(string err)
    {
        Debug.Log("close");
    }

    void HrefRoomList()
    {
        EnterMsg enterMsg = new EnterMsg();
        enterMsg.nickname = user.Nickname;
        enterMsg.token = user.Token;
        NetManager.Send(enterMsg);
        //SceneManager.LoadScene("RoomListScene");
    }
    private void Update()
    {
        NetManager.Update();
    }
}
