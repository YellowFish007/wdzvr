using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Engine
{
    public static class Scene
    {
        public static void LoadSceneAsync<T>(Action<float> progressCallback = null, Action completedCallback = null, params object[] args) where T : SceneBase
        {
            SceneManager.Instance.LoadSceneAsync<T>(progressCallback, completedCallback, args);
        }

        public static T Get<T>() where T : SceneBase
        {
            return SceneManager.Instance.GetScene<T>();
        }

        public static void OpenUI(string name)
        {
            SceneManager.Instance.OpenUI(name);
        }
    }
}