using System.Collections;
using System.Collections.Generic;
using Engine;
using SuperScrollView;
using UnityEngine;

public class UIChatEmoji : UIBase
{
    public LoopGridView chatEmojiGridView;

    private System.Action<int> _onEmojiClick;

    public override void OnCreate(params object[] args)
    {
        if (args != null && args.Length > 0 && args[0] is System.Action<int> callback)
        {
            _onEmojiClick = callback;
        }
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
        _onEmojiClick?.Invoke(index);
        SceneUIManager.Instance.ClosePersistentUI(UIConfig.ChatEmoji);
    }

}
