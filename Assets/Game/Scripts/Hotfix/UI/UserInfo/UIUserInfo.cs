using System.Collections;
using System.Collections.Generic;
using Engine;
using RbEngine;
using UnityEngine;
using UnityEngine.UI;

public class UIUserInfo : UIBase
{
    public Button closeBtn;

    public override void OnCreate(params object[] args)
    {
        closeBtn.AddOnPointerClick(OnBtnClick);
    }
    private void OnBtnClick(Button btn)
    {
        if (btn == closeBtn)
        {
            Close();
        }
    }
}
