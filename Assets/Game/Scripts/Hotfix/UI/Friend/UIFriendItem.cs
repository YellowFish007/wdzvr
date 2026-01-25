using System;
using Engine;
using RbEngine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static FriendData;

public class UIFriendItem : UIScollItem
{
    public GameObject grayBgObj, lightBgObj;
    public Image headImg;
    public Text nameText;
    public TMP_Text levelText;
    public TMP_Text onlineText;
    public Button chatBtn, delBtn;

    private int _id;


    private void Awake()
    {
        chatBtn.AddOnPointerClick(OnBtnClick);
        delBtn.AddOnPointerClick(OnBtnClick);
    }

    private void OnBtnClick(Button btn)
    {
        if (btn == chatBtn)
        {
            SceneUIManager.Instance.OpenUI(UIConfig.Chat);
        }
        else if (btn == delBtn)
        {

        }
    }

    public void FreshItem(int id)
    {
        _id = id;

        FriendInfo data = FriendData.Instance.GetFriend(id);

        nameText.text = data.Name;
        levelText.text = $"Lv.{data.Level}";
        onlineText.text = data.IsOnline ? "在线" : "离线";
        onlineText.color = data.IsOnline ? Color.green : Color.gray;
        grayBgObj.SetActive(!data.IsOnline);
        lightBgObj.SetActive(data.IsOnline);
        headImg.SetHeadSprite(data.HeadIcon);
    }

}
