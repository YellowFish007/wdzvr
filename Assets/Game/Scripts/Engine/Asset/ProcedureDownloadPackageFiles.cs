using System.Collections;
using System.Collections.Generic;
using DG.Tweening.Core.Easing;
using Engine;
using UnityEngine;
using YooAsset;
namespace Engine
{
    public class ProcedureDownloadPackageFiles : ProcedureBase
    {
        public override void OnEnter(params object[] args)
        {
            base.OnEnter();

            YooAssetManager.Instance.StartCoroutine(BeginDownload());
        }

        private IEnumerator BeginDownload()
        {
            var downloader = (ResourceDownloaderOperation)AssetConfig.Downloader;
            downloader.DownloadErrorCallback = OnDownloadError;
            downloader.DownloadUpdateCallback = OnDownloadUpdate;
            downloader.BeginDownload();
            yield return downloader;

            // 下载结果
            if (downloader.Status != EOperationStatus.Succeed)
                yield break;

            Procedure.Change<ProcedureDownloadPackageOver>();
        }

        private void OnDownloadError(DownloadErrorData data)
        {
            GameEvent.Send(AssetConfig.EVENT_DOWNLOAD_ERROR, data);
        }

        private void OnDownloadUpdate(DownloadUpdateData data)
        {
            GameEvent.Send(AssetConfig.EVENT_DOWNLOAD_UPDATE, data);
        }
    }
}