using System;
using System.Collections.Generic;
using UnityEngine;

namespace Engine
{
    /// <summary>
    /// Http静态类，封装HttpManager的访问方法
    /// </summary>
    public static class Http
    {
        /// <summary>
        /// 发送GET请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="callback">回调函数</param>
        public static void Get(string url, Action<long, string> callback)
        {
            HttpManager.Instance.Get(url, callback);
        }
        public static void Post(string url, string body, Action<long, string> callback)
        {
            HttpManager.Instance.Post(url, body, callback);
        }

        public static void Post(string url, Dictionary<string, string> data, Action<long, string> callback)
        {
            HttpManager.Instance.Post(url, data, callback);
        }

        public static void SetToken(string token)
        {
            HttpManager.Instance.SetToken(token);
        }
    }
}