using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Engine
{
    public class PathUtils
    {
        /// <summary>
        /// 获取运行时路径
        /// </summary>
        /// <returns></returns>
        public static string GetRuntimePath()
        {
#if UNITY_EDITOR
            return Application.dataPath + "/" + "Game";
#else
        return Application.persistentDataPath + "/" + "Game";
#endif
        }


        //    /// <summary>
        //    /// 获取包体内路径
        //    /// </summary>
        //    /// <returns></returns>
        //    public static string GetPackagePath()
        //    {
        //#if UNITY_EDITOR
        //        return Application.dataPath + "/StreamingAssets";
        //#elif UNITY_IPHONE
        //        return Application.dataPath + "/Raw";
        //#elif UNITY_ANDROID
        //        return "jar: file://" + Application.dataPath + "!/assets";
        //#endif

        //    }

        /// <summary>
        /// 获取包体内路径
        /// </summary>
        /// <returns></returns>
        public static string GetPackagePath()
        {
#if UNITY_EDITOR || UNITY_ANDROID || UNITY_WEBGL
            return Application.streamingAssetsPath;
#else
        return "file://" + Application.streamingAssetsPath;
#endif
        }
        /// <summary>
        /// 获取Lua路径
        /// </summary>
        /// <returns></returns>
        public static string GetScriptsPath()
        {
#if UNITY_EDITOR
            return GetRuntimePath() + "/Scripts";
#else
        return GetRuntimePath() + "/scripts";
#endif
        }
        /// <summary>
        /// 获取Res路径
        /// </summary>
        /// <returns></returns>
        public static string GetResPath()
        {
#if UNITY_EDITOR
            return GetRuntimePath() + "/Res";
#else
        return GetRuntimePath() + "/res";
#endif
        }
        /// <summary>
        /// 获取Storage路径
        /// </summary>
        /// <returns></returns>
        public static string GetStoragePath()
        {
#if UNITY_EDITOR
            return Application.dataPath + "/../Storage";
#else
        return GetRuntimePath() + "/Storage";
#endif
        }
        /// <summary>
        /// 普通路径转换成AssetPath
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string ConvertToAssetsPath(string path)
        {
            path = path.Replace("\\", "/");
            int index = path.IndexOf("Assets/");
            string tempStr = path.Substring(index, path.Length - index);
            return tempStr;
        }
    }
}