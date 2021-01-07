using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
/// <summary>
/// 首次进入加载60个 ，不足按60个算
/// 
/// </summary>
public class EquipPanel : BasePanel
{
    Animator animator;
    Button closeBtn;
    EquipManagerApi managerApi = (EquipManagerApi)ApiFactory.GetFactory(ApiType.EquipApi);
    int checkedId;
    PackageCell selectEquip;
    Dictionary<int, List<PackageCell>> cells = new Dictionary<int, List<PackageCell>>();
    PageEquipModel[] equipModels = new PageEquipModel[60];

    /// <summary>
    /// 当前页
    /// </summary>
    int curPage = 0;

    /// <summary>
    /// 一页多少个
    /// </summary>
    int pageSize = 12;

    /// <summary>
    /// 总数
    /// </summary>
    int maxCount = 0;
    /// <summary>
    /// 当前页显示多少个分页数据
    /// </summary>
    int PageNum = 5;

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
        CurData();

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
            CurData();
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
            Debug.Log("放下选中物品:" + selectEquip.equip.Id);
            managerApi.Delete(selectEquip.equip.Id);
            CurData();
        }
    }
    //当前页
    private void CurData()
    {
        Page<PageEquipModel> pages = managerApi.ManyPage(curPage, pageSize, PageNum);
        maxCount = pages.Count;
        PageEquipModel[] newModels = new PageEquipModel[60];
        Array.Copy(pages.List.ToArray(), newModels, Math.Min(pages.List.Count, 60));
        equipModels = newModels;
        FlushView();
    }
    //下一页
    public bool NextData()
    {
        //Debug.Log("allCount=" + maxCount);
        if (maxCount < pageSize * (curPage + PageNum))
        {
            return false;
        }
        curPage += 1;
        Page<PageEquipModel> pages = managerApi.Page((curPage - 1) + PageNum, pageSize);
        maxCount = pages.Count;
        PageEquipModel[] newModels = new PageEquipModel[60];
        Array.Copy(equipModels, pageSize, newModels, 0, 48);
        Array.Copy(pages.List.ToArray(), 0, newModels, 48, Math.Min(pages.List.Count, pageSize));
        equipModels = newModels;
        FlushView();     
        return true;
    }
    //上一页
    public bool LastData()
    {
        if(curPage < 1)
        {
            return false;
        }
        curPage -= 1;
        Page<PageEquipModel> pages = managerApi.Page(curPage, pageSize);
        maxCount = pages.Count;
        PageEquipModel[] newModels = new PageEquipModel[60];
        Array.Copy(equipModels, 0, newModels, pageSize, 48);
        Array.Copy(pages.List.ToArray(), 0, newModels, 0, pageSize);
        equipModels = newModels;
        FlushView();
        return true;
    }

    private void FlushView()
    {
        if (selectEquip != null)
        {
            selectEquip.CancelSelected();
            selectEquip = null;
        }
        List<PackageCell> packageCells = cells.Values.SelectMany(x => x).ToList();
        for (int i = 0; i < equipModels.Length; i++)
        {
            packageCells[i].FlushView(equipModels[i]);
        }
    }

    //选中物品
    public void selectedEquip(PackageCell cell)
    {
        if(selectEquip != null)
        {
            selectEquip.CancelSelected();
        }
        Debug.Log("选中物品:" + cell.equip.Id);
        selectEquip = cell;
    }
}
