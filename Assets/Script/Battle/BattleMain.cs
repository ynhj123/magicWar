using System.Collections.Generic;
using UnityEngine;

public class BattleMain : MonoBehaviour
{
    public static Dictionary<string, PlayerInfo> players = new Dictionary<string, PlayerInfo>();
    // Start is called before the first frame update
    void Start()
    {
        //网络socket连接初始化

        //
        PanelManger.Init();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
