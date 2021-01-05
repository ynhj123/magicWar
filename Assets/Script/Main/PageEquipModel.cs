using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageEquipModel
{
    int id;
    string uiPath;
    char type;
    int num;

    public PageEquipModel(EquipModel equipModel, MyEquipModel myEquipModel)
    {
        this.id = equipModel.Id;
        this.uiPath = equipModel.UiPath;
        this.type = equipModel.Type;
        this.num = myEquipModel.Num;
    }

    public int Id { get => id; set => id = value; }
    public string UiPath { get => uiPath; set => uiPath = value; }
    public char Type { get => type; set => type = value; }
    public int Num { get => num; set => num = value; }
}
