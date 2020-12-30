using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendController : MonoBehaviour
{
    bool isShow = false;
    Animator animator;
    Button button;
    // Start is called before the first frame update
    void Start()
    {
        button = transform.Find("Btn").GetComponent<Button>();
        animator = transform.Find("FriendPanel").GetComponent<Animator>();
        button.onClick.AddListener(UpdateShow);
    }

    private void UpdateShow()
    {
        isShow = !isShow;
        animator.SetBool("IsLeft", !isShow);
    }
}
