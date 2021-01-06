using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EquipManager : EquipManagerApi
{
    public static EquipManager Instance()
    {
        return equipManager;
    }
    private static EquipManager equipManager = new EquipManager();
    private EquipManager() { }

    private static string equipFilePath = Application.streamingAssetsPath + "/equipData";
    private static string myEquipFilePath = Application.streamingAssetsPath + "/myEquipData";
    static Dictionary<int, EquipModel>     equips = new Dictionary<int, EquipModel>();
    static Dictionary<int, MyEquipModel> myEquips = new Dictionary<int, MyEquipModel>();

    public void Init()
    {
        string[] equipStrings = FileUtils.ReadLines(equipFilePath);
        equips.Clear();
        foreach (var equipStr in equipStrings)
        {
            string[] equipStrs = equipStr.Split('|');
            int id;
            char type;
            if(int.TryParse(equipStrs[0], out id) && char.TryParse(equipStrs[4], out type))
            {
                EquipModel model = new EquipModel(id, equipStrs[1], equipStrs[2], equipStrs[3], type);
                equips.Add(id, model);

            }
        }
        myEquips = FileUtils.Load<Dictionary<int, MyEquipModel>>(myEquipFilePath);
        if(myEquips == null)
        {
            myEquips = new Dictionary<int, MyEquipModel>();
        }
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
        MyEquipModel myEquipModel = myEquips[equipId];

        if (myEquipModel.Num <= 1)
        {
            myEquips.Remove(equipId);
        }
        else
        {
            myEquipModel.Num = myEquipModel.Num - 1;
        }
    }

    public bool IsContain(int equipId)
    {
        return myEquips.ContainsKey(equipId);
    }

    public EquipDetailModel GetDetail(int equipId)
    {
        if (!equips.ContainsKey(equipId))
        {
            return null;
        }
        if (!myEquips.ContainsKey(equipId))
        {
            return null;
        }
        return new EquipDetailModel(equips[equipId], myEquips[equipId]);
    }

    public Page<PageEquipModel> Page(int curPage, int pageSize)
    {
        //return (from myEquip in myEquips.Values select new PageEquipModel(equips[myEquip.Id], myEquip)).ToList();
        return Page<PageEquipModel>.build()
            .SetList(
            (from myEquip in myEquips.Values select new PageEquipModel(equips[myEquip.Id], myEquip)).Skip(curPage * pageSize).Take(pageSize).ToList()
            )
            .SetCount(myEquips.Count);
    }

    public Page<PageEquipModel> ManyPage(int curPage, int pageSize, int num)
    {
        return Page<PageEquipModel>.build()
            .SetList(
            (from myEquip in myEquips.Values select new PageEquipModel(equips[myEquip.Id], myEquip)).Skip(curPage * pageSize).Take(pageSize * num).ToList()
            )
            .SetCount(myEquips.Count);
    }
}
