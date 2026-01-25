using System;
using System.Collections.Generic;
using UnityEngine;

public class UITabGroup : MonoBehaviour
{
    public List<UITabItem> tabItems;
    public int defaultIndex = 0;

    private int _currentIndex = -1;
    private Action<int> _onTabChanged;

    public void Init(Action<int> onTabChanged = null)
    {
        _onTabChanged = onTabChanged;
        
        // 如果列表为空，尝试自动获取子节点中的 TabItem
        if (tabItems == null || tabItems.Count == 0)
        {
            tabItems = new List<UITabItem>(GetComponentsInChildren<UITabItem>());
        }

        for (int i = 0; i < tabItems.Count; i++)
        {
            tabItems[i].Init(i, OnTabClick);
        }

        if (tabItems.Count > 0)
        {
            SelectTab(defaultIndex);
        }
    }

    private void OnTabClick(int index)
    {
        if (_currentIndex == index) return;
        SelectTab(index);
    }

    public void SelectTab(int index)
    {
        if (index < 0 || index >= tabItems.Count) return;

        _currentIndex = index;

        for (int i = 0; i < tabItems.Count; i++)
        {
            tabItems[i].SetSelected(i == index);
        }

        _onTabChanged?.Invoke(index);
    }
    
    public int GetCurrentIndex()
    {
        return _currentIndex;
    }
}
