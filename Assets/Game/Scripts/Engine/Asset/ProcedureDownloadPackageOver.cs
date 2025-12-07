using System.Collections;
using System.Collections.Generic;
using Engine;
using UnityEngine;
using YooAsset;
namespace Engine
{
    public class ProcedureDownloadPackageOver : ProcedureBase
    {
        public override void OnEnter(params object[] args)
        {
            base.OnEnter();

            var packageName = AssetConfig.PackageName;
            var package = YooAssets.GetPackage(packageName);
            var operation = package.ClearCacheFilesAsync(EFileClearMode.ClearUnusedBundleFiles);
            operation.Completed += Operation_Completed;
        }

        private void Operation_Completed(YooAsset.AsyncOperationBase obj)
        {
            //下载完成后直接进入游戏
            GameEvent.Send(AssetConfig.EVENT_START_GAME);
        }
    }
}