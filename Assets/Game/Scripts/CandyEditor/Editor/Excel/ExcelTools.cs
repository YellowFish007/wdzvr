using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
public class ExcelTools : Editor
{
    [MenuItem("Tools/生成Excel")]
    public static void Start()
    {
        //DirectoryUtils.CopyDirectory("D:/Workspace/Git/match3-art/策划表", EditorPathUtils.TOOLS_PATH + "/Excel/Datas");
        //DirectoryUtils.CopyDirectory("E:/Git/match3-art/策划表", EditorPathUtils.TOOLS_PATH + "/Excel/Datas");

        EditorBatUtils.RunBat("gen.bat", EditorPathUtils.TOOLS_PATH + "/Excel/");
        
        //SyncExcelConfig();
    }

    private static void SyncExcelConfig()
    {
        string excelFolderPath = Application.dataPath + "/Game/Res/RawAssets/Text/Excel";
        string configFilePath = Application.dataPath + "/Game/Scripts/HotFix/Config/ExcelConfig.cs";

        if (Directory.Exists(excelFolderPath))
        {
            string[] files = Directory.GetFiles(excelFolderPath, "*.json");
            List<string> names = files.Select(file => Path.GetFileNameWithoutExtension(file)).ToList();

            string content = "using System.Collections.Generic;\n\n";
            content += "public class ExcelConfig\n";
            content += "{\n";
            content += "    public static List<string> Names = new List<string>()\n";
            content += "    {\n";
            foreach (string name in names)
            {
                content += $"        \"{name}\",\n";
            }
            content += "    };\n";
            content += "}\n";

            File.WriteAllText(configFilePath, content);
            AssetDatabase.Refresh();
            Debug.Log("Excel配置同步成功！");
        }
        else
        {
            Debug.LogError("找不到Excel文件夹：" + excelFolderPath);
        }
    }
}
