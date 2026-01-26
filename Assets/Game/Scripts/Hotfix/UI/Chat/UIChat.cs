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
    private const int CURRENT_USER_ID = 1001; // The friend we are chatting with
    // private const int MY_PLAYER_ID = 0; // Removed constant, use AccountData.Instance.GetId()

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
            ChatData.Instance.AddMessage(CURRENT_USER_ID, ChatData.ChatMsg.CreateVoice(AccountData.Instance.GetId(), voivceBt, 5));

            Debug.Log("voivceBt : " + voivceBt.Length);

            FreshChatMsg(CURRENT_USER_ID);

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

        ChatData.Instance.AddMessage(CURRENT_USER_ID, ChatData.ChatMsg.CreateEmoji(AccountData.Instance.GetId(), index.ToString()));
        FreshChatMsg(CURRENT_USER_ID);
    }

    private void SendTextMessage()
    {
        if (string.IsNullOrEmpty(msgInputField.text)) return;

        ChatData.Instance.AddMessage(CURRENT_USER_ID, ChatData.ChatMsg.CreateText(AccountData.Instance.GetId(), msgInputField.text));
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
            string prefabPath = (msg.SenderId == AccountData.Instance.GetId()) ? "Prefabs/UI/Chat/UIChatMsgRItem" : "Prefabs/UI/Chat/UIChatMsgLItem";
            GameObject obj = chatMsgContent.AddPrefab(prefabPath);
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
