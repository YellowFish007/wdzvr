using UnityEngine;
namespace Engine
{
    public abstract class ProcedureBase
    {
        /// <summary>
        /// 流程初始化。
        /// </summary>
        public virtual void OnInit() { }

        /// <summary>
        /// 进入流程
        /// </summary>
        /// <param name="procedureData">流程参数</param>
        public virtual void OnEnter(params object[] args)
        {
        }

        /// <summary>
        /// 离开流程。
        /// </summary>
        /// <param name="procedureOwner">流程持有者。</param>
        public virtual void OnLeave() { }

        /// <summary>
        /// 流程轮询。
        /// </summary>
        /// <param name="procedureOwner">流程持有者。</param>
        /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
        /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
        public virtual void OnUpdate(float elapseSeconds, float realElapseSeconds) { }

        /// <summary>
        /// 流程销毁。
        /// </summary>
        public virtual void OnDestroy() { }
    }
}