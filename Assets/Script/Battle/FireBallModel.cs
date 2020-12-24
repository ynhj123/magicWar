using UnityEngine;

public class FireBallModel : SkillModel
{
    public float speed = 5f;
    public float force = 10;
    public float forPlayerTime = 1f;

    public Skill skill;


    // Start is called before the first frame update
    void Start()
    {

        Destroy(this.gameObject, 2);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 forward = transform.up;
        transform.position += forward * Time.deltaTime * speed;

    }
    private void OnTriggerEnter(Collider other)
    {
        BasePlayer player;
        bool v = other.TryGetComponent<BasePlayer>(out player);
        //other.GetComponent<Player>();
        if (v && playerId != player.id && player.id == MainController.user.Uid)
        {
            Vector3 direct = player.transform.position - this.transform.position;
            HitMsg hitMsg = new HitMsg();
            hitMsg.X = direct.x;
            hitMsg.Y = 0;
            hitMsg.Z = direct.z;
            hitMsg.TargetId = playerId;
            NetManager.Send(hitMsg);
            player.animator.Play("IsHurrt");
            player.hp -= 10;
            direct.y = 0;
            Rigidbody rig = player.GetComponent<Rigidbody>();
            rig.velocity = direct * force;

            player.finillyHurrtPlyerId = playerId;
            player.ResetPlayer(forPlayerTime);

        }
        if (v && playerId != player.id)
        {
            //显示特效
            Destroy(gameObject);
        }

    }


}
