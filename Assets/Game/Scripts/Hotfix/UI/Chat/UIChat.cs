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
    // Constants
    private const float MSG_SPACING = 20f;
    private const float MSG_PADDING_BOTTOM = 50f;
    private const int CURRENT_USER_ID = 1001;

    // UI Components
    public LoopListView2 chatRoleListView;
    public LoopGridView chatEmojiGridView;
    public ScrollRect chatMsgScrollRect;
    public GameObject chatMsgContent;
    public Button emojiBtn;
    public Button sendBtn;
    public InputField msgInputField;

    public Button cancelVoiceBtn;
    public Button voiceBtn;

    public Button closeBtn;

    private int mChatRoleIndex = 0;

    public override void OnCreate(params object[] args)
    {
        emojiBtn.AddOnPointerClick(OnBtnClick);
        sendBtn.AddOnPointerClick(OnBtnClick);
        cancelVoiceBtn.AddOnPointerClick(OnBtnClick);
        voiceBtn.AddOnPointerClick(OnBtnClick);
        closeBtn.AddOnPointerClick(OnBtnClick);

        Debug.Log("FriendData.Instance.GetFriendCount() : " + FriendData.Instance.GetFriendCount());

        InitRoleListView();
        InitEmojiGridView();
        FreshChatMsg(CURRENT_USER_ID);
    }

    private void OnBtnClick(Button btn)
    {
        if (btn == emojiBtn)
        {
            ToggleEmojiPanel();
        }
        else if (btn == sendBtn)
        {
            SendTextMessage();
        }
        else if (btn == cancelVoiceBtn)
        {
            byte[] voivceBt = VoiceManager.Instance.StopRecord();
            ChatData.Instance.AddMessage(1001, ChatData.ChatMsg.CreateVoice(voivceBt, 5));

            Debug.Log("voivceBt : " + voivceBt.Length);

            FreshChatMsg(1001);

            cancelVoiceBtn.SetActive(false);
            voiceBtn.SetActive(true);

        }
        else if (btn == voiceBtn)
        {
            VoiceManager.Instance.StartRecord();

            cancelVoiceBtn.SetActive(true);
            voiceBtn.SetActive(false);
        }
        else if (btn == closeBtn)
        {
            Close();
        }

    }

    private void ToggleEmojiPanel()
    {
        bool isActive = !chatEmojiGridView.gameObject.activeSelf;
        chatEmojiGridView.gameObject.SetActive(isActive);
    }

    private void SendTextMessage()
    {
        if (string.IsNullOrEmpty(msgInputField.text)) return;

        ChatData.Instance.AddMessage(CURRENT_USER_ID, ChatData.ChatMsg.CreateText(msgInputField.text));
        msgInputField.text = "";
        FreshChatMsg(CURRENT_USER_ID);
    }

    private void InitRoleListView()
    {
        chatRoleListView.InitListView(0, OnGetItemByIndex);
        chatRoleListView.SetListItemCount(FriendData.Instance.GetFriendCount());
        chatRoleListView.RefreshAllShownItem();
    }

    private void InitEmojiGridView()
    {
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

    private LoopListViewItem2 OnGetItemByIndex(LoopListView2 listView, int index)
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
        mChatRoleIndex = index;
        chatRoleListView.RefreshAllShownItem();
    }

    private void OnTouchEmojiItem(int index)
    {
        ChatData.Instance.AddMessage(CURRENT_USER_ID, ChatData.ChatMsg.CreateEmoji(index.ToString()));
        FreshChatMsg(CURRENT_USER_ID);
        chatEmojiGridView.gameObject.SetActive(false);
    }

    private void FreshChatMsg(int friendId)
    {
        ClearChatMessages();

        List<ChatData.ChatMsg> friendMsgList = ChatData.Instance.GetFriendMessages(friendId);
        List<UIChatMsgItem> createdItems = new List<UIChatMsgItem>();

        foreach (var msg in friendMsgList)
        {
            GameObject obj = chatMsgContent.AddPrefab("Prefabs/UI/Chat/UIChatMsgRItem");
            UIChatMsgItem chatMsgItem = obj.GetComponent<UIChatMsgItem>();
            chatMsgItem.FreshItem(msg);
            createdItems.Add(chatMsgItem);
        }

        StartCoroutine(LayoutChatItems(createdItems));
    }

    private void ClearChatMessages()
    {
        for (int i = chatMsgContent.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(chatMsgContent.transform.GetChild(i).gameObject);
        }
    }

    private IEnumerator LayoutChatItems(List<UIChatMsgItem> items)
    {
        // Wait for items to initialize their sizes
        // UIChatMsgItem uses coroutines to size text, so we need to wait a bit
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();

        float currentY = 0;

        foreach (var item in items)
        {
            if (item == null) continue;

            RectTransform itemRect = item.transform as RectTransform;

            // Ensure we have the latest size if possible, though UIChatMsgItem handles its own size
            // LayoutRebuilder.ForceRebuildLayoutImmediate(itemRect);

            float height = itemRect.sizeDelta.y;
            float pivotOffset = height * (1 - itemRect.pivot.y);
            float yPos = -currentY - pivotOffset;

            itemRect.anchoredPosition = new Vector2(0, yPos);
            currentY += height + MSG_SPACING;
        }

        UpdateContentHeight(currentY);

        // Scroll to bottom after layout update
        yield return null;
        chatMsgScrollRect.verticalNormalizedPosition = 0;
    }

    private void UpdateContentHeight(float height)
    {
        RectTransform contentRect = chatMsgContent.transform as RectTransform;
        contentRect.sizeDelta = new Vector2(contentRect.sizeDelta.x, height + MSG_PADDING_BOTTOM);
    }
}
