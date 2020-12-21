using UnityEngine;

public class Player : BasePlayer
{

    //上一次发送同步信息的时间
    private float lastSendSyncTime = 0;
    //同步帧率
    public static float syncInterval = 0.02f;
    private long frame = 0;

    public override void Update()
    {
        //base.Update();
        MoveUpdate();
        JudgeMentDebuff();
        CheckIsAlive();

    }
    private void FixedUpdate()
    {
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
        SyncPlayerMsg msg = new SyncPlayerMsg();
        msg.x = transform.position.x;
        msg.z = transform.position.z;
        msg.ey = transform.eulerAngles.y;
        msg.hp = hp;
        msg.speed = speed;
        msg.frame = frame++;
        //Debug.Log("time=" + Time.time + "send=" + msg.x + ":" + msg.z + ":" + msg.frame);

        NetManager.Send(msg);
    }



    private void OnDestroy()
    {
        SyncPlayerMsg msg = new SyncPlayerMsg();
        msg.x = transform.position.x;
        msg.z = transform.position.z;
        msg.ey = transform.eulerAngles.y;
        msg.hp = 0;
        msg.speed = speed;

        NetManager.Send(msg);
    }


}
