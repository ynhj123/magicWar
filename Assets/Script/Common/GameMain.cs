using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMain : MonoBehaviour
{
    private static GameMain instance = null;

    public static GameMain Instance
    {
        get
        {
            return instance;
        }
    }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        EquipManager.Instance().Init();
        //网络协议监听

        //网络事件监听
        NetManager.AddEventListener(NetManager.NetEvent.ConnectSucc, OnConnectSucc);
        NetManager.AddEventListener(NetManager.NetEvent.ConnectFail, OnConnectFail);
        NetManager.AddEventListener(NetManager.NetEvent.Close, OnConnectClose);
        NetManager.AddMsgListener("EnterMsg", (msgBase) =>
        {
            EnterMsg msg = ProtobufMapper.Deserialize<EnterMsg>(msgBase.content);
            if (msg.Code == "200")
            {
                SceneManager.LoadScene("MainScene");

            }
            else
            {
                PanelManger.Open<SystemTipPanel>(msg.Msg);
            }
        });

        NetManager.AddMsgListener("MsgKick", OnMsgKick);
    }

   


    private void OnConnectFail(string err)
    {
        Debug.Log("连接失败");
    }

    private void OnConnectSucc(string err)
    {
        Debug.Log("连接成功");
        Enter();
    }

    private void OnMsgKick(MsgBase msgBase)
    {
        Debug.Log("下线");
    }

    private void OnConnectClose(string err)
    {
        Debug.Log("close");
    }

    public void Enter()
    {
        EnterMsg enterMsg = new EnterMsg();
        enterMsg.Nickname = MainController.user.Nickname;
        enterMsg.Token = MainController.user.Token;
        NetManager.Send(enterMsg);
    }
}
