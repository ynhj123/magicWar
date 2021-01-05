using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApiFactory
{
   public static Api GetFactory(ApiType type)
    {
        if(type == ApiType.EquipApi)
        {
            return EquipManager.Instance();
        }
        else
        {
            return null;
        }
    }
}

public enum ApiType
{
    EquipApi
}
