using System;
using System.Collections;
using System.Collections.Generic;
using Engine;
using RbEngine;
using SuperScrollView;
using UnityEngine;
using UnityEngine.UI;

public class UIBag : UIBase
{
    public Button closeBtn;

    public UITabGroup tabGroup;

    public LoopGridView bagGridView;

    public override void OnCreate(params object[] args)
    {
        closeBtn.AddOnPointerClick(OnBtnClick);

        tabGroup.Init(OnTabClick);                  
    }

    private void OnTabClick(int index)
    {
        //根据index切换显示内容
        switch (index)
        {
            case 0:
                //显示所有物品
                break;
            case 1:
                //显示装备物品
                break;
        }
    }

    private void OnBtnClick(Button btn)
    {
        if (btn == closeBtn)
        {
            Close();
        }
    }
}
