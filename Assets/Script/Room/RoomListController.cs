﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomListController : MonoBehaviour
{
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

    void FlushRoomList()
    {


        for (int i = 0; i < roomItemScripts.Count; i++)
        {

            RoomItemScript roomItemScript = roomItemScripts[i];
            if (i < rooms.Length)
            {

                RoomInfo roomInfo = rooms[i];
                roomItemScript.UpdateContent(roomInfo.id, roomInfo.count + "/" + roomInfo.maxCount, roomInfo.status);
                roomItemScript.Show();
            }
            else
            {
                roomItemScript.Hide();
            }
        }
    }
    void GetRoomList()
    {
        RoomListMsg roomListMsg = new RoomListMsg();
        roomListMsg.curPage = curPage;
        roomListMsg.pageSize = pageSize;
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
        NetManager.AddMsgListener("CreateRoomMsg", (msgBase) =>
        {
            CreateRoomMsg msg = (CreateRoomMsg)msgBase;
            if (msg.code == "200")
            {
                PanelManger.Open<RoomPanel>(msg.roomId);
            }
            else
            {
                PanelManger.Open<SystemTipPanel>(msg.msg);
            }

        });
        NetManager.AddMsgListener("RoomListMsg", (msgBase) =>
        {
            RoomListMsg msg = (RoomListMsg)msgBase;

            if (msg.code == "200")
            {
                rooms = msg.rooms;
                size = msg.size;
                FlushRoomList();
            }
            else
            {
                PanelManger.Open<SystemTipPanel>(msg.msg);
            }

        });
        NetManager.AddMsgListener("EnterRoomMsg", (msgBase) =>
        {
            Debug.Log("enterroom");
            EnterRoomMsg msg = (EnterRoomMsg)msgBase;
            Debug.Log(msg);
            if (msg.code == "200")
            {
                PanelManger.Open<RoomPanel>(msg.roomId);
            }
            else
            {
                PanelManger.Open<SystemTipPanel>(msg.msg);
            }
        });
        //DontDestroyOnLoad(transform);
        GetRoomList();
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

}
