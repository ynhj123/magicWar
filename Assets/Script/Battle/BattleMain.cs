using System.Collections.Generic;
using UnityEngine;

public class BattleMain : MonoBehaviour
{
    //根据map 创建人物列表
    //收到同步信息后
    public static Dictionary<string, PlayerInfo> playerDatas = new Dictionary<string, PlayerInfo>();
    //实体
    public static Dictionary<string, BasePlayer> players = new Dictionary<string, BasePlayer>();
    //ui
    //public static Dictionary<string, HpScript> playerUis = new Dictionary<string, HpScript>();
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


        PanelManger.Init();
        foreach (var playerinfo in playerDatas.Values)
        {
            //string objName = "player_" + playerinfo.degree;

            //AddComponent
            BasePlayer player = null;
            if (playerinfo.Uid == MainController.user.Uid)
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

            player.id = playerinfo.Uid;
            player.hp = (float)playerinfo.Hp;
            //player.speed = playerinfo.speed;
            //pos rotation
            Vector3 pos = new Vector3(playerinfo.X, 0, playerinfo.Z);
            Vector3 rot = new Vector3(0, playerinfo.Ey, 0);
            player.transform.position = pos;
            player.transform.eulerAngles = rot;
            //init
            GameObject canvas = GameObject.Find("Root/Canvas");
            GameObject hpRes = ResManger.LoadPrefab("Battle/HpHead");
            GameObject hpObj = Instantiate<GameObject>(hpRes, canvas.transform);
            HpScript hpScript = hpObj.GetComponent<HpScript>();
            hpScript.Init(player);
            //playerUis.Add(playerinfo.uid, hpScript);
            //列表
            player.UpdateJointColor(playerinfo.Degree);
            AddPlayer(playerinfo.Uid, player);

        }
        //GameObject

    }

    private void OnEnd(MsgBase msgBase)
    {
        EndMsg msg = ProtobufMapper.Deserialize<EndMsg>(msgBase.content);
        PlayerInfo[] playerInfos = new PlayerInfo[msg.Players.Count];
        msg.Players.CopyTo(playerInfos, 0);
        PanelManger.Open<BattleResultPanel>(playerInfos);
    }

    private void OnSynHit(MsgBase msgBase)
    {
        HitMsg msg = ProtobufMapper.Deserialize<HitMsg>(msgBase.content);
        if (msg.Uid != MainController.user.Uid && players.ContainsKey(msg.Uid))
        {
            //Debug.Log(msg.uid + ":" + msg.x + "," +msg.z);
            SyncPlayer basePlayer = (SyncPlayer)players[msg.Uid];
            basePlayer.SyncHit(msg);

        }
    }

    void OnSynPlayer(MsgBase msgBase)
    {
        SyncPlayerMsg msg = ProtobufMapper.Deserialize<SyncPlayerMsg>(msgBase.content);
        if (msg.Uid != MainController.user.Uid && players.ContainsKey(msg.Uid))
        {
            //Debug.Log(msg.uid + ":" + msg.x + "," +msg.z);

            SyncPlayer basePlayer = (SyncPlayer)players[msg.Uid];
            basePlayer.SyncPos(msg);

        }

    }
    void OnSynSkill(MsgBase msgBase)
    {
        SkillMsg msg = ProtobufMapper.Deserialize<SkillMsg>(msgBase.content);
        if (msg.Uid != MainController.user.Uid && players.ContainsKey(msg.Uid))
        {
            //Debug.Log(msg.uid + ":" + msg.x + "," +msg.z);
            SyncPlayer basePlayer = (SyncPlayer)players[msg.Uid];
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
