using UnityEngine;
using UnityEngine.UI;

public class RoomItemScript : MonoBehaviour
{
    Text roomId;
    Button joinBtn;
    Text num;
    Image image;
    CanvasGroup canvasGroup;


    // Start is called before the first frame update
    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        roomId = transform.Find("Id/Id").GetComponent<Text>();
        num = transform.Find("Num").GetComponent<Text>();
        joinBtn = transform.Find("Join").GetComponent<Button>();
        image = GetComponent<Image>();
        joinBtn.onClick.AddListener(OnJoin);

    }
    public void UpdateContent(string newRoomId, string newNum, int status)
    {
        roomId.text = newRoomId;
        num.text = newNum;
        if (status == 1)
        {
            image.color = Color.red;
        }
        else
        {
            image.color = Color.white;
        }
    }
    public void Hide()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
    public void Show()
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }
    void OnJoin()
    {
        //SceneManager.LoadScene("RoomScene");
        EnterRoomMsg msg = new EnterRoomMsg();
        msg.RoomId = roomId.text;
        NetManager.Send(msg);
    }

}
