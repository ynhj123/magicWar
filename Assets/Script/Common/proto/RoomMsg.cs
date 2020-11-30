//查询成绩
public class MsgGetAchieve : MsgBase
{
    public MsgGetAchieve() { protoName = "MsgGetAchieve"; }
    //服务端回
    public int win = 0;
    public int lost = 0;
}

//房间信息
[System.Serializable]
public class RoomInfo
{
    public string id;      //房间id
    public int maxCount = 0; // 房间最大人数
    public int count = 0;   //人数
    public int status = 0;	//状态0-准备中 1-战斗中
}

//请求房间列表
public class RoomListMsg : MsgBase
{
    public RoomListMsg() { protoName = "RoomListMsg"; }
    public int curPage;
    public int pageSize;

    //服务端回
    public int size;
    public RoomInfo[] rooms;
    public string code;
    public string msg;
}

//创建房间
public class CreateRoomMsg : MsgBase
{
    public CreateRoomMsg() { protoName = "CreateRoomMsg"; }
    //服务端回
    public string roomId = "";
    public string code = "";
    public string msg = "";
}

//进入房间
public class EnterRoomMsg : MsgBase
{
    public EnterRoomMsg() { protoName = "EnterRoomMsg"; }
    public string roomId = "";
    public string code = "";

    public string msg = "";
}


//玩家信息
[System.Serializable]
public class PlayerRoom
{
    public string uid = "lpy";  //id
    public string username = "";  //用户名
    public string nickname = "";  //名称
    public int degree;      //位置 0房主
    public int score = 0;       //积分
    public int roomStatus; //0待准备 1准备

}


//获取房间信息
public class GetRoomInfoMsg : MsgBase
{
    public GetRoomInfoMsg() { protoName = "GetRoomInfoMsg"; }
    //服务端回
    public string code;
    public string msg;
    public PlayerRoom[] players;
}

//离开房间
public class LeaveRoomMsg : MsgBase
{
    public LeaveRoomMsg() { protoName = "LeaveRoomMsg"; }
    //服务端回
    public string code;
    public string msg;
}

//开战
public class StartMsg : MsgBase
{
    public StartMsg() { protoName = "StartMsg"; }
    //服务端回
    public string code = "";
    public string msg;
    
}
//准备
public class ReadyStartMsg : MsgBase
{
    public ReadyStartMsg() { protoName = "ReadyStartMsg"; }

}

//取消准备
public class UnreadyStartMsg : MsgBase
{
    public UnreadyStartMsg() { protoName = "UnreadyStartMsg"; }



}
public class KickRoomMsg : MsgBase
{
    public KickRoomMsg() { protoName = "KickRoomMsg"; }
    public string uid = "";
    public string code = "";
    public string msg;

}