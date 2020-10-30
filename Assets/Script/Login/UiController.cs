using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
   
        PanelManger.Init();
        PanelManger.Open<Buttons>();
    }

 
}
