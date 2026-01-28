using System.Collections;
using System.Collections.Generic;
using Engine;
using RbEngine;
using SuperScrollView;
using UnityEngine;
using UnityEngine.UI;

public class UIHistoryScene : UIBase
{
    public LoopListView2 historyListView;
    public Button closeBtn;

    private List<SceneHistoryData.HistoryInfo> _historyList;

    public override void OnCreate(params object[] args)
    {
        closeBtn.AddOnPointerClick(OnBtnClick);
        
        _historyList = SceneHistoryData.Instance.GetAllHistory();
        
        historyListView.InitListView(_historyList.Count, OnGetItemByIndex);
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
        if (index < 0 || index >= _historyList.Count)
        {
            return null;
        }

        LoopListViewItem2 item = listView.NewListViewItem("UIHistorySceneItem");
        UIHistorySceneItem itemScript = item.GetComponent<UIHistorySceneItem>();
        
        if (!itemScript.isInit)
        {
            itemScript.Init(historyListView.gameObject, OnTouchItem);
        }
        itemScript.SetIndex(index);
        itemScript.FreshItem(_historyList[index]);
        
        return item;
    }
    
    private void OnTouchItem(int index)
    {
        // Optional: Handle item click if needed
        Debug.Log("OnTouchHistoryItem: " + index);
    }
}
