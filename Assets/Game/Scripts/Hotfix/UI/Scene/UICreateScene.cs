using System.Collections;
using System.Collections.Generic;
using Engine;
using RbEngine;
using SuperScrollView;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityTimer;

public class UICreateScene : UIBase
{
    public LoopListView2 sceneListView;
    public Button closeBtn;

    public override void OnCreate(params object[] args)
    {
        sceneListView.InitListView(0, OnGetItemByIndex);
        sceneListView.SetListItemCount(Excel.GetSceneDataList().Count);
        sceneListView.RefreshAllShownItem();
        closeBtn.AddOnPointerClick(OnBtnClick);
    }

    private void OnBtnClick(Button btn)
    {
        if (btn == closeBtn)
        {
            Close();
        }
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
