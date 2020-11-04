using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Player : BasePlayer
{

	//上一次发送同步信息的时间
	private float lastSendSyncTime = 0;
	//同步帧率
	public static float syncInterval = 0.05f;

    public override void Update()
    {
		base.Update();
		MoveUpdate();
		JudgeMentDebuff();
		CheckIsAlive();
		SyncUpdate();

	}
	//发送同步信息
	public void SyncUpdate()
	{
		//时间间隔判断
		if (Time.time - lastSendSyncTime < syncInterval)
		{
			return;
		}
		lastSendSyncTime = Time.time;
		//发送同步协议
		MsgSyncPlayer msg = new MsgSyncPlayer();
		msg.x = transform.position.x;
		msg.y = transform.position.y;
		msg.z = transform.position.z;
		msg.ex = transform.eulerAngles.x;
		msg.ey = transform.eulerAngles.y;
		msg.ez = transform.eulerAngles.z;
		
		NetManager.Send(msg);
	}
}
