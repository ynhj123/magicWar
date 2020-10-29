using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class SkillManger 
{
    private static SkillManger instance = null;

    Dictionary<KeyCode, Skill> skills = new Dictionary<KeyCode, Skill>();
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

    public void Add(KeyCode key, Skill  skill)
    {
        skills.Add(key, skill);
    }
    public Skill Get(KeyCode key)
    {
        return skills[key];
    }

}
