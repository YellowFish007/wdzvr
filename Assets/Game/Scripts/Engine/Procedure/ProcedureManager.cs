using System;
using System.Collections.Generic;
using UnityEngine;

namespace Engine
{
    public class ProcedureManager : Singleton<ProcedureManager>
    {
        private ProcedureBase m_CurrentProcedure;
        private readonly Dictionary<Type, ProcedureBase> m_Procedures = new Dictionary<Type, ProcedureBase>();
        private float m_LastTime;

        private void Start()
        {
            m_LastTime = Time.time;
        }

        private void Update()
        {
            if (m_CurrentProcedure == null)
                return;

            float nowTime = Time.time;
            float elapseSeconds = nowTime - m_LastTime;
            m_LastTime = nowTime;

            m_CurrentProcedure.OnUpdate(elapseSeconds, elapseSeconds);
        }

        /// <summary>
        /// 是否存在流程。
        /// </summary>
        /// <typeparam name="T">流程类型。</typeparam>
        /// <returns>是否存在流程。</returns>
        public bool HasProcedure<T>() where T : ProcedureBase
        {
            return m_Procedures.ContainsKey(typeof(T));
        }

        /// <summary>
        /// 获取流程。
        /// </summary>
        /// <typeparam name="T">流程类型。</typeparam>
        /// <returns>要获取的流程。</returns>
        public T GetProcedure<T>() where T : ProcedureBase
        {
            Type procedureType = typeof(T);
            if (m_Procedures.TryGetValue(procedureType, out ProcedureBase procedure))
            {
                return (T)procedure;
            }

            return null;
        }

        /// <summary>
        /// 获取当前流程。
        /// </summary>
        /// <returns>当前流程。</returns>
        public ProcedureBase GetCurrentProcedure()
        {
            return m_CurrentProcedure;
        }

        /// <summary>
        /// 添加流程。
        /// </summary>
        /// <param name="procedure">要添加的流程。</param>
        public void AddProcedure(ProcedureBase procedure)
        {
            if (procedure == null)
            {
                throw new Exception("Procedure is invalid.");
            }

            Type procedureType = procedure.GetType();
            if (m_Procedures.ContainsKey(procedureType))
            {
                throw new Exception($"Procedure '{procedureType.FullName}' is already exist.");
            }

            procedure.OnInit();
            m_Procedures.Add(procedureType, procedure);
        }

        /// <summary>
        /// 切换当前流程。
        /// </summary>
        /// <typeparam name="T">要切换到的流程类型。</typeparam>
        /// <param name="procedureData">传递给新流程的参数</param>
        public void ChangeProcedure<T>(params object[] args) where T : ProcedureBase
        {
            Type procedureType = typeof(T);
            if (!m_Procedures.TryGetValue(procedureType, out ProcedureBase procedure))
            {
                throw new Exception($"Procedure '{procedureType.FullName}' is not exist.");
            }

            if (m_CurrentProcedure != null)
            {
                m_CurrentProcedure.OnLeave();
            }

            m_CurrentProcedure = procedure;
            m_CurrentProcedure.OnEnter(args);
        }

        private void OnDestroy()
        {
            Clear();
        }

        public void Clear() 
        {
            if (m_CurrentProcedure != null)
            {
                m_CurrentProcedure.OnLeave();
                m_CurrentProcedure = null;
            }
            foreach (var procedure in m_Procedures.Values)
            {
                procedure.OnDestroy();
            }

            m_Procedures.Clear();
        }
    }
}