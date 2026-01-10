using System;
using System.Collections;
using System.Collections.Generic;
using Engine;
using RbEngine;
using SuperScrollView;
using UnityEngine;
using UnityEngine.UI;

public class UIChat : UIBase
{
    public override string Name => "UIChat";

    public LoopListView2 chatRoleListView;

    public LoopGridView chatEmojiGridView;

    private int mChatRoleIndex = 0;

    public Button emojiBtn;

    private void Awake()
    {
        emojiBtn.AddOnPointerClick(OnBtnClick);
    }

    private void OnBtnClick(Button btn)
    {
        if (btn == emojiBtn)
        {
            Debug.Log("emojiBtn");

            chatEmojiGridView.gameObject.SetActive(!chatEmojiGridView.gameObject.activeSelf);
        }
    }

    public override void OnOpen()
    {
        chatRoleListView.InitListView(0, OnGetItemByIndex);
        chatRoleListView.SetListItemCount(FriendData.Instance.GetFriendCount());
        chatRoleListView.RefreshAllShownItem();

        chatEmojiGridView.InitGridView(0, OnGetEmojjItemByRowColumn);
        chatEmojiGridView.SetListItemCount(160);
        chatEmojiGridView.RefreshAllShownItem();
    }

    private LoopGridViewItem OnGetEmojjItemByRowColumn(LoopGridView gridView, int itemIndex, int row, int column)
    {
        LoopGridViewItem item = gridView.NewListViewItem("UIChatEmojiItem");

        UIChatEmojiItem itemScript = item.GetComponent<UIChatEmojiItem>();
        if (!itemScript.isInit)
        {
            itemScript.Init(chatEmojiGridView.gameObject, OnTouchEmojiItem);
        }
        itemScript.SetIndex(itemIndex);

        itemScript.FreshItem(itemIndex);

        return item;
    }

    LoopListViewItem2 OnGetItemByIndex(LoopListView2 listView, int index)
    {
        LoopListViewItem2 item = listView.NewListViewItem("UIChatRoleItem");

        UIChatRoleItem itemScript = item.GetComponent<UIChatRoleItem>();
        if (!itemScript.isInit)
        {
            itemScript.Init(chatRoleListView.gameObject, OnTouchRoleItem);
        }
        itemScript.SetIndex(index);

        List<FriendData.FriendInfo> friends = FriendData.Instance.GetAllFriends();

        itemScript.FreshItem(friends[index].Name, friends[index].Avatar, index == mChatRoleIndex);

        return item;
    }

    private void OnTouchRoleItem(int index)
    {
        Debug.Log(" OnTouchRoleItem " + index);
        mChatRoleIndex = index;
        chatRoleListView.RefreshAllShownItem();
    }


    private void OnTouchEmojiItem(int index)
    {
        Debug.Log(" OnTouchEmojiItem " + index);
    }
}

