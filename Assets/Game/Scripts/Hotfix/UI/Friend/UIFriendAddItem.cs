using System;
using Engine;
using RbEngine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static FriendData;

public class UIFriendAddItem : MonoBehaviour
{
    public GameObject grayBgObj, lightBgObj;
    public Image headImg;
    public Text nameText;
    public TMP_Text levelText;
    public TMP_Text onlineText;
    public Button refuseBtn, acceptBtn;
    private void Awake()
    {
        refuseBtn.AddOnPointerClick(OnBtnClick);
        acceptBtn.AddOnPointerClick(OnBtnClick);
    }
    private void OnBtnClick(Button button)
    {
    }

    public void FreshItem(int friendId)
    {
        FriendInfo friendData = FriendData.Instance.GetFriend(friendId);
        nameText.text = friendData.Name;
        levelText.text = $"等级:{friendData.Level}";
        onlineText.text = friendData.IsOnline ? "在线" : "离线";
        headImg.SetSprite("RawAssets/Texture/Icon/Avatar/" + friendData.HeadIcon);
    }

}
