using System.Collections;
using System.Collections.Generic;
using Engine;
using UnityEngine;

public class AccountData : Singleton<AccountData>
{
    public int roleId = 1001;

    public int GetRoleId()
    {
        return roleId;
    }
}
