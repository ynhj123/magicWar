
//玩家信息
[System.Serializable]
public class PlayerInfo
{
    public string uid = "";  //玩家id
    public string nickname = "";

    public int degree = 0;    //位置
    public int hp = 0;      //生命值

    public float x = 0;     //
    public float z = 0;
    public float ey = 0;

    public float attach;
    public float defense;
    public float speed;

    public int killNum;


}
//玩家rank信息
[System.Serializable]
public class PlayerRank
{
    public string id = "";  //玩家id
    public string nickname = "";
    public int socre = 0;
    public int killNum;

}


//进入战场（服务端推送）
public class LoadFinishMsg : MsgBase
{
    public LoadFinishMsg() { protoName = "LoadFinishMsg"; }
    //服务端回
    public string code;
    public string msg;
    public PlayerInfo[] players;
    public int mapId = 1;	//地图，只有一张
}
public class EndMsg : MsgBase
{
    public EndMsg() { protoName = "EndMsg"; }
    //服务端回
    public PlayerInfo[] players;

}

//战斗结果（服务端推送）
public class BattleResultMsg : MsgBase
{
    public BattleResultMsg() { protoName = "BattleResultMsg"; }
    //服务端回
    //public int winCamp = 0;	 //获胜的阵营
    public PlayerRank[] players;
}

//玩家退出（服务端推送）
public class MsgLeaveBattle : MsgBase
{
    public MsgLeaveBattle() { protoName = "MsgLeaveBattle"; }
    //服务端回
    public string id = "";	//玩家id
}