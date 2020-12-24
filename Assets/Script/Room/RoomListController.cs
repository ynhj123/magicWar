using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomListController : MonoBehaviour
{
    public static RoomListController instance;
    private void Awake()
    {
        instance = this;
    }
    Button createRoomBtn; //创建房间
    Button joinRoomBtn; //加入房间
    Button getLastBtn;  //获取上一页房间
    Button getNextBtn;  //获取下一页房间
    Button getRoomList; //刷新房间列表
    Button quit; //退出
    RoomInfo[] rooms;
    List<RoomItemScript> roomItemScripts;

    int curPage;
    int pageSize;
    int size;

    // Start is called before the first frame update

    private void ShowJoinRoom()
    {
        PanelManger.Open<JoinRoomPanel>();
    }

    public void FlushRoomList()
    {


        for (int i = 0; i < roomItemScripts.Count; i++)
        {

            RoomItemScript roomItemScript = roomItemScripts[i];
            if (i < rooms.Length)
            {

                RoomInfo roomInfo = rooms[i];
                roomItemScript.UpdateContent(roomInfo.Id, roomInfo.Count + "/" + roomInfo.MaxCount, roomInfo.Status);
                roomItemScript.Show();
            }
            else
            {
                roomItemScript.Hide();
            }
        }
    }
    public void GetRoomList()
    {
        RoomListMsg roomListMsg = new RoomListMsg();
        roomListMsg.CurPage = curPage;
        roomListMsg.PageSize = pageSize;
        Debug.Log("GetRoomList");
        NetManager.Send(roomListMsg);
    }
    void GetLast()
    {
        if (curPage == 1)
        {
            return;
        }
        curPage = curPage - 1;
        GetRoomList();
    }
    void GetNext()
    {
        if ((curPage) * pageSize > size)
        {
            return;
        }
        curPage = curPage + 1;
        GetRoomList();
    }
    void Start()
    {

        PanelManger.Init();

        roomItemScripts = new List<RoomItemScript>();
        Transform roomParents = transform.Find("Canvas/Content/RoomList/Viewport/Content");
        foreach (Transform room in roomParents)
        {
            roomItemScripts.Add(room.Find("RoomItem").GetComponent<RoomItemScript>());
        }
        curPage = 1;
        pageSize = 10;
        createRoomBtn = transform.Find("Canvas/Buttom/CreateRoomBtn").GetComponent<Button>();
        joinRoomBtn = transform.Find("Canvas/Buttom/JoinRoom").GetComponent<Button>();
        getLastBtn = transform.Find("Canvas/Buttom/PreviousPage").GetComponent<Button>();
        getNextBtn = transform.Find("Canvas/Buttom/NextPage").GetComponent<Button>();
        getRoomList = transform.Find("Canvas/Buttom/FlushPage").GetComponent<Button>();
        createRoomBtn.onClick.AddListener(CreateRoom);
        getLastBtn.onClick.AddListener(GetLast);
        getNextBtn.onClick.AddListener(GetNext);
        getRoomList.onClick.AddListener(GetRoomList);
        joinRoomBtn.onClick.AddListener(ShowJoinRoom);
        NetManager.AddMsgListener("CreateRoomMsg", OnCreateRoom);
        NetManager.AddMsgListener("RoomListMsg", OnRoomList);
        NetManager.AddMsgListener("EnterRoomMsg", OnEnterRoom);
        //DontDestroyOnLoad(transform);
        GetRoomList();
    }

    private void OnEnterRoom(MsgBase msgBase)
    {
        Debug.Log("enterroom");
        EnterRoomMsg msg = ProtobufMapper.Deserialize<EnterRoomMsg>(msgBase.content);
        Debug.Log(msg);
        if (msg.Code == "200")
        {
            PanelManger.Open<RoomPanel>(msg.RoomId);
        }
        else
        {
            PanelManger.Open<SystemTipPanel>(msg.Msg);
        }
    }

    private void OnRoomList(MsgBase msgBase)
    {
        RoomListMsg msg =  ProtobufMapper.Deserialize<RoomListMsg>(msgBase.content);

        if (msg.Code == "200")
        {
            msg.Rooms.CopyTo(rooms,0);
            size = msg.Size;
            FlushRoomList();
        }

        else
        {
            PanelManger.Open<SystemTipPanel>(msg.Msg);
        }

    }

    private void OnCreateRoom(MsgBase msgBase)
    {
        CreateRoomMsg msg = ProtobufMapper.Deserialize<CreateRoomMsg>(msgBase.content);
        if (msg.Code == "200")
        {
            PanelManger.Open<RoomPanel>(msg.RoomId);
        }
        else
        {
            PanelManger.Open<SystemTipPanel>(msg.Msg);
        }
    }

    void CreateRoom()
    {
        CreateRoomMsg createRoomMsg = new CreateRoomMsg();
        NetManager.Send(createRoomMsg);
    }
    // Update is called once per frame
    void Update()
    {

        NetManager.Update();
    }
    private void OnDestroy()
    {
        NetManager.RemoveMsgListener("CreateRoomMsg", OnCreateRoom);
        NetManager.RemoveMsgListener("RoomListMsg", OnRoomList);
        NetManager.RemoveMsgListener("EnterRoomMsg", OnEnterRoom);

    }

}
