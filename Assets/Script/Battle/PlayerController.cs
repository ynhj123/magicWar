using UnityEngine;

public class PlayerController : MonoBehaviour
{



    public Player player;
    // Start is called before the first frame update
    void Start()
    {
        player.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            if (Input.GetMouseButtonDown(1))
            {
                player.UpdateControl();

            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                player.FireQSkill();


            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                player.FireWSkill();


            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                player.FireESkill();


            }
        }



    }
}
