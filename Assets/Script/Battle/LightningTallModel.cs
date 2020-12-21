using System.Collections.Generic;
using UnityEngine;

public class LightningTallModel : SkillModel
{
    public float force = 5;
    public float forPlayerTime = 0.5f;
    Dictionary<string,BasePlayer> hurrtPlayers = new Dictionary<string, BasePlayer>();
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
            Vector3 direct = player.transform.position - this.transform.position;
            HitMsg hitMsg = new HitMsg();
            hitMsg.x = direct.x;
            hitMsg.y = 0;
            hitMsg.z = direct.z;
            hitMsg.targetId = playerId;
            NetManager.Send(hitMsg);
            player.animator.Play("IsHurrt");
            player.hp -= 5;
            direct.y = 0;
            /*  Rigidbody rig = player.GetComponent<Rigidbody>();
              rig.velocity = direct * force;*/
            player.ResetPlayer(forPlayerTime);
            player.isDebuff = true;
            player.finillyHurrtPlyerId = playerId;
            hurrtPlayers.Add(player.id, player);
        }


    }
    private void OnDestroy()
    {
        foreach (var item in hurrtPlayers)
        {
            item.Value.isDebuff = false;
        }
    }

}
