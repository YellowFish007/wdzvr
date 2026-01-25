using System;
using Engine;
using RbEngine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static FriendData;

public class UIFriendApplyItem : MonoBehaviour
{
    public GameObject grayBgObj, lightBgObj;
    public Image headImg;
    public Text nameText;
    public TMP_Text levelText;
    public TMP_Text onlineText;
    public Button refuseBtn, acceptBtn;

    private FriendInfo _data;
    private Action<FriendInfo, bool> _onActionCallback; // bool: true=accept, false=refuse

    public void Init(FriendInfo data, Action<FriendInfo, bool> callback)
    {
        _data = data;
        _onActionCallback = callback;

        if (nameText != null) nameText.text = data.Name;
        if (levelText != null) levelText.text = $"等级:{data.Level}";
        if (onlineText != null) onlineText.text = data.IsOnline ? "在线" : "离线";
        
        if (headImg != null)
        {
            headImg.SetSprite(data.HeadIcon);
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
        _onActionCallback?.Invoke(_data, false);
    }

    private void OnAccept()
    {
        _onActionCallback?.Invoke(_data, true);
    }
}
