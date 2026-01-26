using System;
using System.Collections;
using System.Collections.Generic;
using Engine;
using RbEngine;
using SuperScrollView;
using UnityEngine;
using UnityEngine.UI;

public class UIBag : UIBase
{
    public Button closeBtn;

    public UITabGroup tabGroup;

    public LoopGridView bagGridView;

    private List<BagData.ItemInfo> _showList = new List<BagData.ItemInfo>();

    public override void OnCreate(params object[] args)
    {
        closeBtn.AddOnPointerClick(OnBtnClick);

        bagGridView.InitGridView(0, OnGetItemByRowColumn);

        tabGroup.Init(OnTabClick);

        // 默认显示全部
        OnTabClick(0);
    }

    private void OnTabClick(int index)
    {
        _showList.Clear();
        List<BagData.ItemInfo> allItems = BagData.Instance.GetAllItems();

        if (index == 0)
        {
            //显示所有物品
            _showList.AddRange(allItems);
        }
        else
        {
            //显示指定类型的物品 (index 对应 Excel 中的 Type)
            foreach (var item in allItems)
            {
                if (Excel.GetItemType(item.Id) == index)
                {
                    _showList.Add(item);
                }
            }
        }

        // 排序：按ID从小到大
        _showList.Sort((a, b) => a.Id.CompareTo(b.Id));

        bagGridView.SetListItemCount(_showList.Count);
        bagGridView.RefreshAllShownItem();
    }

    private LoopGridViewItem OnGetItemByRowColumn(LoopGridView gridView, int itemIndex, int row, int column)
    {
        if (itemIndex < 0 || itemIndex >= _showList.Count)
        {
            return null;
        }

        LoopGridViewItem item = gridView.NewListViewItem("UIBagItem");
        UIBagItem itemScript = item.GetComponent<UIBagItem>();

        if (!itemScript.isInit)
        {
            itemScript.Init(bagGridView.gameObject, OnTouchItem);
        }
        itemScript.SetIndex(itemIndex);
        itemScript.FreshItem(_showList[itemIndex].Id);

        return item;
    }

    private void OnTouchItem(int index)
    {
        Debug.Log("OnTouchItem : " + index);
        SceneUIManager.Instance.OpenPersistentUI(UIConfig.BagInfo, _showList[index].Id);
    }

    private void OnBtnClick(Button btn)
    {
        if (btn == closeBtn)
        {
            Close();
        }
    }
}
