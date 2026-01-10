using System.Collections;
using System.Collections.Generic;
using Engine;
using SuperScrollView;
using UnityEngine;

public class UIChat : UIBase
{
    public override string Name => "UIChat";

    public LoopListView2 chatRolelListView;

    private int mChatRoleIndex = 0;
    public override void OnOpen()
    {
        chatRolelListView.InitListView(0, OnGetItemByIndex);
        chatRolelListView.SetListItemCount(FriendData.Instance.GetFriendCount());
        chatRolelListView.RefreshAllShownItem();

    }

    LoopListViewItem2 OnGetItemByIndex(LoopListView2 listView, int index)
    {
        LoopListViewItem2 item = listView.NewListViewItem("UIChatRoleItem");

        UIChatRoleItem itemScript = item.GetComponent<UIChatRoleItem>();
        if (!itemScript.isInit)
        {
            itemScript.Init(chatRolelListView.gameObject, OnTouchPropItem);
        }
        itemScript.SetIndex(index);

        List<FriendData.FriendInfo> friends = FriendData.Instance.GetAllFriends();

        itemScript.FreshItem(friends[index].Name, friends[index].Avatar, index == mChatRoleIndex);

        return item;
    }

    private void OnTouchPropItem(int index)
    {
        Debug.Log(" OnTouchPropItem " + index);
        mChatRoleIndex = index;
        chatRolelListView.RefreshAllShownItem();
    }
}

