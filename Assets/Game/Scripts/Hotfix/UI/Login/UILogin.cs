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

        var localData = Data.Get<LocalData>();
        phoneNumInputField.text = localData.UserName;
        passwordInputField.text = localData.Password;
    }


    private void OnBtnClick(Button btn)
    {
        if (loginBtn == btn)
        {
            string phone = phoneNumInputField.text;
            string pwd = passwordInputField.text;

            if (LoginUtils.ValidateCredentials(phone, pwd))
            {
                var localData = Data.Get<LocalData>();
                localData.UserName = phone;
                localData.Password = pwd;
                Data.Save<LocalData>();

                SceneUIManager.Instance.OpenUI(UIConfig.Main);
            }
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
            string phone = phoneNumInputField.text;
            if (string.IsNullOrEmpty(phone) || phone.Length != 11)
            {
                GameManager.Instance.ShowFlyTip("请输入正确的手机号");
                return;
            }

            GameManager.Instance.ShowConfirm("是否发送验证码？", () =>
            {
                GameManager.Instance.ShowFlyTip("验证码已发送");
            });

        }
    }
}
