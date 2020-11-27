using System.Collections.Generic;
using UnityEngine;

public class PanelManger : MonoBehaviour
{
    public enum Layer
    {
        Panel,
        Tip
    }
    private static Dictionary<Layer, Transform> layers = new Dictionary<Layer, Transform>();
    public static Dictionary<string, BasePanel> panels = new Dictionary<string, BasePanel>();
    public static Transform root;
    public static Transform canvas;

    public static void Init()
    {
        layers.Clear();
        panels.Clear();
        root = GameObject.Find("Root").transform;
        canvas = root.Find("Canvas");
        Transform panel = canvas.Find("Panel");
        Transform tip = canvas.Find("Tip");
        layers.Add(Layer.Panel, panel);
        layers.Add(Layer.Tip, tip);
    }

    public static void Open<T>(params object[] objects) where T : BasePanel
    {
        string name = typeof(T).ToString();
        if (panels.ContainsKey(name))
        {
            return;
        }
        BasePanel panel = root.gameObject.AddComponent<T>();
        panel.OnInit();
        panel.Init();
        Transform layer = layers[panel.layer];
        panel.skin.transform.SetParent(layer, false);
        panels.Add(name, panel);
        panel.OnShow(objects);
    }
    public static void Close(string name)
    {
        if (!panels.ContainsKey(name))
        {
            return;
        }
        BasePanel panel = panels[name];
        panel.OnClose();
        panels.Remove(name);
        Debug.Log("Close " + name + " " + panel);
        GameObject.Destroy(panel.skin);
        Component.Destroy(panel);
    }
    public static void Close<T>() where T : BasePanel
    {
        string name = typeof(T).ToString();
        Close(name);
    }
}
