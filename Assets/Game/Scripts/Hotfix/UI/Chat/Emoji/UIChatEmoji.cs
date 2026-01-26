using System.Collections;
using System.Collections.Generic;
using Engine;
using SuperScrollView;
using UnityEngine;

public class UIChatEmoji : UIBase
{
    public LoopGridView chatEmojiGridView;

    public override void OnCreate(params object[] args)
    {
        InitEmojiGridView();
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

    private void OnTouchEmojiItem(int index)
    {
        //ChatData.Instance.AddMessage(CURRENT_USER_ID, ChatData.ChatMsg.CreateEmoji(index.ToString()));
        Debug.Log("OnTouchEmojiItem : " + index);
        SceneUIManager.Instance.ClosePersistentUI(UIConfig.ChatEmoji);
    }

}
