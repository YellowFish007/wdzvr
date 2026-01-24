using System;
using System.Collections;
using System.Collections.Generic;
using Engine;
using RbEngine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UILogin : UIBase
{
    public Button loginBtn;
    public Button registerBtn;
    public Button forgetPasswordBtn;
    public Button sendNumBtn;

    public TMP_InputField phoneNumInputField;
    public TMP_InputField passwordInputField;
    public TMP_InputField codeInputField;

    public override void OnCreate(params object[] args)
    {
        loginBtn.AddOnPointerClick(OnBtnClick);
        registerBtn.AddOnPointerClick(OnBtnClick);
        forgetPasswordBtn.AddOnPointerClick(OnBtnClick);
        sendNumBtn.AddOnPointerClick(OnBtnClick);

        Data.Get<LocalData>();

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
            //GameManager.Instance.ShowFlyTip("发送验证码");
            GameManager.Instance.ShowConfirm("是否发送验证码？", () =>
            {
                Debug.Log("是否发送验证码");
            });
            
        }
    }
}
