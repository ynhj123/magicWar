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

    private void Awake()
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
        kickRoomMsg.Uid = uid;
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
        icon.color = Utils.SwitchColor(degree);
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
