using UnityEngine;

namespace Engine
{
    /// <summary>
    /// 流程静态类，封装ProcedureManager的访问方法
    /// </summary>
    public static class Procedure
    {
        /// <summary>
        /// 切换到指定流程
        /// </summary>
        /// <typeparam name="T">流程类型</typeparam>
        public static void Change<T>(params object[] args) where T : ProcedureBase
        {
            ProcedureManager.Instance.ChangeProcedure<T>(args);
        }

        /// <summary>
        /// 添加流程
        /// </summary>
        /// <typeparam name="T">流程类型</typeparam>
        public static void Add(ProcedureBase procedure)
        {
            ProcedureManager.Instance.AddProcedure(procedure);
        }

        /// <summary>
        /// 获取指定类型的流程
        /// </summary>
        /// <typeparam name="T">流程类型</typeparam>
        /// <returns>指定类型的流程实例</returns>
        public static T Get<T>() where T : ProcedureBase
        {
            return ProcedureManager.Instance.GetProcedure<T>();
        }

        /// <summary>
        /// 清除
        /// </summary>
        public static void Clear() 
        {
            ProcedureManager.Instance.Clear();
        }
    }
}