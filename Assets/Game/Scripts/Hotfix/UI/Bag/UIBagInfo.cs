using System;
using System.Collections;
using System.Collections.Generic;
using Engine;
using RbEngine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIBagInfo : UIBase
{

    public Button closeBtn;
    public Button useBtn;
    public Button discardBtn;

    public Image itemImg;
    public TMP_Text nameText;
    public TMP_Text descriptionText;

    int _itemId;
    public override void OnCreate(params object[] args)
    {
        closeBtn.AddOnPointerClick(OnBtnClick);
        useBtn.AddOnPointerClick(OnBtnClick);
        discardBtn.AddOnPointerClick(OnBtnClick);

        int id = (int)args[0];
        _itemId = id;

        nameText.text = Excel.GetItemName(id);
        descriptionText.text = Excel.GetItemDesc(id);
        itemImg.SetItemSprite(Excel.GetItemIcon(id));
    }

    private void OnBtnClick(Button btn)
    {
        if (btn == closeBtn)
        {
            SceneUIManager.Instance.ClosePersistentUI(UIConfig.BagInfo);
        }
        else if (btn == useBtn)
        {
            // 使用物品
        }
        else if (btn == discardBtn)
        {
            // 丢弃物品
        }
    }
}
