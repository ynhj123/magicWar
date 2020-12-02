using UnityEngine;

public class Scene : MonoBehaviour
{
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
      
        BasePlayer[] players = other.GetComponents<BasePlayer>();
        foreach (var player in players)
        {
            player.isDebuff = false;

        }
        Debug.Log("SAFE");

    }
    private void OnTriggerExit(Collider other)
    {
        BasePlayer[] players = other.GetComponents<BasePlayer>();
        foreach (var player in players)
        {
            player.isDebuff = true;
        }
        Debug.Log("NOSAFE");

    }


}
