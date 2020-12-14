//同步坦克信息
public class SyncPlayerMsg : MsgBase
{
    public SyncPlayerMsg() { protoName = "SyncPlayerMsg"; }
    //位置、旋转
    public float x = 0f;
    public float z = 0f;
    public float ey = 0f;

    public float speed;
    public float hp = 0;
    public int killNum;
    public long frame;
    
    //服务端补充
    public string uid = "";		//哪个玩家
}

//开火
public class SkillMsg : MsgBase
{
    public SkillMsg() { protoName = "SkillMsg"; }
    //技能初始位置、旋转
    public float x = 0f;
    public float y = 0f;
    public float z = 0f;
    public float ex = 0f;
    public float ey = 0f;
    public float ez = 0f;
    //技能id
    public int skillId = 0;
    //服务端补充
    public string uid = "";      //哪个玩家
}

//击中
public class HitMsg : MsgBase
{
    public HitMsg() { protoName = "HitMsg"; }
    //谁打了我
    public string targetId = "";
    public int skillId;
    //击中点	
    public float x = 0f;
    public float y = 0f;
    public float z = 0f;
    //服务端补充
    public string uid = "";      //哪个玩家被打
}

public class MsgDebuff : MsgBase
{
    public MsgDebuff() { protoName = "MsgDebuff"; }

    public string id = "";      //哪个玩家
    public int hp = 0;          //被击中玩家血量
    public int damage = 0;      //受到的伤害
    public int debuffSpeed = 0; //减速影响
}