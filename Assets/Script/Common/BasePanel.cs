using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePanel : MonoBehaviour
{
    public string skinPath;
    public GameObject skin;
    public PanelManger.Layer layer = PanelManger.Layer.Panel;
   
    public void Init()
    {
        GameObject skinPrefab = ResManger.LoadPrefab(skinPath);
        skin = Instantiate<GameObject>(skinPrefab);
    }

    public void Close()
    {
        string name = GetType().ToString();
        PanelManger.Close(name);
    }

    public virtual void OnInit() { 
    }

    public virtual void OnShow(params object[] objects)
    {
    }
    public virtual void OnClose()
    {
    }
}
