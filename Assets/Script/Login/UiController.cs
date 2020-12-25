using UnityEngine;

public class UiController : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.Play("Children");
        PanelManger.Init();
        PanelManger.Open<Buttons>();
     
    }


}
