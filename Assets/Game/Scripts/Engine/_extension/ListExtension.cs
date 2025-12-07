using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ListExtension
{
    /// <summary>
    /// 向左旋转列表：第一个元素移动到末尾，其他元素前移
    /// </summary>
    public static void RotateLeft<T>(this List<T> list)
    {
        if (list == null || list.Count <= 1)
            return;

        T first = list[0];
        list.RemoveAt(0);
        list.Add(first);
    }

    /// <summary>
    /// 向右旋转列表：最后一个元素移动到开头，其他元素后移
    /// </summary>
    public static void RotateRight<T>(this List<T> list)
    {
        if (list == null || list.Count <= 1)
            return;

        T last = list[list.Count - 1];
        list.RemoveAt(list.Count - 1);
        list.Insert(0, last);
    }
}