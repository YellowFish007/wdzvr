using System;
using Engine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static FriendData;

public class UIFriendItem : MonoBehaviour
{
    public GameObject grayBgObj, lightBgObj;
    public Image headImg;
    public Text nameText;
    public TMP_Text levelText;
    public TMP_Text onlineText;
    public Button chatBtn, delBtn;

    private FriendInfo _data;
    private Action<FriendInfo> _onClickCallback;

    public void Init(FriendInfo data, Action<FriendInfo> onClick)
    {
        _data = data;
        _onClickCallback = onClick;

        if (nameText != null) nameText.text = data.Name;
        if (levelText != null) levelText.text = $"Lv.{data.Level}";
        
        if (onlineText != null)
        {
            onlineText.text = data.IsOnline ? "在线" : "离线";
            onlineText.color = data.IsOnline ? Color.green : Color.gray;
        }

        if (headImg != null)
        {
            headImg.SetSprite(data.HeadIcon);
        }
    }

    private void OnClick()
    {
        _onClickCallback?.Invoke(_data);
    }
}
