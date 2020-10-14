using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene : MonoBehaviour
{
    // Start is called before the first frame update
 
    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();
        player.isDebuff = false;
      
    }
    private void OnTriggerExit(Collider other)
    {
        Player player = other.GetComponent<Player>();
        player.isDebuff = true;
       
    }

   
}
