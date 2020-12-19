using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireJetModel : MonoBehaviour
{
    public float speed = 15f;
    public float force = 10;
    public float forPlayerTime = 1f;
    public string playerId;

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
}
