using UnityEngine;
using System.Linq;
using System.Reflection;
using System;
using DG.Tweening.Core.Easing;

namespace Engine
{
    public class ProcedureStartGame : ProcedureBase
    {
        public override void OnEnter(params object[] args)
        {
            base.OnEnter();

            StartGame();
        }

        public override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
        }

        void StartGame()
        {
            GameManager.Instance.Init();

            Procedure.Clear();
        }

    }
}