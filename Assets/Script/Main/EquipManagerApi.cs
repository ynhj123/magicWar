using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface EquipManagerApi: Api
{
    /// <summary>
    /// 初始化数据
    /// </summary>
    void Init();

    /// <summary>
    /// 保存当前用户数据
    /// </summary>
    void Save();

    /// <summary>
    /// 获取物品
    /// </summary>
    /// <param name="equipId"></param>
    void Add(int equipId);

    /// <summary>
    /// 丢弃物品
    /// </summary>
    /// <param name="equipId"></param>
    void Delete(int equipId);

    /// <summary>
    /// 检索物品
    /// </summary>
    /// <param name="equipId"></param>
    /// <returns></returns>
    bool IsContain(int equipId);

    /// <summary>
    /// 根据id获取物品详情
    /// </summary>
    /// <param name="equipId"></param>
    /// <returns></returns>
    EquipDetailModel GetDetail(int equipId);

    /// <summary>
    /// 分页查询背包
    /// </summary>
    /// <param name="curPage"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    List<PageEquipModel> Page(int curPage, int pageSize);
}
