using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Networking;
namespace Engine
{
    public class ZipManager : SingletonGameObject<ZipManager>
    {

        private const string UnZipKey = "UnZipKey";

        private const string LocalFileName = "Native";

        /// <summary>
        /// 是否是第一次安装，是否需要解压
        /// </summary>
        public bool IsNeedUnZip()
        {
            bool needUnZip = !PlayerPrefs.HasKey("UnZipKey");
            return needUnZip;
        }

        /// <summary>
        /// 开始解压
        /// </summary>
        /// <param name="action">是否完成，完成进度，是否有错</param>
        public void StartUnZip(Action<bool, float, string> action)
        {
            LoadNativeFile(LocalFileName, delegate (bool isSuccess, byte[] data)
            {
                if (isSuccess)
                {
                    //创建解压界面，开始解压
                    ZipUtils.UnZipByByte(data, PathUtils.GetRuntimePath(), action);
                }
                else
                {
                    //返回解压失败的结果，重新开始解压或者重新下载安装包
                    action(false, 0, "error");
                    //弹出错误提示
                }
            });
        }

        /// <summary>
        /// 保存zipkey
        /// </summary>
        public void SaveZipKey() 
        {
            //设置值
            PlayerPrefs.SetString(UnZipKey, "");
        }

        public void LoadNativeFile(string nativeZipFileName, Action<bool, byte[]> action)
        {
            StartCoroutine(LoadNativeFileIEnumerator(nativeZipFileName, delegate (bool isSuccess, byte[] data)
            {
                if (isSuccess)
                {
                    action(true, data);
                }
                else
                {
                    //返回解压失败的结果，重新开始解压或者重新下载安装包
                    action(false, null);
                    //弹出错误提示
                }
            }));
        }
        /// <summary>
        /// 从安装包获取文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IEnumerator LoadNativeFileIEnumerator(string fileName, Action<bool, byte[]> action)
        {
            string path = PathUtils.GetPackagePath() + "/" + fileName;

            UnityWebRequest request = UnityWebRequest.Get(path);
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                action(true, request.downloadHandler.data);
            }
            else
            {
                Debug.LogError(path + "出错，获取Zip数据出错" + request.error);
                action(false, null);
            }
        }

    }
}