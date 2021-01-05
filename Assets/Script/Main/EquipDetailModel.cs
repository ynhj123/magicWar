using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipDetailModel
{
    int id;
    string name;
    string uiPath;
    string des;
    char type;
    int num;

    public EquipDetailModel(EquipModel equipModel, MyEquipModel myEquipModel)
    {
        this.id = equipModel.Id;
        this.name = equipModel.Name;
        this.uiPath = equipModel.UiPath;
        this.des = equipModel.Des;
        this.type = equipModel.Type;
        this.num = myEquipModel.Num;
    }

    public int Id { get => id; set => id = value; }
    public string Name { get => name; set => name = value; }
    public string UiPath { get => uiPath; set => uiPath = value; }
    public string Des { get => des; set => des = value; }
    public char Type { get => type; set => type = value; }
    public int Num { get => num; set => num = value; }
}
