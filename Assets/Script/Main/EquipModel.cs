using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipModel
{
    int id;
    string name;
    string uiPath;
    string des;
    char type;

    public int Id { get => id; set => id = value; }
    public string Name { get => name; set => name = value; }
    public string UiPath { get => uiPath; set => uiPath = value; }
    public string Des { get => des; set => des = value; }
    public char Type { get => type; set => type = value; }
}
