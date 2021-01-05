using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class EquipPanel : BasePanel
{
    Animator animator;
    Button closeBtn;
    EquipManagerApi managerApi = (EquipManagerApi)ApiFactory.GetFactory(ApiType.EquipApi);
    int checkedId;
    public int selectId;
    Dictionary<int, List<PackageCell>> cells = new Dictionary<int, List<PackageCell>>();
    PageEquipModel[] equipModels = new PageEquipModel[60];

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
        Transform rows = skin.transform.Find("Content/Viewport/Content");
        for (int i = 0; i < rows.childCount; i++)
        {
            List<PackageCell> list = new List<PackageCell>();
            Transform row = rows.GetChild(i);

            for (int j = 0; j < row.childCount; j++)
            {
                Transform cell = row.GetChild(j);
                list.Add(cell.GetComponent<PackageCell>());
            }
            cells.Add(i, list);
        }
        //animator.SetBool("IsShow", true);
        checkedId = Random.Range(1, 1830);
        FlushView();

    }
    public override void OnClose()
    {
        cells.Clear();

        //关闭自动保存
        managerApi.Save();
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
    
    public void Update()
    {
        //拾取
        if (Input.GetKeyDown(KeyCode.F))
        {
            int equipId = Random.Range(1, 1830);
            Debug.Log("随机获取物品:"+ equipId);
            managerApi.Add(equipId);
            FlushView();
        }

        //检测
        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("检测物品存在:" + checkedId);
            if (managerApi.IsContain(checkedId))
            {
                PanelManger.Open<SystemTipPanel>("任务触发");
            }
        }

        //放下
        if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("放下选中物品:" + selectId);
            managerApi.Delete(selectId);
            FlushView();
        }
    }

    private void FlushView()
    {
        List<PageEquipModel> pages = managerApi.Page(0,10);
        Array.Copy(pages.ToArray(), equipModels, 60);
        List<PackageCell> packageCells = cells.Values.SelectMany(x => x).ToList();
        for (int i = 0; i < equipModels.Length; i++)
        {
            packageCells[i].FlushView(equipModels[i]);
        }
    }
}
