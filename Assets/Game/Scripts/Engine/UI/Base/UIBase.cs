using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace Engine
{
    public enum UIType
    {
        BG,
        Noraml,
        Dialog,
        Tip,
    }

    public abstract class UIBase : MonoBehaviour
    {
        //UI显示类型
        public UIType uiType = UIType.Noraml;
        //是否显示阴影
        public bool isShowBlackMask;
        //是否透传
        public bool isRaycastTarget = true;
        //是否驻留
        public bool isDontDestroy = false;

        //UI的ID
        public string id { get; set; }

        [HideInInspector]
        //是否初始化
        public bool IsInit = false;

        //----------------------------生命周期----------------------------
        private void Awake()
        {
        }

        //生命周期-预加载
        public virtual void OnPreload(Action loadCallBack) { loadCallBack(); }
        //生命周期-创建
        public virtual void OnCreate(params object[] args) { }
        //生命周期-进入
        public virtual void OnEnter(params object[] args) { }
        //生命周期-退出
        public virtual void OnExit() { }
        //生命周期-关闭
        public virtual void OnClose() { }
        //刷新界面
        public virtual void OnFresh(params object[] args) { }
        //刷新语言
        public virtual void OnFreshLanguage() { }

        /// <summary>
        /// 快速关闭
        /// </summary>
        protected void CloseUI()
        {
            UIManager.Instance.CloseUIByID(id);
        }

        /// <summary>
        /// 重置位置
        /// </summary>
        public void ResetTransform()
        {
            RectTransform rectTrans = gameObject.GetComponent<RectTransform>();
            rectTrans.anchorMin = new Vector2(0.5f, 0.5f);
            rectTrans.anchorMax = new Vector2(0.5f, 0.5f);
            rectTrans.anchoredPosition3D = new Vector3(0.5f, 0.5f, 0);
            rectTrans.sizeDelta = Vector2.zero;
            rectTrans.anchoredPosition = Vector2.zero;
            rectTrans.localScale = Vector3.one;
        }

        //添加Canvas
        public void AddBaseCanvas()
        {
            //添加Canvas
            gameObject.AddComponent<Canvas>();
            //添加GraphicRaycaster
            gameObject.AddComponent<GraphicRaycaster>();

            Image image = gameObject.AddComponent<Image>();
            //是否透传点击事件
            image.raycastTarget = isRaycastTarget;

            //是否显示阴影
            if (isShowBlackMask)
            {
                image.color = new Color(0, 0, 0, 204.0f / 255.0f);
            }
            else
            {
                image.color = new Color(0, 0, 0, 0);
            }
        }


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