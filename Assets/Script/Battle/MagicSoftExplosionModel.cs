using UnityEngine;

public class MagicSoftExplosionModel : SkillModel
{
    public float force = 10;
    public float forPlayerTime = 1f;
    public Skill skill;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 0.5f);
    }

    // Update is called once per frame

    private void OnTriggerEnter(Collider other)
    {
        BasePlayer player;
        bool v = other.TryGetComponent<BasePlayer>(out player);
        //other.GetComponent<Player>();
        if (v && playerId != player.id && player.id == MainController.user.Uid)
        {
            Vector3 direct = (player.transform.position - this.transform.position).normalized;
            HitMsg hitMsg = new HitMsg();
            hitMsg.x = direct.x;
            hitMsg.y = 0;
            hitMsg.z = direct.z;
            hitMsg.targetId = playerId;
            NetManager.Send(hitMsg);
            player.animator.Play("IsHurrt");
            player.hp -= 10;
            direct.y = 0;
            Rigidbody rig = player.GetComponent<Rigidbody>();
            rig.velocity = direct * force;

            player.finillyHurrtPlyerId = playerId;
            player.ResetPlayer(forPlayerTime);

        }
      

    }
}
