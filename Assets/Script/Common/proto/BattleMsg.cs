
//坦克信息
[System.Serializable]
public class PlayerInfo
{
    public string id = "";  //玩家id
    public int camp = 0;    //阵营
    public int hp = 0;      //生命值

    public float x = 0;     //位置
    public float z = 0;
    public float ey = 0;


}
//坦克rank信息
[System.Serializable]
public class PlayerRank
{
    public string id = "";  //玩家id
    public int rank = 0;
    public int killNum;

}


//进入战场（服务端推送）
public class MsgEnterBattle : MsgBase
{
    public MsgEnterBattle() { protoName = "MsgEnterBattle"; }
    //服务端回
    public PlayerInfo[] players;
    public int mapId = 1;	//地图，只有一张
}

//战斗结果（服务端推送）
public class MsgBattleResult : MsgBase
{
    public MsgBattleResult() { protoName = "MsgBattleResult"; }
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