﻿using UnityEngine;

public class GravitationModel : SkillModel
{
    public float speed = 10f;
    public float force = 1;
    public float forPlayerTime = 1f;

    public Skill skill;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 forward = transform.forward;
        transform.position += forward * Time.deltaTime * speed;
    }
    private void OnTriggerStay(Collider other)
    {
        Debug.Log("on graviation:" + this.transform.position);
        BasePlayer player;
        bool v = other.TryGetComponent<BasePlayer>(out player);
        //other.GetComponent<Player>();
        //
        if (v && playerId != player.id && player.id == MainController.user.Uid)
        {
            Vector3 direct = (this.transform.position + transform.forward * 3 - player.transform.position).normalized;

            /* HitMsg hitMsg = new HitMsg();
             hitMsg.x = direct.x;
             hitMsg.y = 0;
             hitMsg.z = direct.z;
             hitMsg.targetId = playerId;
             NetManager.Send(hitMsg);*/
            player.animator.Play("IsHurrt");
            player.hp -= 0.1f;
            direct.y = 0;
            Rigidbody rig = player.GetComponent<Rigidbody>();
            rig.velocity = direct * force;

            player.finillyHurrtPlyerId = playerId;
            player.ResetPlayer(forPlayerTime);

        }

    }

}
