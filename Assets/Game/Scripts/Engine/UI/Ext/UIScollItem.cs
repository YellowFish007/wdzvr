using System;
using UnityEngine;
using UnityEngine.UI;
namespace Engine
{
    public class UIScollItem : MonoBehaviour
    {
        Action<int> action;

        private int index;

        [HideInInspector]
        public bool isInit = false;

        public void Init(GameObject scrollRectObj, Action<int> action = null)
        {
            Image img = gameObject.AddComponent<Image>();
            img.color = new Color(0, 0, 0, 0);

            ScrollButton scrollBtn = gameObject.AddComponent<ScrollButton>();
            scrollBtn.SetScrollRect(scrollRectObj);

            scrollBtn.AddOnClick(() =>
            {
                action?.Invoke(index);
            });

            isInit = true;
        }

        /// <summary>
        /// 设置索引
        /// </summary>
        /// <param name="index"></param>
        public void SetIndex(int index)
        {
            this.index = index;
        }
    }
}
