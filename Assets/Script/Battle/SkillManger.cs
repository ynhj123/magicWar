using System.Collections.Generic;
using UnityEngine;

public class SkillManger : MonoBehaviour
{
    private static SkillManger instance = null;

    Dictionary<string, Skill> skills = new Dictionary<string, Skill>();
    private SkillManger() { }

    private void Awake()
    {
        instance = this;
    }
    public static SkillManger Instance
    {

        get
        {
            if (instance == null)
            {
                instance = new SkillManger();
            }

            return instance;
        }
    }

    public void Add(string id, Skill skill)
    {
        skills.Add(id, skill);
    }
    public Skill Get(string id)
    {
        return skills[id];
    }

    internal void Handle(Transform transform, int skillId, Vector3 pos, Vector3 eulerAngles, string uid)
    {
        if (skillId == 1)
        {
            string path = "Battle/FireBall";
            GameObject bullet = Instantiate(ResManger.LoadPrefab(path), transform.position, Quaternion.identity);
            bullet.transform.up = eulerAngles;
            FireBallModel skillModel = bullet.GetComponent<FireBallModel>();
            skillModel.playerId = uid;
            return;
        }
        if (skillId == 2)
        {
            string path = "Battle/RangeFireBall";
            GameObject bullet = Instantiate(ResManger.LoadPrefab(path), new Vector3(pos.x, 5, pos.z), Quaternion.identity);
            // bullet.transform.up = forward;
            RangeFireModel skillModel = bullet.GetComponent<RangeFireModel>();

            skillModel.playerId = uid;
            return;
        }
        if (skillId == 3)
        {
            string path = "Battle/Flash";
            GameObject bullet = Instantiate(ResManger.LoadPrefab(path), new Vector3(transform.position.x, 1, transform.position.z), Quaternion.identity);
            transform.position = pos;
            return;
        }
        if (skillId == 4)
        {
            string path = "Battle/FireJet";
            GameObject bullet = Instantiate(ResManger.LoadPrefab(path), new Vector3(transform.position.x, 1, transform.position.z), Quaternion.identity);
            FireJetModel skillModel = bullet.GetComponent<FireJetModel>();
            bullet.transform.forward = eulerAngles;
            skillModel.playerId = uid;

            return;
        }
        if (skillId == 5)
        {
            string path = "Battle/Gravitation";
            GameObject bullet = Instantiate(ResManger.LoadPrefab(path), new Vector3(pos.x, 1, pos.z), Quaternion.identity);
            GravitationModel skillModel = bullet.GetComponent<GravitationModel>();
            bullet.transform.forward = eulerAngles;
            skillModel.playerId = uid;


            return;
        }
        if (skillId == 6)
        {
            string path = "Battle/MagicShield";
            GameObject bullet = Instantiate(ResManger.LoadPrefab(path), new Vector3(pos.x, 1, pos.z), Quaternion.identity);
            MagicShield skillModel = bullet.GetComponent<MagicShield>();
            bullet.transform.right = eulerAngles;
            skillModel.playerId = uid;

            return;
        }
        if (skillId == 7)
        {
            string path = "Battle/LightningTall";
            GameObject bullet = Instantiate(ResManger.LoadPrefab(path), pos, Quaternion.identity);
            LightningTallModel skillModel = bullet.GetComponent<LightningTallModel>();
            skillModel.playerId = uid;
            return;
        }
        if (skillId == 8)
        {
            string path = "Battle/MagicSphere";
            GameObject bullet = Instantiate(ResManger.LoadPrefab(path), new Vector3(pos.x, 1, pos.z), Quaternion.identity);
            MagicSphereModel skillModel = bullet.GetComponent<MagicSphereModel>();
            skillModel.playerId = uid;
            return;
        }
    }
    /*  public Skill Get(KeyCode code)
 {

 }*/

}
