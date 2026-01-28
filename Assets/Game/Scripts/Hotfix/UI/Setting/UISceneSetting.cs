using System;
using System.Collections;
using System.Collections.Generic;
using Engine;
using RbEngine;
using UnityEngine;
using UnityEngine.UI;

public class UISceneSetting : UIBase
{
    public Button loadSceneBtn;

    public override void OnCreate(params object[] args)
    {
        loadSceneBtn.AddOnPointerClick(OnBtnClick);
    }

    private void OnBtnClick(Button btn)
    {
        if (btn == loadSceneBtn)
        {
            SceneUIManager.Instance.OpenUI(UIConfig.HistoryScene);
        }
    }
}
