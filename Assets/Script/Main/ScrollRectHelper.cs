using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollRectHelper : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private float smooting;                          //滑动速度
    private float normalSpeed = 5;
    private float highSpeed = 10;

    /// <summary>
    /// 每页显示的项目(Btn_1)
    /// </summary>
    private int pageCount = 1;

    /// <summary>
    /// content
    /// </summary>
    public GameObject Content;

    private ScrollRect sRect;

    /// <summary>
    ///  总页数
    /// </summary>
    private float pageIndex;

    /// <summary>
    /// 是否拖拽结束
    /// </summary>
    private bool isDrag = true;

    /// <summary>
    /// 总页数索引比列 0-1
    /// </summary>
    private List<float> listPageValue = new List<float> { 0 };  //

    /// <summary>
    /// 滑动的目标位置
    /// </summary>
    private float m_targetPos = 0;

    /// <summary>
    /// 初始位置索引
    /// </summary>
    private float beginIndex = 9f;

    /// <summary>
    /// 当前位置索引
    /// </summary>
    private float nowindex = 0;

    /// <summary>
    /// 开始索引的位置
    /// </summary>
    private float beginDragPos;

    /// <summary>
    /// 结束索引的位置
    /// </summary>
    private float endDragPos;

    /// <summary>
    /// 灵敏度
    /// </summary>
    private float sensitivity = 0f;

    /// <summary>
    /// 每页的数量
    /// </summary>
    private int onePageCount = 6;

    void Awake()
    {
        //获取组件
        sRect = this.GetComponent<ScrollRect>();
        //
        ListPageValueInit();
        //滑度
        smooting = normalSpeed;
    }

    //每页比例
    void ListPageValueInit()
    {
        Debug.Log(Content.transform.childCount);
        //总页数       总页数/每页显示的项目item（1） 
        pageIndex = (Content.transform.childCount / pageCount);

        if (Content != null && Content.transform.childCount != 0)
        {
            for (float i = 1; i <= pageIndex; i++)
            {
                listPageValue.Add((i / pageIndex));//设置(写出)页数
            }
        }
    }
    void Update()
    {
        if (!isDrag)
        {
            sRect.verticalNormalizedPosition = Mathf.Lerp(sRect.verticalNormalizedPosition, m_targetPos, Time.deltaTime * smooting);
        }
    }

    /// <summary>
    /// 拖动开始 接口
    /// </summary>
    public void OnBeginDrag(PointerEventData eventData)
    {
;       isDrag = true;
        //记录拖拽的起点 
        beginDragPos = sRect.verticalNormalizedPosition;
        beginIndex = nowindex;
        //Debug.Log("begin_index:"+ nowindex);
    }

    /// <summary>
    /// 拖拽结束
    /// </summary>
    public void OnEndDrag(PointerEventData eventData)
    {
        isDrag = false;
        //记录放开时的点
        endDragPos = sRect.verticalNormalizedPosition;

        //判断   终点》起点？                            是：终点+灵敏度(换页)        否：终点-灵敏度（不换页）
        endDragPos = endDragPos > (beginDragPos) ? endDragPos + sensitivity : endDragPos - sensitivity;
        int index = 0;

        // 拖动的绝对值   数值决定翻几页
        float offset = Mathf.Abs(listPageValue[index] - endDragPos);
        //遍历全页数
        for (int i = 1; i < listPageValue.Count; i++)
        {
            int a = (listPageValue.Count - i);
            //返回绝对值  例如   1.4-1 = 0.43 、 1.4 - 2= 0.6  、 1.4 -3 = 1.6  、 1.4-4 = 3.6f、4.6
            float temp = Mathf.Abs(endDragPos - listPageValue[i]);
            if (temp < offset)
            {
                index = i;
                offset = temp;
            }
        }
        m_targetPos = listPageValue[index];
        nowindex = index;
        //Debug.Log("targetpos:" + m_targetPos + "-index:" + nowindex + "-offset:"+ offset);
        //Debug.Log("a=="+sRect.verticalNormalizedPosition);
        if (nowindex < 5)
        {
            EquipPanel panel = PanelManger.Get<EquipPanel>();
            if (panel.NextData())
            {
                m_targetPos = 0.5f;
                sRect.verticalNormalizedPosition = 1 - (5 - nowindex) * 0.1f;
                nowindex = 5;
            }
            else
            {
                if (endDragPos < beginDragPos)
                {
                    nowindex = 0;
                    m_targetPos = 0f;
                }
                else
                {
                    nowindex = 5;
                    m_targetPos = 0.5f;
                }
                    
            }
        }
        else if (nowindex > 5)
        {        
            EquipPanel panel = PanelManger.Get<EquipPanel>();
            if (panel.LastData())
            {
                m_targetPos = 0.5f;
                sRect.verticalNormalizedPosition = 0 + (nowindex - 5) * 0.1f;
                nowindex = 5;
            }
            else 
            {
                if (endDragPos > beginDragPos)
                {
                    nowindex = 10;
                    m_targetPos = 1f;
                }
                else
                {
                    nowindex = 5;
                    m_targetPos = 0.5f;
                }                  
            }
        }

    }//horizontalNormalizedPosition 0-1 0表示左侧           vertical 0-1 0表示底部

    public void OnDrag(PointerEventData eventData)
    {
        float distance = sRect.verticalNormalizedPosition - beginDragPos;
        float direct = distance > 0 ? 1 : -1;
        distance = Mathf.Clamp(Mathf.Abs(distance), 0, 0.5f);
        sRect.verticalNormalizedPosition = beginDragPos + distance * direct;
    }
}
