using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LoginUtils
{
    public static bool ValidateCredentials(string phoneNum, string password)
    {
        // 手机号验证
        if (string.IsNullOrEmpty(phoneNum) || phoneNum.Length != 11)
        {
            GameManager.Instance.ShowFlyTip("手机号长度应为11位");
            return false;
        }

        foreach (char c in phoneNum)
        {
            if (!char.IsDigit(c))
            {
                GameManager.Instance.ShowFlyTip("手机号只能包含数字");
                return false;
            }
        }

        // 密码验证
        if (password.Length < 8 || password.Length > 20)
        {
            GameManager.Instance.ShowFlyTip("密码长度应为8-20位");
            return false;
        }

        bool hasLetter = false;
        bool hasDigit = false;

        foreach (char c in password)
        {
            if (char.IsLetter(c)) hasLetter = true;
            if (char.IsDigit(c)) hasDigit = true;
        }

        if (!hasLetter || !hasDigit)
        {
            GameManager.Instance.ShowFlyTip("密码必须包含字母和数字");
            return false;
        }

        return true;
    }

    public static bool ValidateEmail(string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            GameManager.Instance.ShowFlyTip("邮箱不能为空");
            return false;
        }

        // 简单的邮箱格式验证
        if (!System.Text.RegularExpressions.Regex.IsMatch(email, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"))
        {
            GameManager.Instance.ShowFlyTip("邮箱格式不正确");
            return false;
        }

        return true;
    }
}
