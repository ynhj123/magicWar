using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayer : MonoBehaviour
{
    //玩家model
    private GameObject skin;
    public string id = "";
    public Transform myTransonfrom;
    List<Vector3> endPoints;
    public float speed = 5;
    //float angluarSpeed = 100;
    public float hp = 100;
    public string finillyHurrtPlyerId = "";

    public Rigidbody rigidBody;
    public Animator animator;
    Transform bookTranform;
    SkinnedMeshRenderer bodyJointRener;
    // Start is called before the first frame update
    public virtual void Init(string skinPath)
    {
        GameObject skinRes = ResManger.LoadPrefab(skinPath);
        rigidBody = gameObject.AddComponent<Rigidbody>();
        skin = Instantiate<GameObject>(skinRes);
        skin.transform.parent = transform;
        skin.transform.localPosition = Vector3.zero;
        skin.transform.localEulerAngles = Vector3.zero;

    }


    protected virtual void OnAnimatorIK(int layerIndex)
    {
        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
        animator.SetIKPosition(AvatarIKGoal.LeftHand, bookTranform.position);
    }


    public bool isDebuff = false;
    // Start is called before the first frame update
    public void Start()
    {
        myTransonfrom = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        bookTranform = transform
            .Find("ybot/mixamorig:Hips/mixamorig:Spine/mixamorig:Spine1/mixamorig:Spine2/mixamorig:LeftShoulder/mixamorig:LeftArm/mixamorig:LeftForeArm/mixamorig:LeftHand/WeaponSocket");
        endPoints = new List<Vector3>();
        bodyJointRener = transform
            .Find("ybot/Alpha_Joints").GetComponent<SkinnedMeshRenderer>();

    }

    public void UpdateJointColor(int degree)
    {
        transform
            .Find("ybot/Alpha_Joints").GetComponent<SkinnedMeshRenderer>().material.color = Utils.SwitchColor(degree);
    }
    internal void FireRSkill()
    {
        Vector3 pos = transform.position + new Vector3(0, 1, 0) + transform.forward * 2;
        string path = "Battle/FireJet";

        SkillMsg skillMsg = new SkillMsg();
        skillMsg.skillId = 4;
        skillMsg.x = pos.x;
        skillMsg.y = pos.y;
        skillMsg.z = pos.z;
        skillMsg.ex = transform.forward.x;
        skillMsg.ey = transform.forward.y;
        skillMsg.ez = transform.forward.z;
        NetManager.Send(skillMsg);

        GameObject bullet = Instantiate(ResManger.LoadPrefab(path), pos, Quaternion.identity);
        FireJetModel skillModel = bullet.GetComponent<FireJetModel>();
        bullet.transform.forward = transform.forward;
        skillModel.playerId = id;
    }

    internal void FireASkill()
    {
        Vector3 pos = transform.position + new Vector3(0, 1, 0) + transform.forward * 2;
        string path = "Battle/Gravitation";


        SkillMsg skillMsg = new SkillMsg();
        skillMsg.skillId = 5;
        skillMsg.x = pos.x;
        skillMsg.y = pos.y;
        skillMsg.z = pos.z;
        skillMsg.ex = transform.forward.x;
        skillMsg.ey = transform.forward.y;
        skillMsg.ez = transform.forward.z;
        NetManager.Send(skillMsg);

        GameObject bullet = Instantiate(ResManger.LoadPrefab(path), pos, Quaternion.identity);
        GravitationModel skillModel = bullet.GetComponent<GravitationModel>();
        bullet.transform.forward = transform.forward;
        skillModel.playerId = id;
    }

    internal void FireSSkill()
    {
        Vector3 pos = transform.position + new Vector3(0, 1, 0) + transform.forward * 1;
        string path = "Battle/MagicShield";


        SkillMsg skillMsg = new SkillMsg();
        skillMsg.skillId = 6;
        skillMsg.x = pos.x;
        skillMsg.y = pos.y;
        skillMsg.z = pos.z;
        skillMsg.ex = transform.right.x;
        skillMsg.ey = transform.right.y;
        skillMsg.ez = transform.right.z;
        NetManager.Send(skillMsg);

        GameObject bullet = Instantiate(ResManger.LoadPrefab(path), pos, Quaternion.identity);
        MagicShield skillModel = bullet.GetComponent<MagicShield>();
        bullet.transform.forward = transform.right;
        skillModel.playerId = id;

    }

    internal void FireDSkill()
    {
        //获取屏幕坐标
        Vector3 mousepostion = Input.mousePosition;
        //定义从屏幕
        Ray ray = Camera.main.ScreenPointToRay(mousepostion);
        RaycastHit hitInfo;
        if (!Physics.Raycast(ray, out hitInfo))
        {

            return;

        }
        //获取鼠标在场景中坐标
        Vector3 point = hitInfo.point;
        Vector3 pos = new Vector3(point.x, 0, point.z);
        Vector3 forward = transform.forward;
        //Skill skill = SkillManger.Instance.Get(key);

        SkillMsg skillMsg = new SkillMsg();
        skillMsg.skillId = 7;

        skillMsg.x = pos.x;
        skillMsg.y = pos.y;
        skillMsg.z = pos.z;

        NetManager.Send(skillMsg);
        string path = "Battle/LightningTall";
        GameObject bullet = Instantiate(ResManger.LoadPrefab(path), pos, Quaternion.identity);
        // bullet.transform.up = forward;
        LightningTallModel skillModel = bullet.GetComponent<LightningTallModel>();
        skillModel.playerId = id;
    }

    internal void FireFSkill()
    {
        //获取屏幕坐标
        Vector3 mousepostion = Input.mousePosition;
        //定义从屏幕
        Ray ray = Camera.main.ScreenPointToRay(mousepostion);
        RaycastHit hitInfo;
        if (!Physics.Raycast(ray, out hitInfo))
        {

            return;

        }
        //获取鼠标在场景中坐标
        Vector3 point = hitInfo.point;
        //Skill skill = SkillManger.Instance.Get(key);
        Vector3 pos = new Vector3(point.x, 1, point.z);
        string path = "Battle/MagicSphere";

        SkillMsg skillMsg = new SkillMsg();
        skillMsg.skillId = 8;
        skillMsg.x = pos.x;
        skillMsg.y = pos.y;
        skillMsg.z = pos.z;

        NetManager.Send(skillMsg);

        GameObject bullet = Instantiate(ResManger.LoadPrefab(path), pos, Quaternion.identity);
        MagicSphereModel skillModel = bullet.GetComponent<MagicSphereModel>();
        skillModel.playerId = id;
    }
    public void FireQSkill()
    {

        //Skill skill = SkillManger.Instance.Get(key);
        /*Vector3 pos = transform.position + transform.forward * 2 + new Vector3(0, 1, 0);
        

        string path = "Battle/FireBall";
        GameObject bullet = Instantiate(ResManger.LoadPrefab(path), pos, Quaternion.identity);
        bullet.transform.up = forward;
        FireBallModel skillModel = bullet.GetComponent<FireBallModel>();
        skillModel.playerId = id;*/

    }
    public void FireESkill()
    {
        //Skill skill = SkillManger.Instance.Get(key);
        endPoints.Clear();
        string path = "Battle/Flash";
        Vector3 vector3 = new Vector3(transform.position.x, 1, transform.position.z);
        GameObject bullet = Instantiate(ResManger.LoadPrefab(path), vector3, Quaternion.identity);
        Vector3 pos = transform.position + transform.forward * 5;
        Vector3 eulerAngles = transform.eulerAngles;
        SkillMsg skillMsg = new SkillMsg();
        skillMsg.x = pos.x;
        skillMsg.y = pos.y;
        skillMsg.z = pos.z;

        skillMsg.ex = eulerAngles.x;
        skillMsg.ey = eulerAngles.y;
        skillMsg.ez = eulerAngles.z;
        skillMsg.skillId = 3;
        NetManager.Send(skillMsg);
        transform.position = pos;
        transform.eulerAngles = eulerAngles;
    }
    public void FireWSkill()
    {
        //获取屏幕坐标
        Vector3 mousepostion = Input.mousePosition;
        //定义从屏幕
        Ray ray = Camera.main.ScreenPointToRay(mousepostion);
        RaycastHit hitInfo;
        if (!Physics.Raycast(ray, out hitInfo))
        {

            return;

        }
        //获取鼠标在场景中坐标
        Vector3 point = hitInfo.point;
        //Skill skill = SkillManger.Instance.Get(key);
        Vector3 pos = new Vector3(point.x, 5, point.z);
        SkillMsg skillMsg = new SkillMsg();
        skillMsg.skillId = 2;

        skillMsg.x = pos.x;
        skillMsg.y = pos.y;
        skillMsg.z = pos.z;
        Vector3 forward = transform.forward;
        skillMsg.ex = forward.x;
        skillMsg.ey = forward.y;
        skillMsg.ez = forward.z;
        NetManager.Send(skillMsg);
        string path = "Battle/RangeFireBall";
        GameObject bullet = Instantiate(ResManger.LoadPrefab(path), pos, Quaternion.identity);
        // bullet.transform.up = forward;
        RangeFireModel skillModel = bullet.GetComponent<RangeFireModel>();
        skillModel.playerId = id;

    }

    // Update is called once per frame
    public virtual void Update()
    {
    }

    public void MoveUpdate()
    {
        if (endPoints.Count > 0)
        {
            //animator.SetBool("IsMove", true);
            Vector3 v = endPoints[0] - myTransonfrom.position;
            var dot = Vector3.Dot(v, myTransonfrom.right);
            Vector3 next = v.normalized * speed * Time.deltaTime;
            float angle = Vector3.Angle(v, myTransonfrom.forward);

            if (Vector3.SqrMagnitude(v) > 0.2f)
            {
                /* if (id != MainController.user.Uid)
                 {
                    

                     Debug.Log(id + ":" + myTransonfrom.position);
                     Debug.Log(id + ":" + endPoints[0]);
                     *//*
                     Debug.Log(id + ":" + Vector3.SqrMagnitude(v));*//*
                 }*/
                if (isDebuff)
                {
                    speed = 3;
                }
                else
                {
                    speed = 5;
                }

                myTransonfrom.LookAt(endPoints[0]);
                myTransonfrom.position += next;

            }
            else
            {
                endPoints.RemoveAt(0);
            }

        }
        else
        {
            speed = 0;
            // animator.SetBool("IsMove", false);
        }
        animator.SetFloat("Speed", speed);
    }

    public bool IsAlive => hp > 0;
    public void CheckIsAlive()
    {
        if (hp <= 0)
        {
            /*HpScript hpScript;
            if(BattleMain.playerUis.TryGetValue(this.id, out hpScript))
            {
                GameObject.Destroy(hpScript);
                BattleMain.playerUis.Remove(this.id);

            }*/

            BattleMain.players.Remove(this.id);
            Destroy(this.gameObject);
        }
    }

    public void JudgeMentDebuff()
    {
        //float v = Vector3.Distance(transform.position, Vector3.zero);
        if (isDebuff)
        {
            hp -= Time.fixedDeltaTime;
        }




    }
    public void UpdateControl()
    {

        //获取屏幕坐标
        Vector3 mousepostion = Input.mousePosition;
        //定义从屏幕
        Ray ray = Camera.main.ScreenPointToRay(mousepostion);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {

            if (Input.GetKey(KeyCode.LeftShift))
            {
                AddEndPoint(hitInfo.point);
            }
            else
            {
                ReSetEndPoint(hitInfo.point);
            }

        }

    }
    void AddEndPoint(Vector3 endPoint)
    {
        endPoint.y = myTransonfrom.position.y;
        endPoints.Add(endPoint);
    }
    public void ReSetEndPoint(Vector3 endPoint)
    {

        endPoint.y = myTransonfrom.position.y;
        endPoints.Clear();
        endPoints.Add(endPoint);
    }
    public void ResetPlayer(float forTime)
    {
        endPoints.Clear();
        StartCoroutine(ResetPlayerByTime(forTime));


    }
    private IEnumerator ResetPlayerByTime(float forTime)
    {
        yield return new WaitForSeconds(forTime);
        Rigidbody rig = GetComponent<Rigidbody>();
        rig.velocity = Vector3.zero;


    }
}
