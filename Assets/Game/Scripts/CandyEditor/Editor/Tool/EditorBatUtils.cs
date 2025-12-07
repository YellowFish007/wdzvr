using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class EditorBatUtils
{
    public static void RunBat(string batFile, string workingDir)
    {
        var path = FormatPath(workingDir + batFile);
        if (!System.IO.File.Exists(path))
        {
            Debug.LogError("bat�ļ������ڣ�" + path);
        }
        else
        {
            System.Diagnostics.Process proc = null;
            try
            {
                proc = new System.Diagnostics.Process();
                proc.StartInfo.WorkingDirectory = workingDir;
                proc.StartInfo.FileName = batFile;
                //proc.StartInfo.Arguments = args;
                //proc.StartInfo.CreateNoWindow = true;
                //proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;//disable dos window
                proc.Start();
                proc.WaitForExit();
                proc.Close();
            }
            catch (System.Exception ex)
            {
                Debug.LogFormat("Exception Occurred :{0},{1}", ex.Message, ex.StackTrace.ToString());
            }
        }
    }

    static string FormatPath(string path)
    {
        path = path.Replace("/", "\\");
        if (Application.platform == RuntimePlatform.OSXEditor)
            path = path.Replace("\\", "/");
        return path;
    }
}
