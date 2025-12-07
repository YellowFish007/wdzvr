using System;
using UnityEngine;

namespace Engine
{
    /// <summary>
    /// 数据访问静态类，提供简化的数据访问API
    /// </summary>
    public static class Data
    {
        /// <summary>
        /// 获取指定类型的数据
        /// </summary>
        public static T Get<T>() where T : class
        {
            return DataManager.Instance.GetData<T>();
        }

        /// <summary>
        /// 设置指定类型的数据
        /// </summary>
        public static void Set<T>(T data) where T : class
        {
            DataManager.Instance.RegisterData(data);
        }

        /// <summary>
        /// 清除指定类型的数据
        /// </summary>
        public static void Clear<T>() where T : class
        {
            DataManager.Instance.ClearData<T>();
        }

        /// <summary>
        /// 清除所有数据
        /// </summary>
        public static void ClearAll()
        {
            DataManager.Instance.ClearAllData();
        }

        /// <summary>
        /// 保存指定类型的数据
        /// </summary>
        public static void Save<T>() where T : class
        {
            DataManager.Instance.SaveData<T>();
        }

        /// <summary>
        /// 加载指定类型的数据
        /// </summary>
        public static void Load<T>() where T : class, new()
        {
            DataManager.Instance.LoadData<T>();
        }
    }
}