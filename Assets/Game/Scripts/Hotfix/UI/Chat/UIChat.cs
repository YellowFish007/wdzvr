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
    public ScrollRect chatMsgScrollRect;
    public GameObject chatMsgContent;
    public Button emojiBtn;
    public Button sendBtn;
    public InputField msgInputField;

    public Button cancelVoiceBtn;
    public Button voiceBtn;
    public Button delChatBtn;

    public Button closeBtn;

    private int mChatRoleIndex = 0;

    public override void OnCreate(params object[] args)
    {
        emojiBtn.AddOnPointerClick(OnBtnClick);
        sendBtn.AddOnPointerClick(OnBtnClick);
        cancelVoiceBtn.AddOnPointerClick(OnBtnClick);
        voiceBtn.AddOnPointerClick(OnBtnClick);
        closeBtn.AddOnPointerClick(OnBtnClick);
        delChatBtn.AddOnPointerClick(OnBtnClick);

        InitRoleListView();

        FreshChatMsg(CURRENT_USER_ID);
    }

    private void OnBtnClick(Button btn)
    {
        if (btn == emojiBtn)
        {
            SceneUIManager.Instance.OpenPersistentUI(UIConfig.ChatEmoji, (Action<int>)OnEmojiSelected);
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
        else if (btn == delChatBtn)
        {

        }
        else if (btn == closeBtn)
        {
            Close();
        }

    }


    private void OnEmojiSelected(int index)
    {
        Debug.Log("OnEmojiSelected : " + index);

        ChatData.Instance.AddMessage(CURRENT_USER_ID, ChatData.ChatMsg.CreateEmoji(index.ToString()));
        FreshChatMsg(CURRENT_USER_ID);
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
        itemScript.FreshItem(friends[index].Name, friends[index].HeadIcon, index == mChatRoleIndex);
        return item;
    }

    private void OnTouchRoleItem(int index)
    {
        mChatRoleIndex = index;
        chatRoleListView.RefreshAllShownItem();
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

        LayoutChatItems(createdItems);
    }

    private void ClearChatMessages()
    {
        for (int i = chatMsgContent.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(chatMsgContent.transform.GetChild(i).gameObject);
        }
    }

    private void LayoutChatItems(List<UIChatMsgItem> items)
    {
        float currentY = 0;

        foreach (var item in items)
        {
            if (item == null) continue;

            RectTransform itemRect = item.transform as RectTransform;

            float height = itemRect.sizeDelta.y;
            float pivotOffset = height * (1 - itemRect.pivot.y);
            float yPos = -currentY - pivotOffset;

            itemRect.anchoredPosition = new Vector2(0, yPos);
            currentY += height + MSG_SPACING;
        }

        RectTransform contentRect = chatMsgContent.transform as RectTransform;
        contentRect.sizeDelta = new Vector2(contentRect.sizeDelta.x, currentY + MSG_PADDING_BOTTOM);

        // Ensure layout is updated before scrolling
        Canvas.ForceUpdateCanvases();
        chatMsgScrollRect.verticalNormalizedPosition = 0;
    }
}
