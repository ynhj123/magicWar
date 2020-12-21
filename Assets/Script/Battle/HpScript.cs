using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpScript : MonoBehaviour
{
    private Image image;
    private RectTransform rectTrans;
    private Text nickName;
    private Text hpValue;
    public Transform target;
    public Vector2 offset;

    public float maxValue;
    BasePlayer player;
    // Start is called before the first frame update
    void Awake()
    {
        image = transform.Find("HpImg").GetComponent<Image>();
        rectTrans = GetComponent<RectTransform>();
        hpValue = transform.Find("HpVal").GetComponent<Text>();
        nickName = transform.Find("NickName").GetComponent<Text>();

    }
    public void Init(BasePlayer initPlayer)
    {
        player = initPlayer;
        maxValue = player.hp;
        Dictionary<string, PlayerInfo> playerDatas = BattleMain.playerDatas;
        nickName = transform.GetChild(1).GetComponent<Text>();


        if (playerDatas.ContainsKey(player.id))
        {
            PlayerInfo playerInfo;
            if (playerDatas.TryGetValue(player.id, out playerInfo))
            {
                Debug.Log(playerInfo.uid + "," + playerInfo.nickname);
                nickName.text = playerInfo.nickname;


            }
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            Vector3 tarPos = player.transform.position;
            Vector2 pos = RectTransformUtility.WorldToScreenPoint(Camera.main, tarPos);
            rectTrans.position = pos + offset;
            image.fillAmount = player.hp / maxValue;
            hpValue.text = Convert.ToInt32(player.hp).ToString();
            if (player.hp == 0)
            {
                Destroy(this.gameObject);
            }
        }


    }
}
