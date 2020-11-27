using UnityEngine;

public class SkillModel : MonoBehaviour
{
    public float speed = 5f;
    public float force = 10;
    public float forPlayerTime = 1f;
    public int playerId;


    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 1);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 forward = transform.up;
        transform.position += forward * Time.deltaTime * speed;

    }
    private void OnTriggerEnter(Collider other)
    {

        Player player = other.GetComponent<Player>();
        if (playerId != player.id)
        {

            player.hp -= 10;
            Vector3 direct = player.transform.position - this.transform.position;
            direct.y = 0;
            Rigidbody rig = player.GetComponent<Rigidbody>();
            rig.velocity = direct * force;

            player.finillyHurrtPlyerId = this.playerId;
            player.ResetPlayer(forPlayerTime);
            //显示特效
            Destroy(gameObject);
        }

    }


}
