using System.Collections;
using System.Collections.Generic;
using Engine;
using RbEngine;
using UnityEngine;
using UnityEngine.UI;

public class UIBagInfo : UIBase
{

    public Button closeBtn;
    public Button useBtn;
    public Button discardBtn;

    public override void OnCreate(params object[] args)
    {
        closeBtn.AddOnPointerClick(OnBtnClick);
        useBtn.AddOnPointerClick(OnBtnClick);
        discardBtn.AddOnPointerClick(OnBtnClick);
    }

    private void OnBtnClick(Button btn)
    {
        if (btn == closeBtn)
        {
            Close();
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
