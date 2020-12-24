using UnityEngine;

public class SyncPlayer : BasePlayer
{
    //预测信息，哪个时间到达哪个位置
    private Vector3 lastPos;
    private Vector3 lastRot;
    private Vector3 forecastPos;
    private Vector3 forecastRot;

    private float forecastTime;
    private long frame;

    //重写Init
    public override void Init(string skinPath)
    {
        base.Init(skinPath);
        //不受物理运动影响
        rigidBody.constraints = RigidbodyConstraints.FreezeAll;
        rigidBody.useGravity = false;
        //初始化预测信息
        lastPos = transform.position;
        lastRot = transform.eulerAngles;
        forecastPos = transform.position;
        forecastRot = transform.eulerAngles;

        forecastTime = Time.time;
    }

    public override void Update()
    {
        //base.Update();
        //更新位置
        //ForecastUpdate();
        CheckIsAlive();
        //Debug.Log(id + ":" + myTransonfrom.position);
        //MoveUpdate();
    }
    private void FixedUpdate()
    {
        ForecastUpdate();
    }

    //移动同步
    public void SyncPos(SyncPlayerMsg msg)
    {
        //预测位置
        Vector3 pos = new Vector3(msg.X, 0, msg.Z);
        Vector3 rot = new Vector3(0, msg.Ey, 0);
        /*forecastPos = pos + 1f * (pos - lastPos);
        forecastRot = rot + 1f * (rot - lastRot);*/
        forecastPos = pos;  //跟随不预测
        forecastRot = rot;

        //更新
        /*   lastPos = pos;
           lastRot = rot;*/
        // ReSetEndPoint(pos);
        hp = (float)msg.Hp;
        speed = (float)msg.Speed;
        forecastTime = Time.time;
        frame = msg.Frame;
        //Debug.Log("Time="+Time.time+";receive:Id="+msg.uid + ":" + id + ":" + msg.x + ":" + msg.z + ":" + msg.frame);

    }


    //更新位置
    public void ForecastUpdate()
    {

        //时间
        float deltaT = Time.time - forecastTime;
        if (Mathf.Abs(deltaT) < 0.01)
        {
            return;
        }
        float t = deltaT / Player.syncInterval;
        t = Mathf.Clamp(t, 0f, 1f);
        //位置
        Vector3 pos = transform.position;

        Vector3 v = forecastPos - pos;

        /*  if (Vector3.SqrMagnitude(v) > 0.2f)
          {
              float dis = v.magnitude;
              float curSpeed = dis / t;
              Vector3 next = v.normalized * (curSpeed) * Time.deltaTime;
              transform.position += next;
              Debug.Log(forecastPos + ":" + pos + ":" + t + ":" + dis + ":"+ curSpeed + ":" + deltaT+":"+speed);
              if(speed == 0)
              {
                  Debug.Log(frame);
              }
          }
          else
          {

          }*/
        transform.position = forecastPos;


        //myTransonfrom.LookAt(forecastPos);
        //旋转
        myTransonfrom.eulerAngles = forecastRot;
        // 动画
        animator.SetFloat("Speed", speed);

    }

    internal void SyncHit(HitMsg msg)
    {
        animator.Play("IsHurrt");
        finillyHurrtPlyerId = msg.TargetId;

    }

    //开火
    public void SyncFire(SkillMsg msg)
    {
        Vector3 pos = new Vector3(msg.X, msg.Y, msg.Z);
        Vector3 eulerAngles = new Vector3(msg.Ex, msg.Ey, msg.Ez);
        /* GameObject bullet = Instantiate(ResManger.LoadPrefab(path), transform.position + transform.forward * 2 + new Vector3(0, 1, 0), Quaternion.identity);*/
        SkillManger.Instance.Handle(transform, msg.SkillId, pos, eulerAngles, msg.Uid);
    }

}
