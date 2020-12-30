using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipPanel : BasePanel
{
    Animator animator;
    Button closeBtn;
    public override void OnInit()
    {
        skinPath = "Panel/EquipPanel";
        layer = PanelManger.Layer.Panel;
    }

    public override void OnShow(params object[] objects)
    {
        animator = skin.GetComponent<Animator>();
        closeBtn = skin.transform.Find("Close").GetComponent<Button>();
        closeBtn.onClick.AddListener(OnHide);
        animator.SetBool("IsShow", true);

    }
    public override void OnClose()
    {

    }
    public void OnHide()
    {
        Invoke("Close", GetHideTime("HideAnim"));
        animator.SetBool("IsShow", false);
    }

    private float GetHideTime(string HideAnim)
    {
        //获取动画组件中所有动画

        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        for (int i = 0; i < clips.Length; i++)
        {
            //根据动画名字  找到你要添加的动画
            if (string.Equals(clips[i].name, HideAnim))
            {
                return clips[i].length;
            }
        }

        return 0f;
    }
}
