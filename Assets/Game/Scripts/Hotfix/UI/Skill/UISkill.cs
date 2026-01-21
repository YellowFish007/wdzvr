using System;
using System.Collections;
using System.Collections.Generic;
using Engine;
using RbEngine;
using UnityEngine;
using UnityEngine.UI;

public class UISkill : UIBase
{
    public Button closeBtn;

    public override void OnCreate()
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
