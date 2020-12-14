using IngameDebugConsole;
using UnityEngine;

public class UiController : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

        PanelManger.Init();
        PanelManger.Open<Buttons>();
        
    }


}
