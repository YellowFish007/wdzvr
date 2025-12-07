using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class EditorPathUtils
{
    public static string TOOLS_PATH { get { return Path.Combine(Application.dataPath + "/../Tools"); } }
    public static string SRC_PATH { get { return Path.Combine(Application.dataPath + "/Game/Scripts"); } }
    public static string RES_PATH { get { return Path.Combine(Application.dataPath + "/Game/Res"); } }
    public static string SCENES_PATH { get { return Path.Combine(Application.dataPath + "/Game/Scenes"); } }
    public static string PACK_PATH { get { return Path.Combine(Application.dataPath + "/../" + "OUT_PACK"); } }
    public static string Excel_PATH { get { return Path.Combine(Application.dataPath + "/../Tools/Excel"); } }

    public static string PlatformName
    {
        get
        {
            switch (EditorUserBuildSettings.activeBuildTarget)
            {
                case BuildTarget.Android:
                    return "android";
                case BuildTarget.iOS:
                    return "ios";
            }
            return "windows";
        }
    }
}
