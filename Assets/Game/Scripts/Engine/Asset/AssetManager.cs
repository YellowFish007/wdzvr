using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Engine
{
    public class AssetManager : SingletonGameObject<AssetManager>
    {
        public void Init()
        {
            Debug.Log("AssetManager: Initialized (Resources Mode)");
        }

        //------------------------------------------同步加载------------------------------------------

        /// <summary>
        /// 同步加载资源
        /// </summary>
        public T LoadAsset<T>(string path) where T : UnityEngine.Object
        {
            var asset = Resources.Load<T>(path);
            if (asset == null)
            {
                Debug.LogWarning($"[AssetManager] Failed to load asset '{path}'. Ensure it is in a 'Resources' folder and the path is relative without extension.");
            }
            return asset;
        }

        /// <summary>
        /// 同步加载通用资源
        /// </summary>
        public UnityEngine.Object LoadAsset(string path, Type type)
        {
            var asset = Resources.Load(path, type);
            if (asset == null)
            {
                Debug.LogWarning($"[AssetManager] Failed to load asset '{path}'. Ensure it is in a 'Resources' folder and the path is relative without extension.");
            }
            return asset;
        }

        //------------------------------------------异步加载------------------------------------------

        /// <summary>
        /// 异步加载资源
        /// </summary>
        public void LoadAssetAsync<T>(string path, Action<T> callback) where T : UnityEngine.Object
        {
            StartCoroutine(LoadAssetAsyncRoutine(path, callback));
        }

        private IEnumerator LoadAssetAsyncRoutine<T>(string path, Action<T> callback) where T : UnityEngine.Object
        {
            ResourceRequest request = Resources.LoadAsync<T>(path);
            yield return request;
            
            if (request.asset == null)
            {
                Debug.LogWarning($"[AssetManager] Failed to load asset async '{path}'.");
            }
            callback?.Invoke(request.asset as T);
        }

        /// <summary>
        /// 异步加载通用资源
        /// </summary>
        public void LoadAssetAsync(string path, Type type, Action<UnityEngine.Object> callback)
        {
            StartCoroutine(LoadAssetAsyncRoutine(path, type, callback));
        }

        private IEnumerator LoadAssetAsyncRoutine(string path, Type type, Action<UnityEngine.Object> callback)
        {
            ResourceRequest request = Resources.LoadAsync(path, type);
            yield return request;

            if (request.asset == null)
            {
                Debug.LogWarning($"[AssetManager] Failed to load asset async '{path}'.");
            }
            callback?.Invoke(request.asset);
        }

        /// <summary>
        /// 异步加载场景
        /// </summary>
        public void LoadSceneAsync(string scenePath, Action<float> progressCallback = null, Action completedCallback = null)
        {
            StartCoroutine(LoadSceneAsyncRoutine(scenePath, progressCallback, completedCallback));
        }

        private IEnumerator LoadSceneAsyncRoutine(string scenePath, Action<float> progressCallback, Action completedCallback)
        {
            AsyncOperation op = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(scenePath);
            if (op == null)
            {
                Debug.LogError($"[AssetManager] Failed to load scene '{scenePath}'. Ensure it is added to Build Settings.");
                yield break;
            }

            while (!op.isDone)
            {
                progressCallback?.Invoke(op.progress);
                yield return null;
            }

            progressCallback?.Invoke(1.0f);
            completedCallback?.Invoke();
        }
    }
}
