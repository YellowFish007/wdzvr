using System;
using LitJson;
using UnityEngine;

namespace Engine
{
    public static class JsonParserUtils
    {
        public static T Parse<T>(string jsonText) where T : class
        {
            try
            {
                if (string.IsNullOrEmpty(jsonText))
                    return null;
                JsonData jsonDt = JsonMapper.ToObject(jsonText);
                return JsonMapper.ToObject<T>(jsonDt["data"].ToJson());
            }
            catch (Exception e)
            {
                Debug.LogError($"JSON解析失败: {e.Message}");
                return null;
            }
        }

        public static string ToJson<T>(T obj) where T : class
        {
            try
            {
                if (obj == null)
                    return string.Empty;

                return JsonMapper.ToJson(obj);
            }
            catch (Exception e)
            {
                Debug.LogError($"JSON序列化失败: {e.Message}");
                return string.Empty;
            }
        }
    }
}