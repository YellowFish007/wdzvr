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

    private int m_CurrentHeight = 170;
    private const int MIN_HEIGHT = 165;
    private const int MAX_HEIGHT = 190;

    public override void OnCreate(params object[] args)
    {
        closeBtn.AddOnPointerClick(OnBtnClick);
        minusHeightBtn.AddOnPointerClick(OnBtnClick);
        plusHeightBtn.AddOnPointerClick(OnBtnClick);
        sureBtn.AddOnPointerClick(OnBtnClick);
        registerBtn.AddOnPointerClick(OnBtnClick);

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
            if (m_CurrentHeight > MIN_HEIGHT)
            {
                m_CurrentHeight--;
                UpdateHeightText();
            }
        }
        else if (btn == plusHeightBtn)
        {
            if (m_CurrentHeight < MAX_HEIGHT)
            {
                m_CurrentHeight++;
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

            // 验证通过，执行注册逻辑
            GameManager.Instance.ShowFlyTip("验证通过");
        }
    }

    private void UpdateHeightText()
    {
        if (heightText != null)
        {
            heightText.text = m_CurrentHeight.ToString();
        }
    }
}
