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
    public bool isDebuff = false;


    public Rigidbody rigidBody;
    public Animator animator;
    Transform bookTranform;
    SkinnedMeshRenderer bodyJointRener;
    ParticleSystem particleSystem;
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

    private void Awake()
    {
        myTransonfrom = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        bookTranform = transform
            .Find("ybot/mixamorig:Hips/mixamorig:Spine/mixamorig:Spine1/mixamorig:Spine2/mixamorig:LeftShoulder/mixamorig:LeftArm/mixamorig:LeftForeArm/mixamorig:LeftHand/WeaponSocket");
        bodyJointRener = transform
            .Find("ybot/Alpha_Joints").GetComponent<SkinnedMeshRenderer>();
        particleSystem = transform.Find("PowerupGlow5").GetComponent<ParticleSystem>();
        endPoints = new List<Vector3>();

    }

    

    public void UpdateJointColor(int degree)
    {
        bodyJointRener.material.color = Utils.SwitchColor(degree);
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
            particleSystem.gameObject.SetActive(true);
        }
        else
        {
            particleSystem.gameObject.SetActive(false);
        }
    }

    public void JudgeParticleDebuff()
    {
        if (isDebuff)
        {
            particleSystem.gameObject.SetActive(true);
        }
        else
        {
            particleSystem.gameObject.SetActive(false);
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
