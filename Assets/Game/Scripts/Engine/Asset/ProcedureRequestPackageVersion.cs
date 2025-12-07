using System.Collections;
using System.Collections.Generic;
using Engine;
using UnityEngine;
using YooAsset;
namespace Engine
{
    public class ProcedureRequestPackageVersion : ProcedureBase
    {
        public override void OnEnter(params object[] args)
        {
            base.OnEnter();
            YooAssetManager.Instance.StartCoroutine(UpdatePackageVersion());
        }

        private IEnumerator UpdatePackageVersion()
        {
            var package = YooAssets.GetPackage(AssetConfig.PackageName);
            var operation = package.RequestPackageVersionAsync();
            yield return operation;

            if (operation.Status != EOperationStatus.Succeed)
            {
                Debug.LogWarning(operation.Error);
                GameEvent.Send(AssetConfig.EVENT_UPDATE_PACKAGE_VERSION_FAILED);
            }
            else
            {
                Debug.Log($"Request package version : {operation.PackageVersion}");
                AssetConfig.PackageVersion = operation.PackageVersion;

                Procedure.Change<ProcedureUpdatePackageManifest>();
            }
        }


    }
}