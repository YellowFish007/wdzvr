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

    //角色列表
    public LoopListView2 chatRoleListView;

    //表情列表
    public LoopGridView chatEmojiGridView;

    private int mChatRoleIndex = 0;

    //聊天记录
    public ScrollRect chatMsgScrollRect;
    public GameObject chatMsgContent;


    public Button emojiBtn;
    public Button sendBtn;

    public InputField msgInputField;

    private void Awake()
    {
        emojiBtn.AddOnPointerClick(OnBtnClick);
        sendBtn.AddOnPointerClick(OnBtnClick);
    }

    private void OnBtnClick(Button btn)
    {
        if (btn == emojiBtn)
        {
            Debug.Log("emojiBtn");

            chatEmojiGridView.gameObject.SetActive(!chatEmojiGridView.gameObject.activeSelf);
        }
        else if (btn == sendBtn)
        {
            ChatData.Instance.AddMessage(1001, ChatData.ChatMsg.CreateText(msgInputField.text));
            msgInputField.text = "";

            FreshChatMsg(1001);
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

        FreshChatMsg(1001);
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

        ChatData.Instance.AddMessage(1001, ChatData.ChatMsg.CreateEmoji(index + ""));
        FreshChatMsg(1001);

        chatEmojiGridView.gameObject.SetActive(false);
    }

    private void FreshChatMsg(int friendId)
    {
        // 1. 清理现有消息
        for (int i = chatMsgContent.transform.childCount - 1; i >= 0; i--)
        {
            GameObject.Destroy(chatMsgContent.transform.GetChild(i).gameObject);
        }

        List<ChatData.ChatMsg> friendMsgList = ChatData.Instance.GetFriendMessages(friendId);
        List<UIChatMsgItem> createdItems = new List<UIChatMsgItem>();

        for (int i = 0; i < friendMsgList.Count; i++)
        {
            GameObject obj = chatMsgContent.AddPrefab("Prefabs/UI/Chat/UIChatMsgRItem");

            UIChatMsgItem chatMsgItem = obj.GetComponent<UIChatMsgItem>();

            chatMsgItem.FreshItem(friendMsgList[i]);
            createdItems.Add(chatMsgItem);
        }

        // 2. 启动排版协程
        StartCoroutine(LayoutChatItems(createdItems));
    }

    private IEnumerator LayoutChatItems(List<UIChatMsgItem> items)
    {
        // 等待几帧，确保Item内部的尺寸计算完成
        yield return null;
        yield return null;

        float startY = 0;
        float spacing = 20; // 间距
        float paddingBottom = 50;

        foreach (var item in items)
        {
            if (item == null) continue;

            RectTransform itemRect = item.transform as RectTransform;

            // 强制更新Item布局以防万一
            // LayoutRebuilder.ForceRebuildLayoutImmediate(itemRect);

            float height = itemRect.sizeDelta.y;

            // 计算Y坐标 (假设锚点在顶部，向下延伸)
            // 如果Pivot是(0.5, 0.5)，Y应该是 -startY - height/2
            // 这里我们动态调整Pivot或者根据Pivot计算

            float pivotOffset = height * (1 - itemRect.pivot.y);
            float yPos = -startY - pivotOffset;

            itemRect.anchoredPosition = new Vector2(0, yPos);

            startY += height + spacing;
        }

        // 更新Content高度
        RectTransform contentRect = chatMsgContent.transform as RectTransform;
        contentRect.sizeDelta = new Vector2(contentRect.sizeDelta.x, startY + paddingBottom);

        // 滚动到底部
        yield return null;
        chatMsgScrollRect.verticalNormalizedPosition = 0;
    }
}

