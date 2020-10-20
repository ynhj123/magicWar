using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject prefabBullet;
    public Player player;
    // Start is called before the first frame update
    void Start()
    {
        
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

                GameObject bullet = Instantiate(prefabBullet, player.transform.position + player.transform.forward * 2, Quaternion.identity);
                bullet.transform.up = player.transform.forward;
                Skill skill = bullet.GetComponent<Skill>();
                skill.playerId = player.id;

            }
        }
        
    }
}
