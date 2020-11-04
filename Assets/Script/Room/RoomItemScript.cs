using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoomItemScript : MonoBehaviour
{
    Text roomId; 
    Button joinBtn;
    Text num;


    // Start is called before the first frame update
    void Start()
    {
        roomId = transform.Find("Id").GetComponent<Text>();
        num = transform.Find("Num").GetComponent<Text>();
        joinBtn = transform.Find("Join").GetComponent<Button>();
        joinBtn.onClick.AddListener(OnJoin);
    }

    private void OnJoin()
    {
        SceneManager.LoadScene("RoomScene");
    }
}
