using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoomMain : MonoBehaviour
{
    Text roomId;
    Button startGame;
    Button quit;
    // Start is called before the first frame update
    void Start()
    {
        roomId = transform.Find("Header/RoomId").GetComponent<Text>();
        startGame = transform.Find("Right/Bottom/Start").GetComponent<Button>();
        quit = transform.Find("Right/Bottom/Quit").GetComponent<Button>();
        startGame.onClick.AddListener(StartGame);
        quit.onClick.AddListener(OnQuit);
    }

    private void OnQuit()
    {
        SceneManager.LoadScene("RoomListScene");
    }

    private void StartGame()
    {
        SceneManager.LoadScene("BattleScene");
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
