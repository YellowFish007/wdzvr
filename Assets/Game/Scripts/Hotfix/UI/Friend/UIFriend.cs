using System;
using System.Collections;
using System.Collections.Generic;
using Engine;
using RbEngine;
using SuperScrollView;
using UnityEngine;
using UnityEngine.UI;
using static FriendData;

public class UIFriend : UIBase
{
    public Button closeBtn;
    public UITabGroup tabGroup;

    public LoopListView2 friendlListView;
    public LoopListView2 friendApplyListView;

    private List<FriendInfo> _friendList = new List<FriendInfo>();
    private List<FriendInfo> _applyList = new List<FriendInfo>();

    public override void OnCreate(params object[] args)
    {
        closeBtn.AddOnPointerClick(OnBtnClick);

        if (friendlListView != null)
        {
            friendlListView.InitListView(0, OnGetFriendItem);
        }

        if (friendApplyListView != null)
        {
            friendApplyListView.InitListView(0, OnGetApplyItem);
        }

        if (tabGroup != null)
        {
            tabGroup.Init(OnTabChanged);
        }
    }

    private void OnBtnClick(Button btn)
    {
        if (btn == closeBtn)
        {
            Close();
        }
    }

    private void OnTabChanged(int index)
    {
        if (index == 0)
        {
            if (friendlListView != null) friendlListView.gameObject.SetActive(true);
            if (friendApplyListView != null) friendApplyListView.gameObject.SetActive(false);
            RefreshFriendList();
        }
        else if (index == 1)
        {
            if (friendlListView != null) friendlListView.gameObject.SetActive(false);
            if (friendApplyListView != null) friendApplyListView.gameObject.SetActive(true);
            RefreshApplyList();
        }
    }

    private void RefreshFriendList()
    {
        _friendList = FriendData.Instance.GetAllFriends();
        if (friendlListView != null)
        {
            friendlListView.SetListItemCount(_friendList.Count);
            friendlListView.RefreshAllShownItem();
        }
    }

    private void RefreshApplyList()
    {
        _applyList = FriendData.Instance.GetApplyList();
        if (friendApplyListView != null)
        {
            friendApplyListView.SetListItemCount(_applyList.Count);
            friendApplyListView.RefreshAllShownItem();
        }
    }

    private LoopListViewItem2 OnGetFriendItem(LoopListView2 listView, int index)
    {
        if (index < 0 || index >= _friendList.Count) return null;

        LoopListViewItem2 item = listView.NewListViewItem("UIFriendItem");
        UIFriendItem script = item.GetComponent<UIFriendItem>();
        if (script != null)
        {
            script.Init(_friendList[index], OnFriendItemClick);
        }
        return item;
    }

    private void OnFriendItemClick(FriendInfo info)
    {
        // 点击好友列表项
        Debug.Log($"Clicked friend: {info.Name}");
    }

    private LoopListViewItem2 OnGetApplyItem(LoopListView2 listView, int index)
    {
        if (index < 0 || index >= _applyList.Count) return null;

        LoopListViewItem2 item = listView.NewListViewItem("UIFriendApplyItem");
        UIFriendApplyItem script = item.GetComponent<UIFriendApplyItem>();
        if (script != null)
        {
            script.Init(_applyList[index], OnApplyItemAction);
        }
        return item;
    }

    private void OnApplyItemAction(FriendInfo info, bool isAccept)
    {
        if (isAccept)
        {
            // 同意好友申请
            Debug.Log($"Accept friend: {info.Name}");
            // 逻辑处理：添加到好友，从申请列表移除
            FriendData.Instance.AddFriend(info);
            FriendData.Instance.RemoveApply(info.Id);
            RefreshApplyList();
        }
        else
        {
            // 拒绝好友申请
            Debug.Log($"Refuse friend: {info.Name}");
            FriendData.Instance.RemoveApply(info.Id);
            RefreshApplyList();
        }
    }
}
