using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace Engine
{
    /// <summary>
    /// 数据管理器，负责管理游戏运行时的数据
    /// </summary>
    public class DataManager : Singleton<DataManager>
    {
        private Dictionary<Type, object> _dataDict = new Dictionary<Type, object>();
        protected void Awake()
        {
            // 初始化时不需要特殊处理
        }

        /// <summary>
        /// 注册数据
        /// </summary>
        public void RegisterData<T>(T data) where T : class
        {
            var type = typeof(T);
            if (_dataDict.ContainsKey(type))
            {
                _dataDict[type] = data;
            }
            else
            {
                _dataDict.Add(type, data);
            }
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        public T GetData<T>() where T : class
        {
            var type = typeof(T);
            if (_dataDict.TryGetValue(type, out object data))
            {
                return data as T;
            }
            return null;
        }

        /// <summary>
        /// 清除指定类型的数据
        /// </summary>
        public void ClearData<T>() where T : class
        {
            var type = typeof(T);
            if (_dataDict.ContainsKey(type))
            {
                _dataDict.Remove(type);
            }
        }

        /// <summary>
        /// 清除所有数据
        /// </summary>
        public void ClearAllData()
        {
            _dataDict.Clear();
        }

        /// <summary>
        /// 保存指定类型的数据
        /// </summary>
        public void SaveData<T>() where T : class
        {
            var data = GetData<T>();
            if (data != null)
            {
                var json = JsonUtility.ToJson(data);
                PlayerPrefs.SetString(typeof(T).Name, json);
                PlayerPrefs.Save();
            }
        }

        /// <summary>
        /// 加载指定类型的数据
        /// </summary>
        public void LoadData<T>() where T : class, new()
        {
            var key = typeof(T).Name;
            if (PlayerPrefs.HasKey(key))
            {
                var json = PlayerPrefs.GetString(key);
                var data = JsonUtility.FromJson<T>(json);
                RegisterData(data);
            }
            else
            {
                var data = new T();
                RegisterData(data);
                SaveData<T>();
            }
        }
    }
}