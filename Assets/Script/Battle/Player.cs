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
        JudgeParticleDebuff();
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
        msg.X = transform.position.x;
        msg.Z = transform.position.z;
        msg.Ey = transform.eulerAngles.y;
        msg.Hp = hp;
        msg.Speed = speed;
        msg.Frame = frame++;
        //Debug.Log("time=" + Time.time + "send=" + msg.x + ":" + msg.z + ":" + msg.frame);

        NetManager.Send(msg);
    }



    private void OnDestroy()
    {
        SyncPlayerMsg msg = new SyncPlayerMsg();
        msg.X = transform.position.x;
        msg.Z = transform.position.z;
        msg.Ey = transform.eulerAngles.y;
        msg.Hp = 0;
        msg.Speed = speed;

        NetManager.Send(msg);
    }


}
