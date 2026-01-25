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

    public LoopListView2 friendListView;
    public LoopListView2 friendApplyListView;

    private List<FriendInfo> _friendList = new List<FriendInfo>();
    private List<FriendInfo> _applyList = new List<FriendInfo>();

    public override void OnCreate(params object[] args)
    {
        closeBtn.AddOnPointerClick(OnBtnClick);

        if (friendListView != null)
        {
            friendListView.InitListView(0, OnGetFriendItem);
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
            friendListView.gameObject.SetActive(true);            
            friendApplyListView.gameObject.SetActive(false);
            RefreshFriendList();
        }
        else if (index == 1)
        {
            friendListView.gameObject.SetActive(false);            
            friendApplyListView.gameObject.SetActive(true);
            RefreshApplyList();
        }
    }

    private void RefreshFriendList()
    {
        _friendList = FriendData.Instance.GetAllFriends();
        if (friendListView != null)
        {
            friendListView.SetListItemCount(_friendList.Count);
            friendListView.RefreshAllShownItem();
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
        if (!script.isInit)
        {
            script.Init(friendListView.gameObject);
        }
        script.FreshItem(_friendList[index].Id);
        return item;
    }

    private LoopListViewItem2 OnGetApplyItem(LoopListView2 listView, int index)
    {
        if (index < 0 || index >= _applyList.Count) return null;

        LoopListViewItem2 item = listView.NewListViewItem("UIFriendApplyItem");
        UIFriendApplyItem script = item.GetComponent<UIFriendApplyItem>();
        if (!script.isInit)
        {
            script.Init(friendListView.gameObject);
        }
        script.FreshItem(_applyList[index].Id);
        return item;
    }

    private void OnApplyItemAction(int id, bool isAccept)
    {
        // Find info by ID from apply list (or FriendData helpers if improved)
        FriendInfo info = null;
        foreach (var item in _applyList)
        {
            if (item.Id == id)
            {
                info = item;
                break;
            }
        }

        // Fallback to friend data if needed, but apply list items should be in apply list
        if (info == null) info = FriendData.Instance.GetFriend(id);

        if (info == null) return;

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
