using System.Collections;
using System.Collections.Generic;
using Engine;
using UnityEngine;
using YooAsset;
namespace Engine
{
    public class ProcedureUpdatePackageManifest : ProcedureBase
    {
        public override void OnEnter(params object[] args)
        {
            base.OnEnter();
            YooAssetManager.Instance.StartCoroutine(UpdateManifest());
        }

        private IEnumerator UpdateManifest()
        {
            var package = YooAssets.GetPackage(AssetConfig.PackageName);
            var operation = package.UpdatePackageManifestAsync(AssetConfig.PackageVersion);
            yield return operation;

            if (operation.Status != EOperationStatus.Succeed)
            {
                Debug.LogWarning(operation.Error);

                GameEvent.Send(AssetConfig.EVENT_UPDATE_PACKAGE_MANIFEST_FAILED);

                yield break;
            }
            else
            {
                Procedure.Change<ProcedureCreateDownloader>();
            }
        }
    }
}