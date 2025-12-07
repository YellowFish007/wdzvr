using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GenerateProto : MonoBehaviour
{
    [MenuItem("Tools/Generate Proto")]
    public static void Start()
    {
        EditorBatUtils.RunBat("BuildProto.bat", EditorPathUtils.TOOLS_PATH + "/Protoc3.18/");

        string protoPath = EditorPathUtils.TOOLS_PATH + "/Protoc3.18/Output/Csharp";

        //DirectoryUtils.CopyDirectory(protoPath, EditorPathUtils.SRC_PATH + "/HotFix/_/PbFile");
    }
}
