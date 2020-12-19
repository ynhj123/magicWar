using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicSphereModel : MonoBehaviour
{
   
    public string playerId;

    public Skill skill;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 2f);
    }

    private void OnDestroy()
    {
        string path = "Battle/MagicSoftExplosion";
        GameObject bullet = Instantiate(ResManger.LoadPrefab(path), transform.position, Quaternion.identity);
        MagicSoftExplosionModel skillModel = bullet.GetComponent<MagicSoftExplosionModel>();
        skillModel.playerId = playerId;
    }
}
