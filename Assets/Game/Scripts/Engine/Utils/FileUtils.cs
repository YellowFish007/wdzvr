using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using UnityEngine.Networking;
namespace Engine
{
    public class FileUtils
    {
        /// <summary>
        /// 获取文件忽略某些文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="extensionNames"></param>
        /// <returns></returns>
        public static string[] GetFilesIgnore(string path, params string[] extensionNames)
        {
            List<string> fileList = new List<string>();
            GetFiles(path, fileList);

            List<string> newFileList = new List<string>();

            for (int i = 0; i < fileList.Count; i++)
            {
                foreach (var extensionName in extensionNames)
                {
                    if (!fileList[i].EndsWith(extensionName))
                    {
                        newFileList.Add(fileList[i].Replace("\\", "/"));
                    }
                }
            }

            return newFileList.ToArray();
        }

        /// <summary>
        /// 获取所有的文件
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string[] GetFiles(string path)
        {
            List<string> fileList = new List<string>();

            GetFiles(path, fileList);

            return fileList.ToArray();
        }
        /// <summary>
        /// 获取所有文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="fileList"></param>
        public static void GetFiles(string path, List<string> fileList)
        {
            string[] files = Directory.GetFiles(path);
            string[] dirs = Directory.GetDirectories(path);

            foreach (var file in files)
            {
                fileList.Add(Path.Combine(file).Replace("\\", "/"));
            }
            foreach (var dir in dirs)
            {
                GetFiles(dir, fileList);
            }
        }

        /// <summary>
        /// 计算文件的MD5值
        /// </summary>
        public static string GetFileMd5(string file)
        {
            try
            {
                FileStream fs = new FileStream(file, FileMode.Open);
                System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] retVal = md5.ComputeHash(fs);
                fs.Close();

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("md5file() fail, error:" + ex.Message);
            }
        }

        /// <summary>
        /// 保存文本
        /// </summary>
        /// <param name="path"></param>
        /// <param name="content"></param>
        public static void SaveText(string path, string content)
        {
            File.WriteAllText(path, content);
        }
        public static string GetText(string path)
        {
            if (File.Exists(path))
            {
                return File.ReadAllText(path);
            }
            else
            {
                Debug.LogError("FileUtils 文件不存在 path : " + path);
                return "";
            }
        }

    }
}