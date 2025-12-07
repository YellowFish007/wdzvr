using System;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using UnityEngine;
using UnityEngine.Networking;

namespace Engine
{
    public class HttpManager : Singleton<HttpManager>
    {
        private string Token = "";

        public void SetToken(string token)
        {
            Token = token;
        }

        public void Get(string url, Action<long, string> callback)
        {
            var request = UnityWebRequest.Get(url);
            request.timeout = 30;
            if (!string.IsNullOrEmpty(Token))
                request.SetRequestHeader("token", Token);
            var operation = request.SendWebRequest();
            operation.completed += (asyncOp) =>
            {
                if (request.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError($"HTTP错误 {request.responseCode}: {request.error}\nURL: {url}\n响应: {request.downloadHandler.text}");
                    callback?.Invoke(request.responseCode, request.error);
                }
                else
                {
                    callback?.Invoke(request.responseCode, request.downloadHandler.text);
                }
            };
        }

        public void Post(string url, Dictionary<string, string> formData, Action<long, string> callback)
        {
            var request = UnityWebRequest.Post(url, formData);
            request.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");
            if (!string.IsNullOrEmpty(Token))
                request.SetRequestHeader("token", Token);

            var operation = request.SendWebRequest();
            operation.completed += (asyncOp) =>
            {
                if (request.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError($"HTTP错误 {request.responseCode}: {request.error}\nURL: {url}\n响应: {request.downloadHandler.text}");
                    callback?.Invoke(request.responseCode, request.error);
                }
                else
                {
                    callback?.Invoke(request.responseCode, request.downloadHandler.text);
                }
            };
        }

        public void Post(string url, string jsonData, Action<long, string> callback)
        {
            // 使用 Put 方法发送 JSON 数据，或者使用 Post 配合 UploadHandler
            var request = new UnityWebRequest(url, "POST");
            // 设置 JSON 数据
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            if (!string.IsNullOrEmpty(Token))
                request.SetRequestHeader("token", Token);

            var operation = request.SendWebRequest();
            operation.completed += (asyncOp) =>
            {
                try
                {
                    if (request.result != UnityWebRequest.Result.Success)
                    {
                        Debug.LogError($"HTTP错误 {request.responseCode}: {request.error}\nURL: {url}\n响应: {request.downloadHandler.text}");
                        callback?.Invoke(request.responseCode, request.error);
                    }
                    else
                    {
                        if (request.responseCode == 200)
                        {
                            string jsonText = request.downloadHandler.text;
                            JsonData jsonDt = JsonMapper.ToObject(jsonText);
                            int code = int.Parse(jsonDt["code"].ToString());

                            callback?.Invoke(code, request.downloadHandler.text);
                        }
                        else
                        {
                            callback?.Invoke(request.responseCode, request.downloadHandler.text);
                        }
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError($"请求处理异常: {e.Message}");
                    callback?.Invoke(request.responseCode, e.Message);
                }
                finally
                {
                    // 重要：释放请求对象
                    request.Dispose();
                }
            };
        }

    }
}