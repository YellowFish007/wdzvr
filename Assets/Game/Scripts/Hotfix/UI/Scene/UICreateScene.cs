using System.Collections;
using System.Collections.Generic;
using Engine;
using SuperScrollView;
using UnityEngine;

public class UICreateScene : UIBase
{
    public override string Name => "UICreateScene";

    public LoopListView2 sceneListView;

    public override void OnOpen()
    {
        sceneListView.InitListView(0, OnGetItemByIndex);
        sceneListView.SetListItemCount(Excel.GetSceneDataList().Count);
        sceneListView.RefreshAllShownItem();
    }

    LoopListViewItem2 OnGetItemByIndex(LoopListView2 listView, int index)
    {
        LoopListViewItem2 item = listView.NewListViewItem("UICreateSceneItem");

        UICreateSceneItem itemScript = item.GetComponent<UICreateSceneItem>();
        if (!itemScript.isInit)
        {
            itemScript.Init(sceneListView.gameObject, OnTouchPropItem);
        }
        itemScript.SetIndex(index);

        itemScript.FreshItem(Excel.GetSceneDataList()[index].Id);

        return item;
    }

    private void OnTouchPropItem(int index)
    {
        Debug.Log(" OnTouchPropItem " + index);
    }
}
