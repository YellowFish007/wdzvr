using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Engine
{
    /// <summary>
    /// 资源访问静态类，提供简化的资源访问API
    /// </summary>
    public static class Asset
    {
        //public static readonly string PrefabPath = "Assets/Game/Res/Prefabs/";
        //public static readonly string TextPath = "Assets/Game/Res/RawAssets/Text/";
        //public static readonly string AudioPath = "Assets/Game/Res/RawAssets/Audio/";
        //public static readonly string TexturePath = "Assets/Game/Res/RawAssets/Texture/";
        //public static readonly string ScenePath = "Assets/Game/Res/Scenes/";


        //------------------------------------------同步简化------------------------------------------
        public static TextAsset LoadTextAssetSync(string path)
        {
            return LoadAssetSync<TextAsset>(path);
        }
        public static Sprite LoadSpriteSync(string path)
        {
            return LoadAssetSync<Sprite>(path);
        }
        //------------------------------------------异步简化------------------------------------------
        public static void LoadPrefabAsync(string path, Action<GameObject> callBack)
        {
            LoadAssetAsync<GameObject>(path, callBack);
        }

        public static void LoadTextAssetAsync(string path, Action<TextAsset> callBack)
        {
            LoadAssetAsync<TextAsset>(path, callBack);
        }

        public static void LoadAudioClipAsync(string path, Action<AudioClip> callBack)
        {
            LoadAssetAsync<AudioClip>(path, callBack);
        }

        public static void LoadSpriteAsync(string path, Action<Sprite> callBack)
        {
            LoadAssetAsync<Texture2D>(path, delegate (Texture2D texture)
            {
                Sprite sp = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                callBack(sp);
            });
        }

        //------------------------------------------同步方法------------------------------------------

        public static T LoadAssetSync<T>(string path) where T : UnityEngine.Object
        {
            if (YooAssetManager.Instance == null)
            {
                Debug.LogError($"Asset.LoadAssetSync: YooAssetManager.Instance is null! Cannot load {path}");
                return null;
            }
            return YooAssetManager.Instance.LoadAssetSync<T>(path);
        }

        //------------------------------------------异步方法------------------------------------------

        /// <summary>
        /// 异步获取资源
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <param name="callBack"></param>
        public static void LoadAssetAsync<T>(string path, Action<T> callBack) where T : UnityEngine.Object
        {
            YooAssetManager.Instance.LoadAssetAsync<T>(path, callBack);
        }

        /// <summary>
        /// 异步获取通用资源
        /// </summary>
        /// <param name="path"></param>
        /// <param name="callBack"></param>
        public static void LoadAssetAsync(string path, Action<UnityEngine.Object> callBack)
        {
            YooAssetManager.Instance.LoadAssetAsync(path, callBack);
        }

        /// <summary>
        /// 异步获取多个资源
        /// </summary>
        /// <param name="paths"></param>
        /// <param name="callBack"></param>
        public static async void LoadMultipleAssetsAsync(List<string> paths, Action<List<UnityEngine.Object>> callBack)
        {
            var tasks = new List<Task<UnityEngine.Object>>();
            foreach (var path in paths)
            {
                var tcs = new TaskCompletionSource<UnityEngine.Object>();
                LoadAssetAsync(path, (asset) => tcs.SetResult(asset));
                tasks.Add(tcs.Task);
            }

            var results = await Task.WhenAll(tasks);
            callBack?.Invoke(new List<UnityEngine.Object>(results));
        }

        /// <summary>
        /// 异步获取多个资源
        /// </summary>
        /// <param name="paths"></param>
        /// <param name="callBack"></param>
        public static async void LoadMultipleAssetsAsync(
            List<(string path, Action<UnityEngine.Object> callback)> callbacks,
            Action allCompleted = null)
        {
            var tasks = new List<Task>();

            foreach (var (path, callback) in callbacks)
            {
                var tcs = new TaskCompletionSource<bool>();
                LoadAssetAsync(path, (asset) =>
                {
                    callback?.Invoke(asset);
                    tcs.SetResult(true);
                });
                tasks.Add(tcs.Task);
            }

            await Task.WhenAll(tasks);
            allCompleted?.Invoke();
        }

        /// <summary>
        /// 异步加载场景
        /// </summary>
        /// <param name="scenePath"></param>
        /// <param name="progressCallback"></param>
        /// <param name="completedCallback"></param>
        public static void LoadSceneAsync(string scenePath, Action<float> progressCallback = null, Action completedCallback = null)
        {
            YooAssetManager.Instance.LoadSceneAsync(scenePath, progressCallback, completedCallback);
        }


        public static Sprite ConvertTextureToSprite(Texture2D texture)
        {
            Sprite sp = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            return sp;
        }

    }
}