using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleMain : MonoBehaviour
{
    //根据map 创建人物列表
    //收到同步信息后
    public static Dictionary<string, PlayerInfo> playerDatas = new Dictionary<string, PlayerInfo>();
    //实体
    public static Dictionary<string, BasePlayer> players = new Dictionary<string, BasePlayer>();
    public static BasePlayer self;
    // Start is called before the first frame update
    void Start()
    {
        //网络socket连接初始化
        NetManager.AddMsgListener("SyncPlayerMsg", OnSynPlayer);

        NetManager.AddMsgListener("SkillMsg", OnSynSkill);
        NetManager.AddMsgListener("HitMsg", OnSynHit);
        NetManager.AddMsgListener("EndMsg", OnEnd);
        //


        //PanelManger.Init();
        foreach (var playerinfo in playerDatas.Values)
        {
            //string objName = "player_" + playerinfo.degree;

            //AddComponent
            BasePlayer player = null;
            if (playerinfo.uid == MainController.user.Uid)
            {
                PlayerController playerController = GetComponent<PlayerController>();
                player = playerController.player;
                self = player;

            }
            else
            {
                GameObject skinRes = ResManger.LoadPrefab("Battle/Player");
                GameObject gameObject1 = Instantiate<GameObject>(skinRes);
                player = gameObject1.AddComponent<SyncPlayer>();
                gameObject1.layer = 8;
                gameObject1.tag = "Player";
            }

            //属性

            player.id = playerinfo.uid;
            player.hp = playerinfo.hp;
            //player.speed = playerinfo.speed;
            //pos rotation
            Vector3 pos = new Vector3(playerinfo.x, 0, playerinfo.z);
            Vector3 rot = new Vector3(0, playerinfo.ey, 0);
            player.transform.position = pos;
            player.transform.eulerAngles = rot;
            //init
            GameObject canvas = GameObject.Find("Root/Canvas");
            GameObject hpRes = ResManger.LoadPrefab("Battle/HpHead");
            GameObject hpObj = Instantiate<GameObject>(hpRes, canvas.transform);
            HpScript hpScript = hpObj.GetComponent<HpScript>();
            hpScript.Init(player);
            //列表
            AddPlayer(playerinfo.uid, player);

        }
        //GameObject

    }

    private void OnEnd(MsgBase msgBase)
    {
        SceneManager.LoadScene("RoomScene");
    }

    private void OnSynHit(MsgBase msgBase)
    {
        HitMsg msg = (HitMsg)msgBase;
        if (msg.uid != MainController.user.Uid && players.ContainsKey(msg.uid))
        {
            //Debug.Log(msg.uid + ":" + msg.x + "," +msg.z);
            SyncPlayer basePlayer = (SyncPlayer)players[msg.uid];
            basePlayer.SyncHit(msg);

        }
    }

    void OnSynPlayer(MsgBase msgBase)
    {
        SyncPlayerMsg msg = (SyncPlayerMsg)msgBase;
        if (msg.uid != MainController.user.Uid && players.ContainsKey(msg.uid))
        {
            //Debug.Log(msg.uid + ":" + msg.x + "," +msg.z);
            
            SyncPlayer basePlayer = (SyncPlayer)players[msg.uid];
            basePlayer.SyncPos(msg);

        }

    }
    void OnSynSkill(MsgBase msgBase)
    {
        SkillMsg msg = (SkillMsg)msgBase;
        if (msg.uid != MainController.user.Uid && players.ContainsKey(msg.uid))
        {
            //Debug.Log(msg.uid + ":" + msg.x + "," +msg.z);
            SyncPlayer basePlayer = (SyncPlayer)players[msg.uid];
            basePlayer.SyncFire(msg);

        }

    }
    private void AddPlayer(string uid, BasePlayer player)
    {
        players.Add(uid, player);
    }

    // Update is called once per frame
    void Update()
    {
        NetManager.Update();
    }

    private void OnDestroy()
    {
        NetManager.RemoveMsgListener("SyncPlayerMsg", OnSynPlayer);
        NetManager.RemoveMsgListener("SkillMsg", OnSynSkill);
        NetManager.RemoveMsgListener("HitMsg", OnSynHit);
        NetManager.RemoveMsgListener("EndMsg", OnEnd);
        players.Clear();
        playerDatas.Clear();
        self = null;
    }
}
