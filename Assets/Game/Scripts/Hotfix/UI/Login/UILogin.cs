using System;
using System.Collections;
using System.Collections.Generic;
using Engine;
using RbEngine;
using UnityEngine;
using UnityEngine.UI;

public class UILogin : UIBase
{
    public Button loginBtn;

    public override void OnCreate()
    {
        loginBtn.AddOnPointerClick(OnBtnClick);
    }


    private void OnBtnClick(Button btn)
    {
        if (loginBtn == btn)
        {
            //GameEvent.Send("UIRoot", UIConfig.Main);
            SceneUIManager.Instance.OpenUI(UIConfig.Main);
        }
    }
}
