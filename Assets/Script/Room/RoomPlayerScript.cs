using UnityEngine;
using UnityEngine.UI;

public class RoomPlayerScript : MonoBehaviour
{
    string uid;

    Image icon;
    Text nickname;
    Button kick;
    Text ready;
    Text own;
    // Start is called before the first frame update
    private void Start()
    {
        Init();
    }
    public void Init()
    {

        nickname = transform.Find("background/Name").GetComponent<Text>();
        icon = transform.Find("background/Icon").GetComponent<Image>();
        kick = transform.Find("background/Kick").GetComponent<Button>();
        ready = transform.Find("background/Ready").GetComponent<Text>();
        own = transform.Find("background/Own").GetComponent<Text>();
        kick.onClick.AddListener(Onkick);
    }

    private void Onkick()
    {
        KickRoomMsg kickRoomMsg = new KickRoomMsg();
        kickRoomMsg.uid = uid;
        NetManager.Send(kickRoomMsg);
    }

    public void FlushView(int degree, string nickName, int status, string uid)
    {
        this.uid = uid;
        ShowAll();
        SwitchDegreeColor(degree);
        nickname.text = nickName;
        if (degree == 0)
        {
            ShowOwn();
            HideReady();

        }
        else
        {
            HideOwn();
            if (status == 1)
            {
                ShowReady();
            }
            else
            {
                HideReady();
            }
        }
    }

    private void SwitchDegreeColor(int degree)
    {
        switch (degree)
        {
            case 0:
                icon.color = Color.red;
                break;
            case 1:
                icon.color = Color.green;
                break;
            case 2:
                icon.color = Color.yellow;
                break;
            case 3:
                icon.color = Color.blue;
                break;
            case 4:
                icon.color = Color.gray;
                break;
            case 5:
                icon.color = Color.grey;
                break;
            case 6:
                icon.color = Color.cyan;
                break;
            case 7:
                icon.color = Color.black;
                break;
        }
    }


    void ShowAll()
    {
        ShowIcon();
        ShowName();
    }
    public void HideAll()
    {
        HideIcon();
        HideName();
        HideOwn();
        HideReady();
        HideKick();

    }
    void ShowName()
    {
        Color n = nickname.color;
        n.a = 1f;
        nickname.color = n;
    }
    void HideName()
    {
        Color n = nickname.color;
        n.a = 0f;
        nickname.color = n;
    }
    void ShowIcon()
    {
        Color i = icon.color;
        i.a = 1f;
        icon.color = i;
    }
    void HideIcon()
    {
        Color i = icon.color;
        i.a = 0f;
        icon.color = i;
    }
    void ShowReady()
    {
        Color c = ready.color;
        c.a = 1f;
        ready.color = c;
    }
    void HideReady()
    {
        Color c = ready.color;
        c.a = 0f;
        ready.color = c;
    }
    void ShowOwn()
    {
        Color o = own.color;
        o.a = 1f;
        own.color = o;

    }
    void HideOwn()
    {
        Color o = own.color;
        o.a = 0f;
        own.color = o;
    }
    public void ShowKick()
    {
        CanvasGroup canvasGroup = kick.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }
    public void HideKick()
    {
        CanvasGroup canvasGroup = kick.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
    private void OnDestroy()
    {
        kick.onClick.RemoveAllListeners(); ;
    }
}
