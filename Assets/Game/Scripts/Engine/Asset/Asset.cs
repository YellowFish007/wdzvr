using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Engine
{
    /// <summary>
    /// 资源访问静态类，提供简化的资源访问API (类似于 Resources 类)
    /// </summary>
    public static class Asset
    {
        //------------------------------------------同步加载 (Load)------------------------------------------

        /// <summary>
        /// 加载资源 (泛型)
        /// </summary>
        public static T Load<T>(string path) where T : UnityEngine.Object
        {
            return AssetManager.Instance.LoadAsset<T>(path);
        }

        /// <summary>
        /// 加载资源 (非泛型)
        /// </summary>
        public static UnityEngine.Object Load(string path, Type type)
        {
            return AssetManager.Instance.LoadAsset(path, type);
        }

        /// <summary>
        /// 加载资源 (非泛型，仅路径)
        /// </summary>
        public static UnityEngine.Object Load(string path)
        {
            return Load(path, typeof(UnityEngine.Object));
        }

        //------------------------------------------异步加载 (LoadAsync)------------------------------------------

        /// <summary>
        /// 异步加载资源
        /// </summary>
        public static void LoadAsync<T>(string path, Action<T> callback) where T : UnityEngine.Object
        {
            AssetManager.Instance.LoadAssetAsync<T>(path, callback);
        }

        /// <summary>
        /// 异步加载资源 (通用)
        /// </summary>
        public static void LoadAsync(string path, Type type, Action<UnityEngine.Object> callback)
        {
            AssetManager.Instance.LoadAssetAsync(path, type, callback);
        }

        /// <summary>
        /// 异步加载资源 (通用，仅路径)
        /// </summary>
        public static void LoadAsync(string path, Action<UnityEngine.Object> callback)
        {
            LoadAsync(path, typeof(UnityEngine.Object), callback);
        }

        //------------------------------------------特定类型同步加载------------------------------------------

        public static TextAsset LoadTextAsset(string path)
        {
            return Load<TextAsset>(path);
        }

        public static GameObject LoadPrefab(string path)
        {
            return Load<GameObject>(path);
        }

        public static Sprite LoadSprite(string path)
        {
            return Load<Sprite>(path);
        }

        public static void LoadSpriteAsync(string path, Action<Sprite> callback)
        {
            LoadAsync<Sprite>(path, callback);
        }

        public static void LoadAudioClipAsync(string path, Action<AudioClip> callback)
        {
            LoadAsync<AudioClip>(path, callback);
        }
        
        //------------------------------------------兼容旧接口 (Optional)------------------------------------------
        // 为了兼容之前的代码，保留了 LoadAssetSync 和 LoadAssetAsync 的命名别名
        
        public static T LoadAssetSync<T>(string path) where T : UnityEngine.Object => Load<T>(path);
        public static void LoadAssetAsync<T>(string path, Action<T> callback) where T : UnityEngine.Object => LoadAsync<T>(path, callback);

        //------------------------------------------场景加载------------------------------------------

        /// <summary>
        /// 异步加载场景
        /// </summary>
        public static void LoadSceneAsync(string scenePath, Action<float> progressCallback = null, Action completedCallback = null)
        {
            AssetManager.Instance.LoadSceneAsync(scenePath, progressCallback, completedCallback);
        }
        
    }
}
