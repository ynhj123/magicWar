using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface EquipManagerApi
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
}
