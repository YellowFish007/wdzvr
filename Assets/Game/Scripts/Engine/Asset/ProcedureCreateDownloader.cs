using System.Collections;
using System.Collections.Generic;
using Engine;
using UnityEngine;
using YooAsset;
namespace Engine
{
    public class ProcedureCreateDownloader : ProcedureBase
    {
        public override void OnEnter(params object[] args)
        {
            base.OnEnter();
            CreateDownloader();
        }

        void CreateDownloader()
        {
            Debug.Log("ProcedureCreateDownloader");
            var packageName = AssetConfig.PackageName;
            var package = YooAssets.GetPackage(packageName);
            int downloadingMaxNum = 10;
            int failedTryAgain = 3;
            var downloader = package.CreateResourceDownloader(downloadingMaxNum, failedTryAgain);
            AssetConfig.Downloader = downloader;


            if (downloader.TotalDownloadCount == 0)
            {
                Debug.Log("Not found any download files !");
                GameEvent.Send(AssetConfig.EVENT_START_GAME);
            }
            else
            {
                // 发现可下载文件，构建下载系统
                // 注意：下载前请确保磁盘空间足够
                int totalDownloadCount = downloader.TotalDownloadCount;
                long totalDownloadBytes = downloader.TotalDownloadBytes;

                Debug.Log("downloader.TotalDownloadCount " + totalDownloadCount);
                Debug.Log("downloader.totalDownloadBytes " + totalDownloadBytes);

                //显示下载信息
                GameEvent.Send(AssetConfig.EVENT_DOWNLOAD_INFO, totalDownloadCount, totalDownloadBytes);
            }
        }
    }
}