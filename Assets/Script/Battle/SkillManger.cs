using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillManger
{
    private static SkillManger instance = null;

    Dictionary<string, Skill> skills = new Dictionary<string, Skill>();
    private SkillManger() { }

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
  /*  public Skill Get(KeyCode code)
    {
       
    }*/

}
