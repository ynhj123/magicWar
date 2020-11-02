﻿//同步坦克信息
public class MsgSyncPlayer:MsgBase {
	public MsgSyncPlayer() {protoName = "MsgSyncPlayer"; }
	//位置、旋转、炮塔旋转
	public float x = 0f;		
	public float y = 0f;
	public float z = 0f;
	public float ex = 0f;		
	public float ey = 0f;
	public float ez = 0f;

	public float hp = 0;
	//服务端补充
	public string id = "";		//哪个玩家
}

//开火
public class MsgFire:MsgBase {
	public MsgFire() {protoName = "MsgFire";}
	//技能初始位置、旋转
	public float x = 0f;		
	public float y = 0f;
	public float z = 0f;
	public float ex = 0f;
	public float ey = 0f;
	public float ez = 0f;
	//服务端补充
	public string id = "";      //哪个玩家
}

//击中
public class MsgHit:MsgBase {
	public MsgHit() {protoName = "MsgHit";}
	//击中谁
	public string targetId = "";
	//击中点	
	public float x = 0f;		
	public float y = 0f;
	public float z = 0f;
	//服务端补充
	public string id = "";		//哪个玩家
	public int hp = 0;			//被击中玩家血量
	public int damage = 0;		//受到的伤害
}