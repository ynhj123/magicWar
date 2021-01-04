using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipManager : EquipManagerApi
{
    public static EquipManager Instance()
    {
        return equipManager;
    }
    private static EquipManager equipManager = new EquipManager();
    private EquipManager() { }

    private static string equipFilePath = "";
    private static string myEquipFilePath = "";
    static Dictionary<int, EquipModel> equips = new Dictionary<int, EquipModel>();
    static Dictionary<int, MyEquipModel> myEquips = new Dictionary<int, MyEquipModel>();

    public void Init()
    {
        equips   = FileUtils.Load<Dictionary<int, EquipModel>>(equipFilePath);
        myEquips = FileUtils.Load<Dictionary<int, MyEquipModel>>(myEquipFilePath);
    }

    public void Save()
    {
        FileUtils.Save(myEquipFilePath, myEquips);
    }

    public void Add(int equipId)
    {
        if (myEquips.ContainsKey(equipId))
        {
            MyEquipModel myEquipModel =  myEquips[equipId];
            myEquipModel.Num = myEquipModel.Num + 1;
        }
        else
        {
            MyEquipModel myEquipModel = new MyEquipModel();
            myEquipModel.Id = equipId;
            myEquipModel.Num = 1;
            myEquips.Add(equipId, myEquipModel);
        }
        
    }

    public void Delete(int equipId)
    {
        if (!myEquips.ContainsKey(equipId))
        {
            return;
        }    
        else
        {
            MyEquipModel myEquipModel = myEquips[equipId];
            myEquipModel.Num = myEquipModel.Num - 1;

            if(myEquipModel.Num <= 0)
            {
                myEquips.Remove(equipId);
            }
        }
    }

    public bool IsContain(int equipId)
    {
        return myEquips.ContainsKey(equipId);
    }

}
