using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainController : MonoBehaviour
{
    public Button JoinRoomBtn;
    // Start is called before the first frame update
    void Start()
    {
        JoinRoomBtn.onClick.AddListener(HrefRoomList);
    }

    void HrefRoomList()
    {
        SceneManager.LoadScene("RoomListScene");
    }
}
