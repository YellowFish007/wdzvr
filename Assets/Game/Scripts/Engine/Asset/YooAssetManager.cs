using System;
using System.Collections;
using UnityEngine;
using YooAsset;

namespace Engine
{
    public class YooAssetManager : SingletonGameObject<YooAssetManager>
    {
        private ResourcePackage _package;
        public void Init(EPlayMode playMode = EPlayMode.OfflinePlayMode ,string serverUrl = "")
        {
            AssetConfig.PlayMode = playMode;
            AssetConfig.ServerUrl = serverUrl;

            Procedure.Add(new ProcedureInitAsset());
            Procedure.Add(new ProcedureRequestPackageVersion());
            Procedure.Add(new ProcedureUpdatePackageManifest());
            Procedure.Add(new ProcedureCreateDownloader());
            Procedure.Add(new ProcedureDownloadPackageFiles());
            Procedure.Add(new ProcedureDownloadPackageOver());
            Procedure.Add(new ProcedureStartGame());

            Procedure.Change<ProcedureInitAsset>();
        }

        public void SetPackage(ResourcePackage package)
        {
            _package = package;
        }
        public ResourcePackage GetPackage()
        {
            return _package;
        }
        /// <summary>
        /// 同步加载资源
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="location"></param>
        /// <returns></returns>
        public T LoadAssetSync<T>(string assetPath) where T : UnityEngine.Object
        {
            if (_package == null)
            {
                Debug.LogError($"YooAssetManager.LoadAssetSync: _package is null! Cannot load {assetPath}");
                return null;
            }
            AssetHandle handle = _package.LoadAssetSync<UnityEngine.Object>(assetPath);
            return handle.AssetObject as T;
        }

        /// <summary>
        /// 异步加载资源转换成T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assetPath"></param>
        /// <param name="callBack"></param>
        public void LoadAssetAsync<T>(string assetPath, Action<T> callBack) where T : UnityEngine.Object
        {
            LoadAssetAsync(assetPath, delegate (UnityEngine.Object obj)
            {
                callBack(obj as T);
            });
        }

        /// <summary>
        /// 异步加载资源 object
        /// </summary>
        /// <param name="assetPath"></param>
        /// <param name="callBack"></param>
        public void LoadAssetAsync(string assetPath, Action<UnityEngine.Object> callBack)
        {
            YooAssetManager.Instance.StartCoroutine(LoadAssetAsyncIEnumerator(assetPath, callBack));
        }

        IEnumerator LoadAssetAsyncIEnumerator(string assetPath, Action<UnityEngine.Object> callBack)
        {
            AssetHandle handle = _package.LoadAssetAsync<UnityEngine.Object>(assetPath);
            yield return handle;
            callBack(handle.AssetObject);
        }

        /// <summary>
        /// 异步加载场景
        /// </summary>
        /// <param name="scenePath">场景路径</param>
        /// <param name="progressCallback">进度回调(0-1)</param>
        /// <param name="completedCallback">完成回调</param>
        public void LoadSceneAsync(string scenePath, Action<float> progressCallback = null, Action completedCallback = null)
        {
            StartCoroutine(LoadSceneAsyncIEnumerator(scenePath, progressCallback, completedCallback));
        }

        IEnumerator LoadSceneAsyncIEnumerator(string scenePath, Action<float> progressCallback, Action completedCallback)
        {
            SceneHandle handle = _package.LoadSceneAsync(scenePath);
            while (!handle.IsDone)
            {
                progressCallback?.Invoke(handle.Progress);
                yield return null;
            }

            completedCallback?.Invoke();
        }


        //---------------------------专门给YIUI调用---------------------------

        public (UnityEngine.Object, int) LoadAsset(string arg1, string arg2, Type arg3)
        {
            var handle = _package.LoadAssetSync(arg2, arg3);
            return (handle.AssetObject, handle.GetHashCode());
        }

    }
}