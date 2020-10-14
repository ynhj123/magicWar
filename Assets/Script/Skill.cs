using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public float speed = 1.5f;
    public float force = 10;
    
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 5);
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
        player.hp -= 10;
        Vector3 direct = player.transform.position - this.transform.position;
        Rigidbody rig = player.GetComponent<Rigidbody>();
        rig.velocity = direct * force;
        Destroy(gameObject);
    }
}
