using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoomPanel : BasePanel
{
    

    string roomId;
    bool isOwn;
    PlayerRoom mySelf;
    Text roomIdVo;
    Button startGame;
    Button readyStart;
    Button unreadyStart;
    Button quit;
    Button sendBtn;
    InputField sendContent;
    Text chatContent;
    PlayerRoom[] playerRooms;
    ScrollRect scrollRect;
    List<RoomPlayerScript> roomPlayerScripts;

    // Start is called before the first frame update
    public override void OnInit()
    {
        skinPath = "Panel/RoomPanel";
        layer = PanelManger.Layer.Panel;
    }

   

 

    private void OnStartMsg(MsgBase msgBase)
    {
        StartMsg msg = (StartMsg)msgBase;
        if (msg.code == "200")
        {
            Close();
            SceneManager.LoadScene("LoadScene");


        }
        else
        {
            PanelManger.Open<SystemTipPanel>(msg.msg);
        }
    }

    private void OnSendChatMsg()
    {
        ChatRoomMsg chatRoomMsg = new ChatRoomMsg();
        chatRoomMsg.content = sendContent.text;
        NetManager.Send(chatRoomMsg);
    }

    void OnGetRoomInfoMsg(MsgBase msgBase)
    {
        GetRoomInfoMsg msg = (GetRoomInfoMsg)msgBase;
        if (msg.code == "200")
        {
            playerRooms = msg.players;
            FlushView();
        }
        else
        {
            PanelManger.Open<SystemTipPanel>(msg.msg);
        }
    }
    public override void OnClose()
    {
        NetManager.RemoveMsgListener("GetRoomInfoMsg", OnGetRoomInfoMsg);
        NetManager.RemoveMsgListener("LeaveRoomMsg", OnLeaveRoomMsg);
        NetManager.RemoveMsgListener("ChatRoomMsg", OnChatRoomMsg);
        NetManager.RemoveMsgListener("StartMsg", OnStartMsg);
    }

    public override void OnShow(params object[] objs)
    {
        object[] objects = objs;
        roomPlayerScripts = new List<RoomPlayerScript>();
        Transform roomPlayers = skin.transform.Find("Right/Content/RoomPlayers/Viewport/Content");
        foreach (Transform roomPlayer in roomPlayers)
        {
            RoomPlayerScript roomPlayerScript = roomPlayer.GetComponent<RoomPlayerScript>();
            //roomPlayerScript.Init();
            //Debug.Log(roomPlayerScript.gameObject);

            roomPlayerScripts.Add(roomPlayerScript);
        }

        roomIdVo = skin.transform.Find("Header/RoomId").GetComponent<Text>();
        roomId = objects[0].ToString();
        roomIdVo.text = "房间号：" + roomId;
        startGame = skin.transform.Find("Right/Bottom/Start").GetComponent<Button>();
        readyStart = skin.transform.Find("Right/Bottom/Prepare").GetComponent<Button>();
        unreadyStart = skin.transform.Find("Right/Bottom/Unprepare").GetComponent<Button>();
        quit = skin.transform.Find("Right/Bottom/Quit").GetComponent<Button>();
        sendBtn = skin.transform.Find("Left/Bottom/Send").GetComponent<Button>();
        sendContent = skin.transform.Find("Left/Bottom/SendContent").GetComponent<InputField>();
        chatContent = skin.transform.Find("Left/Chat/ChatView/Viewport/Content").GetComponent<Text>();
        scrollRect = skin.transform.Find("Left/Chat/ChatView").GetComponent<ScrollRect>();

        startGame.onClick.AddListener(StartGame);
        readyStart.onClick.AddListener(ReadyStart);
        unreadyStart.onClick.AddListener(UnreadyStart);
        quit.onClick.AddListener(OnQuit);
        sendBtn.onClick.AddListener(OnSendChatMsg);
        NetManager.AddMsgListener("GetRoomInfoMsg", OnGetRoomInfoMsg);
        NetManager.AddMsgListener("LeaveRoomMsg", OnLeaveRoomMsg);
        NetManager.AddMsgListener("ChatRoomMsg", OnChatRoomMsg);
        NetManager.AddMsgListener("StartMsg", OnStartMsg);
        GetRoomInfoMsg getRoomInfoMsg = new GetRoomInfoMsg();
        NetManager.Send(getRoomInfoMsg);
    }

    private void FlushView()
    {
        for (int i = 0; i < roomPlayerScripts.Count; i++)
        {
            RoomPlayerScript roomPlayerScript = roomPlayerScripts[i];
            if (i < playerRooms.Length)
            {
                PlayerRoom playerRoom = playerRooms[i];
                roomPlayerScript.FlushView(i, playerRoom.nickname, playerRoom.roomStatus, playerRoom.uid);
            }
            else
            {
                roomPlayerScript.HideAll();
            }
        }
        //状态 自己是房主 // 自己不是房主
        for (int i = 0; i < playerRooms.Length; i++)
        {
            PlayerRoom playerRoom = playerRooms[i];
            //自己
            if (playerRoom.uid == MainController.user.Uid)
            {
                mySelf = playerRoom;
                //房主
                if (i == 0)
                {
                    isOwn = true;
                    ShowStartBtn();
                    HideReadyBtn();
                    HideUnreadyBtn();
                    //显示开始按钮
                    //关闭准备按钮
                    //关闭取消准备
                }
                else
                {
                    isOwn = false;
                    if (playerRoom.roomStatus == 0)
                    {
                        HideStartBtn();
                        ShowReadyBtn();
                        HideUnreadyBtn();
                    }
                    else
                    {
                        HideStartBtn();
                        HideReadyBtn();
                        ShowUnreadyBtn();
                    }
                }
            }

        }

        for (int i = 0; i < playerRooms.Length; i++)
        {
            RoomPlayerScript roomPlayerScript = roomPlayerScripts[i];
            if (i == 0)
            {
                roomPlayerScript.HideKick();
            }
            else if (isOwn)
            {
                roomPlayerScript.ShowKick();

            }
            else
            {
                roomPlayerScript.HideKick();
            }

        }

    }
    void ShowStartBtn()
    {
        CanvasGroup canvasGroup = startGame.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }
    void ShowReadyBtn()
    {
        CanvasGroup canvasGroup = readyStart.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }
    void ShowUnreadyBtn()
    {
        CanvasGroup canvasGroup = unreadyStart.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }
    void HideUnreadyBtn()
    {
        CanvasGroup canvasGroup = unreadyStart.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
    void HideReadyBtn()
    {
        CanvasGroup canvasGroup = readyStart.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
    void HideStartBtn()
    {
        CanvasGroup canvasGroup = startGame.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    private void UnreadyStart()
    {
        if (mySelf.roomStatus == 1)
        {
            NetManager.Send(new UnreadyStartMsg());
        }
    }

    private void ReadyStart()
    {
        Debug.Log("readystart");
        Debug.Log(mySelf.roomStatus);
        if (mySelf.roomStatus == 0)
        {
            NetManager.Send(new ReadyStartMsg());
        }
    }



    private void OnQuit()
    {
        LeaveRoomMsg leaveRoomMsg = new LeaveRoomMsg();
        NetManager.Send(leaveRoomMsg);
        //Close();
    }
    void OnLeaveRoomMsg(MsgBase msgBase)
    {
        LeaveRoomMsg msg = (LeaveRoomMsg)msgBase;
        if (msg.code == "200")
        {
            Close();
            RoomListController.instance.FlushRoomList();
        }
    }
    void OnChatRoomMsg(MsgBase msgBase)
    {
        ChatRoomMsg msg = (ChatRoomMsg)msgBase;

        chatContent.text += GetPlayerRoomByUid(msg.fromId).nickname + ":" + msg.content + "\n";
        scrollRect.verticalNormalizedPosition = 0;
        sendContent.text = "";
    }
    PlayerRoom GetPlayerRoomByUid(string uid)
    {
        for (int i = 0; i < playerRooms.Length; i++)
        {
            if (playerRooms[i].uid == uid)
            {
                return playerRooms[i];
            }
        }
        return null;
    }

    private void StartGame()
    {
        StartMsg msg = new StartMsg();
        NetManager.Send(msg);
        //SceneManager.LoadScene("BattleScene");
    }

}
