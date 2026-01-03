using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace Engine
{
    public abstract class UIBase : MonoBehaviour
    {
        public virtual string Name { get; set; }

        public virtual void OnOpen() { }

        /// <summary>
        /// 设置可见
        /// </summary>
        /// <param name="active"></param>
        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }

    }
}