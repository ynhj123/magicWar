using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AiController : MonoBehaviour
{
    public enum AIState
    {
        Idle,   // 待命状态
        Attack, // 进攻敌方
        Back,   // 回归原位
    
    }
    public enum TrunState
    {
        Left,
        Right

    }
    public enum AttackState
    {
        Move,
        Attack

    }
    //视野距离
    public float viewRadius = 11.0f;

    public float AttachRadius = 6.0f;
    //扇形区域
    public float viewMinAngle = 30;
    public float trunSpeed = 100;
    public LayerMask layerMask;
    [SerializeField]
    float attackInterval = 1;
    Player player;
    [SerializeField]
    AIState curState;
    [SerializeField]
    TrunState trunState;
    [SerializeField]
    AttackState attackState;
    float attchTime = 1;
    Transform targetTansform;
    Transform orginTansform;
    Vector3 orginPosition;
    Vector3 orginForward;
    
    Animator animator;


   
    // Start is called before the first frame update
    void Start()
    {
        
        player = GetComponent<Player>();
        animator = GetComponent<Animator>();
        orginPosition = new Vector3(transform.position.x,transform.position.y,transform.position.z);
        orginForward = transform.forward;
        orginTansform = transform;
        curState = AIState.Idle;
        trunState = TrunState.Left;
        attackInterval = 1;
        player.id = 1;
    }


    // Update is called once per frame
    void Update()
    {
        HandleAIStatus();
        DrawFieldOfView();
    }

    private void HandleAIStatus()
    {
        switch (curState)
        {
            case AIState.Idle:
                HandleIdle();
                break;
            case AIState.Attack:
                HandleAttack();
                break;
            case AIState.Back:
                HandleBack();
                break;
        
            default:
                break;
        }
    }

    private void HandleBack()
    {
        //回到原位
        player.ReSetEndPoint(orginPosition);
    
        if ((orginPosition - transform.position).sqrMagnitude < 0.1f)
        {
            transform.position = orginPosition;
            transform.forward = orginForward;
            curState = AIState.Idle;
            animator.SetBool("IsMove", false);
        }
    }

    private void HandleAttack()
    {
        switch (attackState)
        {
            case AttackState.Attack:
                HandleTurnAttach();
                break;
            case AttackState.Move:
                HandleAttachMove();
                break;
            default:
                break;
        }
    }

    private void HandleTurnAttach()
    {
        transform.LookAt(targetTansform.position);
       
        if (attchTime >= attackInterval)
        {
            player.FireQSkill();
            attchTime = 0;
        }
        else
        {
            attchTime += Time.fixedDeltaTime;
        }
             
    }



    private void HandleAttachMove()
    {
        player.ReSetEndPoint(targetTansform.position) ;
    }

    private void JudgeAttach()
    {
        Vector3 target = targetTansform.position;
        //在攻击范围和视野范围之间移动
        //在攻击范围攻击
    
        if ((target - transform.position).sqrMagnitude > AttachRadius * AttachRadius)
        {
           attackState  =  AttackState.Move;
           animator.SetBool("IsMove", true);

        }
        else
        {
            attackState = AttackState.Attack;
        }
    }

    void HandleIdle()
    {
        switch (trunState)
        {
            case TrunState.Left:
                Vector3 l = new Vector3(-1, 0, -1);
                HandleTurn(TrunState.Right, l);
                break;
            case TrunState.Right:
                Vector3 r = new Vector3(1, 0, 1);
                HandleTurn(TrunState.Left, r);
                break;
            default:
                break;
        }
       
    }

    private void HandleTurn(TrunState newState, Vector3 target)
    {
        //站在原地来回旋转
        float angle = Vector3.Angle(target, transform.forward);
        //如果夹角大于0.5f先旋转到小于0.5f再移动
        if (angle > 0.5f)
        {
            //计算每帧的旋转的角度，如果超过angle则为angle
            float minAngle = Mathf.Min(angle, trunSpeed * Time.deltaTime);

            //差积做法
            orginTansform.Rotate(Vector3.Cross(transform.forward, target), minAngle);
        }
        else
        {
            trunState = newState;
        }
    }


    void DrawFieldOfView()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position,viewRadius, layerMask);
      
        //检测
        Collider playerCollider = colliders.Where(collider => collider.CompareTag("Player") && Vector3.Angle(transform.forward, collider.transform.position - transform.position) < viewMinAngle).FirstOrDefault();
      
        if(playerCollider != null)
        {
            //player.ReSetEndPoint(playerCollider.transform.position);
            curState = AIState.Attack;
            targetTansform = playerCollider.transform;
            JudgeAttach();
        }
        else
        {
            if(curState == AIState.Attack)
            {
                curState = AIState.Back;
                animator.SetBool("IsMove", true);
            }
        }
    }
   
}
