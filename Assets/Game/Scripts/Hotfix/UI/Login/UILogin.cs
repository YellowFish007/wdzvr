using System;
using System.Collections;
using System.Collections.Generic;
using Engine;
using RbEngine;
using UnityEngine;
using UnityEngine.UI;

public class UILogin : UIBase
{
    public override string Name => "UILogin";

    public Button loginBtn;

    private void Awake()
    {
        loginBtn.AddOnPointerClick(OnBtnClick);
    }

    private void OnBtnClick(Button btn)
    {
        if (loginBtn == btn)
        {
            GameEvent.Send("UIRoot", UIConfig.Main);
        }
    }

    public override void OnOpen()
    {
        // string icon = Excel.GetSceneIcon(1001);
        // Debug.Log("UILogin icon : " + icon);
    }

}
