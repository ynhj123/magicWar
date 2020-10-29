using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill {
    //法术接口：编号，名称，动画，施法时间，冷却时间，耗蓝，伤害，图标，施法距离，作用范围，法术属性，描述；
    int id;
    string skinPath;
    string name;
    string animationBeforeCastingModelPath;
    float beforeCastTime;
    string animationCastingModelPath;
    float castTime;
    float cd;
    float speed;
    float force;
    float forPlayerTime;
    string iconPath;
    float castingDistance;
    float castingRange;
    int type;
    string des;

    public int Id { get => id; set => id = value; }
    public string Name { get => name; set => name = value; }
    public string AnimationBeforeCastingModelPath { get => animationBeforeCastingModelPath; set => animationBeforeCastingModelPath = value; }
    public float BeforeCastTime { get => beforeCastTime; set => beforeCastTime = value; }
    public string AnimationCastingModelPath { get => animationCastingModelPath; set => animationCastingModelPath = value; }
    public float CastTime { get => castTime; set => castTime = value; }
    public float Cd { get => cd; set => cd = value; }
    public float Speed { get => speed; set => speed = value; }
    public float Force { get => force; set => force = value; }
    public float ForPlayerTime { get => forPlayerTime; set => forPlayerTime = value; }
    public string IconPath { get => iconPath; set => iconPath = value; }
    public float CastingDistance { get => castingDistance; set => castingDistance = value; }
    public float CastingRange { get => castingRange; set => castingRange = value; }
    public int Type { get => type; set => type = value; }
    public string Des { get => des; set => des = value; }


    public string SkinPath { get => skinPath; set => skinPath = value; }
}
