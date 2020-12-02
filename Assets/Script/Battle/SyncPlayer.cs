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
        ForecastUpdate();
        //Debug.Log(id + ":" + myTransonfrom.position);
        //MoveUpdate();
    }

    //移动同步
    public void SyncPos(SyncPlayerMsg msg)
    {
        //Debug.Log(msg.uid +":" +id);
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
        float t = (Time.time - forecastTime) / Player.syncInterval;
        t = Mathf.Clamp(t, 0f, 1f);
        //位置
        Vector3 pos = transform.position;
        /* 
         pos = Vector3.Lerp(pos, forecastPos, t);
         transform.position = pos;*/
        Vector3 v = forecastPos - pos;
        Vector3 next = v.normalized * speed * Time.deltaTime;
        transform.position += next;
        //旋转
        myTransonfrom.eulerAngles = forecastRot;
        //myTransonfrom.LookAt(forecastPos);
        // 动画
        animator.SetFloat("Speed", speed);
       
    }

    //开火
    public void SyncFire(SkillMsg msg)
    {
        string path = "Battle/Skill";
       /* GameObject bullet = Instantiate(ResManger.LoadPrefab(path), transform.position + transform.forward * 2 + new Vector3(0, 1, 0), Quaternion.identity);*/
        GameObject bullet = Instantiate(ResManger.LoadPrefab(path), new Vector3(msg.x,msg.y,msg.z), Quaternion.identity);
        bullet.transform.up = transform.forward;
        SkillModel skillModel = bullet.GetComponent<SkillModel>();
        skillModel.playerId = msg.uid;
       
    }

}
