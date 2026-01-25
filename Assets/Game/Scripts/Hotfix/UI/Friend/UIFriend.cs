using System;
using System.Collections;
using System.Collections.Generic;
using Engine;
using RbEngine;
using UnityEngine;
using UnityEngine.UI;

public class UIFriend : UIBase
{
    public Button closeBtn;
    public UITabGroup tabGroup;

    public override void OnCreate(params object[] args)
    {
        closeBtn.AddOnPointerClick(OnBtnClick);

        if (tabGroup != null)
        {
            tabGroup.Init(OnTabChanged);
        }
    }

    private void OnBtnClick(Button btn)
    {
        if (btn == closeBtn)
        {
            Close();
        }
    }

    private void OnTabChanged(int index)
    {
        Debug.Log($"Switch to tab: {index}");
        if (index == 0)
        {
            // 刷新好友列表
        }
        else if (index == 1)
        {
            // 刷新添加好友界面
        }
    }
}
