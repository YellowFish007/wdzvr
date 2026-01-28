using System;
using System.Collections;
using System.Collections.Generic;
using Engine;
using RbEngine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISetting : UIBase
{
    public Button closeBtn;

    public TMP_Text heightText;
    public Button minusHeightBtn;
    public Button plusHeightBtn;

    public TMP_InputField emailInputField;
    public TMP_InputField phoneInputField;
    public TMP_InputField passwordInputField;

    public Button registerBtn;
    public Button sureBtn;

    private const int MIN_HEIGHT = 165;
    private const int MAX_HEIGHT = 190;

    public override void OnCreate(params object[] args)
    {
        closeBtn.AddOnPointerClick(OnBtnClick);
        minusHeightBtn.AddOnPointerClick(OnBtnClick);
        plusHeightBtn.AddOnPointerClick(OnBtnClick);
        sureBtn.AddOnPointerClick(OnBtnClick);
        registerBtn.AddOnPointerClick(OnBtnClick);

        RefreshView();
    }

    private void RefreshView()
    {
        var accountData = AccountData.Instance;
        if (emailInputField != null) emailInputField.text = accountData.GetEmail();
        if (phoneInputField != null) phoneInputField.text = accountData.GetPhone();
        if (passwordInputField != null) passwordInputField.text = accountData.GetPassword();
        UpdateHeightText();
    }

    private void OnBtnClick(Button btn)
    {
        if (btn == closeBtn)
        {
            Close();
        }
        else if (btn == minusHeightBtn)
        {
            if (AccountData.Instance.height > MIN_HEIGHT)
            {
                AccountData.Instance.height--;
                UpdateHeightText();
            }
        }
        else if (btn == plusHeightBtn)
        {
            if (AccountData.Instance.height < MAX_HEIGHT)
            {
                AccountData.Instance.height++;
                UpdateHeightText();
            }
        }
        else if (btn == sureBtn)
        {
            // TODO: Implement save logic
            Close();
        }
        else if (btn == registerBtn)
        {
            string email = emailInputField.text;
            string phone = phoneInputField.text;
            string password = passwordInputField.text;

            if (!LoginUtils.ValidateEmail(email))
            {
                return;
            }

            if (!LoginUtils.ValidateCredentials(phone, password))
            {
                return;
            }

            // 验证通过后再保存到 AccountData
            AccountData.Instance.email = email;
            AccountData.Instance.phone = phone;
            AccountData.Instance.password = password;

            // 验证通过，执行注册逻辑
            GameManager.Instance.ShowFlyTip("验证通过");
        }
    }

    private void UpdateHeightText()
    {
        if (heightText != null)
        {
            heightText.text = AccountData.Instance.GetHeight().ToString();
        }
    }
}
