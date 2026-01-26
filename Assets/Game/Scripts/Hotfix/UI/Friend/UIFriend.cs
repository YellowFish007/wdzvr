using System;
using System.Collections;
using System.Collections.Generic;
using Engine;
using RbEngine;
using SuperScrollView;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static FriendData;

public class UIFriend : UIBase
{
    public Button closeBtn;
    public UITabGroup tabGroup;

    public TMP_InputField searchInputField;
    public Button searchBtn;

    public LoopListView2 friendListView;
    public LoopListView2 friendApplyListView;

    private List<FriendInfo> _friendList = new List<FriendInfo>();
    private List<FriendInfo> _applyList = new List<FriendInfo>();

    public override void OnCreate(params object[] args)
    {
        closeBtn.AddOnPointerClick(OnBtnClick);
        searchBtn.AddOnPointerClick(OnBtnClick);

        friendListView.InitListView(0, OnGetFriendItem);
        friendApplyListView.InitListView(0, OnGetApplyItem);
        tabGroup.Init(OnTabChanged);
    }

    private void OnBtnClick(Button btn)
    {
        if (btn == closeBtn)
        {
            Close();
        }
        else if (btn == searchBtn)
        {
            SceneUIManager.Instance.OpenUI(UIConfig.FriendInfo);
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
        _friendList.Sort((a, b) =>
        {
            if (a.IsOnline != b.IsOnline)
            {
                return b.IsOnline.CompareTo(a.IsOnline);
            }
            return 0;
        });

        if (friendListView != null)
        {
            friendListView.SetListItemCount(_friendList.Count);
            friendListView.RefreshAllShownItem();
        }
    }

    private void RefreshApplyList()
    {
        _applyList = new List<FriendInfo>(FriendData.Instance.GetApplyList());
        _applyList.Sort((a, b) =>
        {
            if (a.IsOnline != b.IsOnline)
            {
                return b.IsOnline.CompareTo(a.IsOnline);
            }
            return 0;
        });

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
            script.Init(friendApplyListView.gameObject);
        }
        script.FreshItem(_applyList[index].Id);
        return item;
    }

}
