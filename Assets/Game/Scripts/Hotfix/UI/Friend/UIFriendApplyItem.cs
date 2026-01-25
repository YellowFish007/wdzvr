using System;
using Engine;
using RbEngine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static FriendData;

public class UIFriendApplyItem : UIScollItem
{
    public GameObject grayBgObj, lightBgObj;
    public Image headImg;
    public Text nameText;
    public TMP_Text levelText;
    public TMP_Text onlineText;
    public Button refuseBtn, acceptBtn;

    private int _id;
    private Action<int, bool> _onActionCallback; // bool: true=accept, false=refuse

    public void SetClickCallback(Action<int, bool> callback)
    {
        _onActionCallback = callback;
    }

    public void FreshItem(int id)
    {
        _id = id;

        FriendInfo data = null;
        var applyList = FriendData.Instance.GetApplyList();
        foreach (var item in applyList)
        {
            if (item.Id == id)
            {
                data = item;
                break;
            }
        }
        
        // If not found in apply list, check friend list? (Unlikely for ApplyItem)
        if (data == null) data = FriendData.Instance.GetFriend(id);
        
        if (data == null) return;

        if (nameText != null) nameText.text = data.Name;
        if (levelText != null) levelText.text = $"等级:{data.Level}";
        if (onlineText != null) onlineText.text = data.IsOnline ? "在线" : "离线";
        
        if (headImg != null)
        {
            headImg.SetHeadSprite(data.HeadIcon);
        }

        if (refuseBtn != null)
        {
            refuseBtn.onClick.RemoveAllListeners();
            refuseBtn.onClick.AddListener(OnRefuse);
        }

        if (acceptBtn != null)
        {
            acceptBtn.onClick.RemoveAllListeners();
            acceptBtn.onClick.AddListener(OnAccept);
        }
    }

    private void OnRefuse()
    {
        _onActionCallback?.Invoke(_id, false);
    }

    private void OnAccept()
    {
        _onActionCallback?.Invoke(_id, true);
    }
}
