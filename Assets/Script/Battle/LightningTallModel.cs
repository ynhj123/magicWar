using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningTallModel : MonoBehaviour
{
    public float force = 5;
    public float forPlayerTime = 0.5f;
    public string playerId;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
