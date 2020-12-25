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

        //网络事件监听
        NetManager.AddEventListener(NetManager.NetEvent.ConnectSucc, OnConnectSucc);
        NetManager.AddEventListener(NetManager.NetEvent.ConnectFail, OnConnectFail);
        NetManager.AddEventListener(NetManager.NetEvent.Close, OnConnectClose);
        NetManager.Connect("192.168.1.105", 8222);
        NetManager.AddMsgListener("EnterMsg", (msgBase) =>
        {
            EnterMsg msg = ProtobufMapper.Deserialize<EnterMsg>(msgBase.content);
            if (msg.Code == "200")
            {
                SceneManager.LoadScene("RoomScene");
            }
            else
            {
                PanelManger.Open<SystemTipPanel>(msg.Msg);
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
        enterMsg.Nickname = user.Nickname;
        enterMsg.Token = user.Token;
        NetManager.Send(enterMsg);
        //SceneManager.LoadScene("RoomListScene");
    }
    private void Update()
    {
        NetManager.Update();
    }
}
