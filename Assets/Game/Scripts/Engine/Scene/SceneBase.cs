using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Engine
{
    public class SceneBase : MonoBehaviour
    {
        public virtual void OnPreload(Action loadCallBack) { loadCallBack(); }
        public virtual void OnCreate(params object[] args) { }
        public virtual void OnClose() { }

        public virtual void OnTouchDown() { }
        public virtual void OnTouchUp() { }

    }
}