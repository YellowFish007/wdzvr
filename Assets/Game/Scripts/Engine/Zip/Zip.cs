using UnityEngine;
using System;

namespace Engine
{
    /// <summary>
    /// Zip静态类，封装ZipManager的访问方法
    /// </summary>
    public static class Zip
    {   
        /// <summary>
        /// 是否需要解压缩
        /// </summary>
        /// <returns>是否需要解压缩</returns>
        public static bool IsNeedUnZip()
        {
            return ZipManager.Instance.IsNeedUnZip();
        }

        /// <summary>
        /// 开始解压缩
        /// </summary>
        /// <param name="action">解压进度回调（是否完成，完成进度，是否有错）</param>
        public static void StartUnZip(Action<bool, float, string> action)
        {
            ZipManager.Instance.StartUnZip(action);
        }

        public static void LoadNativeFile(string nativeZipFileName, Action<bool, byte[]> action)
        {
            ZipManager.Instance.LoadNativeFile(nativeZipFileName,action);
        }

        /// <summary>
        /// 保存解压标记
        /// </summary>
        public static void SaveZipKey()
        {
            ZipManager.Instance.SaveZipKey();
        }
    }
}