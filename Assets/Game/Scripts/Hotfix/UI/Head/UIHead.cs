using System.Collections;
using System.Collections.Generic;
using Engine;
using RbEngine;
using UnityEngine;
using UnityEngine.UI;

public class UIHead : UIBase
{

    public Button closeBtn;
    public UITabGroup tabGroup;

    public override void OnCreate(params object[] args)
    {
        closeBtn.AddOnPointerClick(OnBtnClick);

        tabGroup.defaultIndex = 1;
        tabGroup.Init(OnTabChanged);
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
        Debug.Log("OnTabChanged : " + index);
    }
}
