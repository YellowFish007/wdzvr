using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HttpConfig
{
    public static string HEAD = GameConfig.ServerUrl + "/";

    public static string Register = HEAD + "admin/users/register";

    public static string Login = HEAD + "admin/users/login";

    public static string Index = HEAD + "admin/users/index";
}
