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

}
