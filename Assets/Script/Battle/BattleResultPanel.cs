using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleResultPanel : BasePanel
{
    Button quitBtn;
    public override void OnInit()
    {
        skinPath = "Panel/BattleResultPanel";
        layer = PanelManger.Layer.Panel;
    }


    public override void OnShow(params object[] objects)
    {
        PlayerInfo[] playerInfos = (PlayerInfo[])objects;
        quitBtn = skin.transform.Find("Quit").GetComponent<Button>();
        quitBtn.onClick.AddListener(Quit);
        RectTransform rectTransform = skin.transform.Find("View/RankView/Viewport/Content").GetComponent<RectTransform>();

        foreach (var playerInfo in playerInfos)
        {
            GameObject prefab = ResManger.LoadPrefab("Battle/PlayerScore");
            PlayerScoreScript playerScore = Instantiate(prefab, rectTransform).GetComponent<PlayerScoreScript>();
            playerScore.UpdateContent(playerInfo);
        }
    }

    private void Quit()
    {
        Close();
        SceneManager.LoadScene("RoomScene");
    }

    public override void OnClose()
    {
        quitBtn.onClick.RemoveListener(Quit);
    }
}
