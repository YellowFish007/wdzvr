using System.Collections;
using System.Collections.Generic;
using Engine;
using UnityEngine;

public class AccountData : Singleton<AccountData>
{
    // id
    public int id = 12423567;

    // 昵称
    public string nickname;

    // 等级
    public int level = 1;

    // 经验值
    public long exp = 0;

    // 金币
    public long gold = 0;

    // 钻石
    public long diamond = 0;

    // 头像图标
    public string headIcon = "icon_avatar_04";

    // 创建时间
    public long createTime;

    // 上次登录时间
    public long lastLoginTime;

    // 身高
    public int height = 170;
    // 邮箱
    public string email = "";
    // 手机号
    public string phone = "";
    // 密码
    public string password = "";

    public int GetId()
    {
        return id;
    }

    public string GetNickname()
    {
        return nickname;
    }

    public int GetLevel()
    {
        return level;
    }

    public long GetExp()
    {
        return exp;
    }

    public long GetGold()
    {
        return gold;
    }

    public long GetDiamond()
    {
        return diamond;
    }

    public string GetHeadIcon()
    {
        return headIcon;
    }

    public long GetCreateTime()
    {
        return createTime;
    }

    public long GetLastLoginTime()
    {
        return lastLoginTime;
    }

    public int GetHeight()
    {
        return height;
    }

    public string GetEmail()
    {
        return email;
    }

    public string GetPhone()
    {
        return phone;
    }

    public string GetPassword()
    {
        return password;
    }

}
