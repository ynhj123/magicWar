using UnityEngine;

public class MagicSphereModel : SkillModel
{


    public Skill skill;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 1f);
    }

    private void OnDestroy()
    {
        string path = "Battle/MagicSoftExplosion";
        GameObject bullet = Instantiate(ResManger.LoadPrefab(path), transform.position, Quaternion.identity);
        MagicSoftExplosionModel skillModel = bullet.GetComponent<MagicSoftExplosionModel>();
        skillModel.playerId = playerId;
    }
}
