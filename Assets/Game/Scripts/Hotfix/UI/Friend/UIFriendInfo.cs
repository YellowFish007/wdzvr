using System.Collections;
using System.Collections.Generic;
using Engine;
using RbEngine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIFriendInfo : UIBase
{
    public Image headImg;
    public TMP_Text idText;
    public TMP_Text nameText;
    public TMP_Text levelText;
    public Button applyBtn, closeBtn;

    public override void OnCreate(params object[] args)
    {
        closeBtn.AddOnPointerClick(OnBtnClick);
        applyBtn.AddOnPointerClick(OnBtnClick);
    }

    private void OnBtnClick(Button btn)
    {
        if (btn == closeBtn)
        {
            Close();
        }
        else if (btn == applyBtn)
        {
            Close();
        }
    }
}
