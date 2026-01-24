using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Engine
{
    public class SDKManager : Singleton<SDKManager>
    {
        BaseSdk sdk;

        //返回按钮
        private Action backAction;

        public void Init()
        {
#if UNITY_ANDROID
            sdk = new AndroidSdk();
#elif UNITY_IPHONE
                sdk = new IOSSdk();
#else
                sdk = new WindowsSdk();
#endif
        }
        /// <summary>
        /// 获取手机电量
        /// </summary>
        /// <returns></returns>
        public float GetBattery()
        {
            return SystemInfo.batteryLevel;
        }

        /// <summary>
        /// 获取剪切板内容
        /// </summary>
        /// <returns></returns>
        public string GetClipBoard()
        {
            return GUIUtility.systemCopyBuffer;
        }

        /// <summary>
        /// 判断是否有网络连接
        /// </summary>
        /// <returns>true: 有网络, false: 无网络</returns>
        public bool IsNetworkAvailable()
        {
            return Application.internetReachability != NetworkReachability.NotReachable;
        }

        /// <summary>
        /// 添加返回按钮监听
        /// </summary>
        /// <returns></returns>
        public void AddBackBtnListener(Action backAction)
        {
            this.backAction = backAction;
        }

        void Update()
        {
#if UNITY_ANDROID
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                //安卓返回键
                backAction();
            }
#endif
        }
    }
}