using UnityEngine;

public class MagicShield : SkillModel
{

    public Skill skill;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 3f);
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);
        if(other.gameObject.tag == "FlySkill")
        {
            SkillModel skill = other.gameObject.GetComponent<SkillModel>();
            if(skill.playerId != playerId)
            {
                Destroy(other.gameObject);
            }
        }
    }

}
