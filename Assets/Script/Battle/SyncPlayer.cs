using UnityEngine;

public class SyncPlayer : BasePlayer
{
    //预测信息，哪个时间到达哪个位置
    private Vector3 lastPos;
    private Vector3 lastRot;
    private Vector3 forecastPos;
    private Vector3 forecastRot;

    private float forecastTime;

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
        Debug.Log(msg.uid +":" +id+":"+msg.x + ":"+msg.z+":"+msg.hp);
        //预测位置
        Vector3 pos = new Vector3(msg.x, 0, msg.z);
        Vector3 rot = new Vector3(0, msg.ey, 0);
        /*forecastPos = pos + 1f * (pos - lastPos);
        forecastRot = rot + 1f * (rot - lastRot);*/
        forecastPos = pos;  //跟随不预测
        forecastRot = rot;

        //更新
        /*   lastPos = pos;
           lastRot = rot;*/
        // ReSetEndPoint(pos);
        hp = msg.hp;
        speed = msg.speed;
        forecastTime = Time.time;
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
        float dis = v.magnitude;
      
        Vector3 next = v.normalized * (dis / t) * Time.deltaTime;
        transform.position += next;
        //Debug.Log(forecastPos + ":" + pos + ":" + t + ":" + dis+":"+(dis / t));



        //myTransonfrom.LookAt(forecastPos);
        //旋转
        myTransonfrom.eulerAngles = forecastRot;
        // 动画
        animator.SetFloat("Speed", speed);

    }

    internal void SyncHit(HitMsg msg)
    {
        animator.Play("IsHurrt");

    }

    //开火
    public void SyncFire(SkillMsg msg)
    {
       
        /* GameObject bullet = Instantiate(ResManger.LoadPrefab(path), transform.position + transform.forward * 2 + new Vector3(0, 1, 0), Quaternion.identity);*/
        if(msg.skillId == 1)
        {
            string path = "Battle/FireBall";

            GameObject bullet = Instantiate(ResManger.LoadPrefab(path), new Vector3(msg.x, msg.y, msg.z), Quaternion.identity);
            bullet.transform.up = new Vector3(msg.ex, msg.ey, msg.ez);
            FireBallModel skillModel = bullet.GetComponent<FireBallModel>();

            skillModel.playerId = msg.uid;
            return;
        }
        if (msg.skillId == 2)
        {
            string path = "Battle/RangeFireBall";
            GameObject bullet = Instantiate(ResManger.LoadPrefab(path), new Vector3(msg.x, msg.y, msg.z), Quaternion.identity);
            // bullet.transform.up = forward;
            RangeFireModel skillModel = bullet.GetComponent<RangeFireModel>();

            skillModel.playerId = msg.uid;
            return;
        }
        if (msg.skillId == 3)
        {
            string path = "Battle/Flash"; 
            GameObject bullet = Instantiate(ResManger.LoadPrefab(path), new Vector3(transform.position.x, 1, transform.position.z), Quaternion.identity);
            Vector3 pos = new Vector3(msg.x, msg.y, msg.z);
            Vector3 eulerAngles = new Vector3(msg.ex, msg.ey, msg.ez);
            transform.position = pos;
            transform.eulerAngles = eulerAngles;


            return;
        }


    }

}
