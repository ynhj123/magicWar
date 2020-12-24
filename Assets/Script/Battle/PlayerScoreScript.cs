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
        nickname.text = player.Nickname;
        killNum.text = player.KillNum.ToString();
        score.text = (player.Rank + 1).ToString();

    }

}
