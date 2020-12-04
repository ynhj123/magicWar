using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadAsyncScene : MonoBehaviour
{
    //显示进度的文本
    private Text progress;
    //进度条的数值
    private float progressValue;
    //进度条
    private Slider slider;
    [Tooltip("下个场景的名字")]
    public string nextSceneName;

    private AsyncOperation async = null;

    private void Start()
    {
        progress = transform.Find("Canvas/Slider/Text").GetComponent<Text>();
        slider = transform.Find("Canvas/Slider").GetComponent<Slider>();
        NetManager.AddMsgListener("LoadFinishMsg", OnLoadFinish);

        StartCoroutine("LoadScene");
    }

    private void OnDestroy()
    {
        NetManager.RemoveMsgListener("LoadFinishMsg", OnLoadFinish);
        async = null;
    }
    private void OnLoadFinish(MsgBase msgBase)
    {
        Debug.Log("OnLoadFinish:"+nextSceneName);
        LoadFinishMsg msg = (LoadFinishMsg)msgBase;
        BattleMain.playerDatas.Clear();
        PlayerInfo[] players = msg.players;
        for (int i = 0; i < players.Length; i++)
        {

            PlayerInfo playerInfo = players[i];
            BattleMain.playerDatas.Add(playerInfo.uid, playerInfo);

        }
        async.allowSceneActivation = true;
    }
    private void Update()
    {
        NetManager.Update();
    }

    IEnumerator LoadScene()
    {
        Debug.Log("load Scene");
        async = SceneManager.LoadSceneAsync(nextSceneName);
        async.allowSceneActivation = false;
        while (!async.isDone)
        {
            if (async.progress < 0.9f)
                progressValue = async.progress;
            else
                progressValue = 1.0f;

            slider.value = progressValue;
            progress.text = (int)(slider.value * 100) + " %";

            if (progressValue >= 0.9)
            {
                //progress.text = "按任意键继续";
                //获取用户信息
                //获取完成后发送发成信息
                //先拉取技能列表
                //当所有玩家响应后开启游戏
                LoadFinishMsg loadFinishMsg = new LoadFinishMsg();
                NetManager.Send(loadFinishMsg);
                // StopCoroutine("LoadScene");
                yield break;
                //if (Input.anyKeyDown)
                //{
                //    async.allowSceneActivation = true;
                //}
            }

            yield return null;
        }

    }

}
