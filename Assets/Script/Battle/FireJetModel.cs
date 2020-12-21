using UnityEngine;

public class FireJetModel : SkillModel
{
    public float speed = 15f;
    public float force = 10;
    public float forPlayerTime = 1f;

    public Skill skill;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 0.4f);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 forward = transform.forward;
        transform.position += forward * Time.deltaTime * speed;
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("on firejet:" + other.gameObject.name);
        BasePlayer player;
        bool v = other.TryGetComponent<BasePlayer>(out player);
        //other.GetComponent<Player>();
        if (v && playerId != player.id && player.id == MainController.user.Uid)
        {
            Vector3 direct = (player.transform.position - this.transform.position).normalized;

            /* HitMsg hitMsg = new HitMsg();
             hitMsg.x = direct.x;
             hitMsg.y = 0;
             hitMsg.z = direct.z;
             hitMsg.targetId = playerId;
             NetManager.Send(hitMsg);*/
            player.animator.Play("IsHurrt");
            player.hp -= 10;
            direct.y = 0;
            Rigidbody rig = player.GetComponent<Rigidbody>();
            rig.velocity = direct * force;

            player.finillyHurrtPlyerId = playerId;
            player.ResetPlayer(forPlayerTime);

        }
        //
        if (v && playerId != player.id)
        {
            //显示特效
            Destroy(gameObject);
        }
    }
}
