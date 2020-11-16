﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayer : MonoBehaviour
{
    //玩家model
    private GameObject skin;
    public int id = 0;
    Transform myTransonfrom;
    List<Vector3> endPoints;
    float speed = 5;
    //float angluarSpeed = 100;
    public double hp = 100;
    public int finillyHurrtPlyerId = -1;
    public Rigidbody rigidBody;
    public Animator animator;
    public Transform bookTranform;
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
        
        endPoints = new List<Vector3>();

    }

     public void FireQSkill()
    {
        //Skill skill = SkillManger.Instance.Get(key);
        string path = "Battle/Skill";
        GameObject bullet = Instantiate(ResManger.LoadPrefab(path), transform.position + transform.forward * 2 + new Vector3(0,1,0), Quaternion.identity);
        bullet.transform.up = transform.forward;
        SkillModel skillModel = bullet.GetComponent<SkillModel>();
        skillModel.playerId = id;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }

    public void MoveUpdate()
    {
        animator.SetFloat("Speed", speed);
        if (endPoints.Count > 0)
        {
            //animator.SetBool("IsMove", true);
            Vector3 v = endPoints[0] - myTransonfrom.position;
            var dot = Vector3.Dot(v, myTransonfrom.right);
            Vector3 next = v.normalized * speed * Time.deltaTime;
            float angle = Vector3.Angle(v, myTransonfrom.forward);
            if (Vector3.SqrMagnitude(v) > 0.01f)
            {
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
            animator.SetFloat("Speed", 0);
            // animator.SetBool("IsMove", false);
        }
    }

    public bool IsAlive => hp > 0;
    public void CheckIsAlive()
    {
        if (hp <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void JudgeMentDebuff()
    {
        //float v = Vector3.Distance(transform.position, Vector3.zero);
        if (isDebuff)
        {
            hp -= Time.fixedDeltaTime;
            speed = 3;
        }
        else
        {
            speed = 5;
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
        StartCoroutine(ResetPlayerByTime(forTime));


    }
    private IEnumerator ResetPlayerByTime(float forTime)
    {
        yield return new WaitForSeconds(forTime);
        Rigidbody rig = GetComponent<Rigidbody>();
        rig.velocity = Vector3.zero;


    }
}
