using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Engine;
using YooAsset;
using System;

namespace Engine
{
    public class SceneManager : Singleton<SceneManager>
    {
        private SceneBase m_CurrentScene;

        /// <summary>
        /// 异步加载场景
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="progressCallback"></param>
        /// <param name="completedCallback"></param>
        public void LoadSceneAsync<T>(Action<float> progressCallback = null, Action completedCallback = null, params object[] args) where T : SceneBase
        {
            if (m_CurrentScene != null)
            {
                m_CurrentScene.OnClose();
            }

            string sceneName = typeof(T).Name;
            LoadSceneAsync(sceneName, progressCallback, delegate ()
            {
                GameObject obj = FindRootObjectByName(sceneName);
                completedCallback?.Invoke();
                m_CurrentScene = obj.GetComponent<SceneBase>();
                OnPreloadSceneRes(args);
            });
        }

        public T GetScene<T>() where T : SceneBase
        {
            return m_CurrentScene as T;
        }

        /// <summary>
        /// 异步加载场景
        /// </summary>
        private void LoadSceneAsync(string scenePath, Action<float> progressCallback = null, Action completedCallback = null)
        {
            Asset.LoadSceneAsync(scenePath, progressCallback, completedCallback);
        }

        /// <summary>
        /// 获取当前场景的根节点
        /// </summary>
        private GameObject[] GetCurrentSceneRootObjects()
        {
            UnityEngine.SceneManagement.Scene currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
            return currentScene.GetRootGameObjects();
        }
        /// <summary>
        /// 查找当前场景中指定名称的根节点
        /// </summary>
        private GameObject FindRootObjectByName(string name)
        {
            var rootObjects = GetCurrentSceneRootObjects();
            foreach (var obj in rootObjects)
            {
                if (obj.name == name)
                    return obj;
            }
            return null;
        }

        private void OnPreloadSceneRes(params object[] args)
        {
            m_CurrentScene.OnPreload(delegate ()
             {
                 m_CurrentScene.OnCreate(args);
             });
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                m_CurrentScene.OnTouchDown();
            }
            if (Input.GetMouseButtonUp(0))
            {
                m_CurrentScene.OnTouchUp();
            }
        }

        public void OpenUI(string name)
        {
            m_CurrentScene.GetUIRoot().OpenUI(name);
        }
    }
}