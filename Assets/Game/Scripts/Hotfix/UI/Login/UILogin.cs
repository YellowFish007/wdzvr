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
    public Button registerBtn;
    public Button forgetPasswordBtn;
    public Button sendNumBtn;

    public override void OnCreate()
    {
        loginBtn.AddOnPointerClick(OnBtnClick);
        registerBtn.AddOnPointerClick(OnBtnClick);
        forgetPasswordBtn.AddOnPointerClick(OnBtnClick);
        sendNumBtn.AddOnPointerClick(OnBtnClick);
    }


    private void OnBtnClick(Button btn)
    {
        if (loginBtn == btn)
        {
            SceneUIManager.Instance.OpenUI(UIConfig.Main);
        }           
        else if (registerBtn == btn)
        {
            SceneUIManager.Instance.OpenUI(UIConfig.Register);
        }
        else if (forgetPasswordBtn == btn)
        {
            SceneUIManager.Instance.OpenUI(UIConfig.ForgetPassword);
        }
        else if (sendNumBtn == btn)
        {
            GameManager.Instance.ShowFlyTip("发送验证码");
        }
    }
}
