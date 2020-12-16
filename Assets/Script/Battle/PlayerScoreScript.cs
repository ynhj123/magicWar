using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScoreScript : MonoBehaviour
{
    Image icon;
    Text nickname;
    Text killNum;
    Text score;

    public void UpdateContent(PlayerInfo player)
    {
        icon = this.transform.Find("Icon").GetComponent<Image>();
        nickname = this.transform.Find("NickName").GetComponent<Text>();
        killNum = this.transform.Find("KillNum").GetComponent<Text>();
        score = this.transform.Find("Score").GetComponent<Text>();
        icon.sprite = ResManger.LoadSprite("Ui/mainIcon");
        nickname.text = player.nickname;
        killNum.text = player.killNum.ToString();
        score.text = (player.rank+1).ToString();
        
    }
    
}
