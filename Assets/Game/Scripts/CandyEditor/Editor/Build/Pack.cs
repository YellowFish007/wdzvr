using System.IO;
using UnityEditor;
using UnityEngine;
using System;

public class Pack
{
    private static string packageTempPath = "G:/Pack";

    ////强更新模块
    ////[MenuItem("Build/打包脚本")]
    //[MenuItem("Custom/ shift + o 更新脚本 #o")]

    //public static void CopyScriptToHotFixScript()
    //{
    //    //编译Dll包括所有的代码
    //    HybridCLR.Editor.Commands.PrebuildCommand.GenerateAll();

    //    var target = EditorUserBuildSettings.activeBuildTarget;

    //    string hotfixDllSrcDir = SettingsUtil.GetHotUpdateDllsOutputDirByTarget(target);
    //    string hotfixAssembliesDstDir = EditorPathUtils.RES_PATH + "/RawAssets/Text/Script";
    //    foreach (var dll in SettingsUtil.HotUpdateAssemblyFilesExcludePreserved)
    //    {
    //        string dllPath = $"{hotfixDllSrcDir}/{dll}";
    //        string dllBytesPath = $"{hotfixAssembliesDstDir}/{dll}.bytes";
    //        File.Copy(dllPath, dllBytesPath, true);
    //        Debug.Log($"[CopyHotUpdateAssembliesToStreamingAssets] copy hotfix dll {dllPath} -> {dllBytesPath}");
    //    }

    //    CopyAOTAssembliesToStreamingAssets();

    //    ////打包内部
    //    //BuildInternal(EditorUserBuildSettings.activeBuildTarget);

    //    ////打包整包
    //    //StartPackageBuild();

    //    Debug.Log("打包成功");
    //    //Debug.Log("SettingsUtil.HotUpdateDllsRootOutputDir  :  " + SettingsUtil.HotUpdateDllsRootOutputDir);
    //}

    //public static void CopyAOTAssembliesToStreamingAssets()
    //{
    //    var target = EditorUserBuildSettings.activeBuildTarget;
    //    string aotAssembliesSrcDir = SettingsUtil.GetAssembliesPostIl2CppStripDir(target);
    //    string aotAssembliesDstDir = Application.streamingAssetsPath;

    //    foreach (var dll in SettingsUtil.AOTAssemblyNames)
    //    {
    //        string srcDllPath = $"{aotAssembliesSrcDir}/{dll}.dll";
    //        if (!File.Exists(srcDllPath))
    //        {
    //            Debug.LogError($"ab中添加AOT补充元数据dll:{srcDllPath} 时发生错误,文件不存在。裁剪后的AOT dll在BuildPlayer时才能生成，因此需要你先构建一次游戏App后再打包。");
    //            continue;
    //        }
    //        string dllBytesPath = $"{aotAssembliesDstDir}/{dll}.dll.bytes";
    //        File.Copy(srcDllPath, dllBytesPath, true);
    //        Debug.Log($"[CopyAOTAssembliesToStreamingAssets] copy AOT dll {srcDllPath} -> {dllBytesPath}");
    //    }
    //}

    //[MenuItem("Tools/CheckAccessMissingMetadata")]
    //public static void CheckAccessMissingMetadata()
    //{
    //    BuildTarget target = EditorUserBuildSettings.activeBuildTarget;
    //    // aotDir指向 构建主包时生成的裁剪aot dll目录，而不是最新的SettingsUtil.GetAssembliesPostIl2CppStripDir(target)目录。
    //    // 一般来说，发布热更新包时，由于中间可能调用过generate/all，SettingsUtil.GetAssembliesPostIl2CppStripDir(target)目录中包含了最新的aot dll，
    //    // 肯定无法检查出类型或者函数裁剪的问题。
    //    // 需要在构建完主包后，将当时的aot dll保存下来，供后面补充元数据或者裁剪检查。
    //    string aotDir = SettingsUtil.GetAssembliesPostIl2CppStripDir(target);

    //    // 第2个参数hotUpdateAssNames为热更新程序集列表。对于旗舰版本，该列表需要包含DHE程序集，即SettingsUtil.HotUpdateAndDHEAssemblyNamesIncludePreserved。
    //    var checker = new MissingMetadataChecker(aotDir, SettingsUtil.HotUpdateAssemblyNamesIncludePreserved);

    //    string hotUpdateDir = SettingsUtil.GetHotUpdateDllsOutputDirByTarget(target);
    //    foreach (var dll in SettingsUtil.HotUpdateAssemblyFilesExcludePreserved)
    //    {
    //        string dllPath = $"{hotUpdateDir}/{dll}";
    //        bool notAnyMissing = checker.Check(dllPath);
    //        if (!notAnyMissing)
    //        {
    //            // DO SOMETHING
    //        }
    //    }
    //}


    //private static void BuildInternal(BuildTarget buildTarget)
    //{
    //    Debug.Log($"开始构建 : {buildTarget}");

    //    var buildoutputRoot = AssetBundleBuilderHelper.GetDefaultBuildOutputRoot();
    //    var streamingAssetsRoot = AssetBundleBuilderHelper.GetStreamingAssetsRoot();

    //    // 构建参数
    //    BuiltinBuildParameters buildParameters = new BuiltinBuildParameters();
    //    buildParameters.BuildOutputRoot = buildoutputRoot;
    //    buildParameters.BuildinFileRoot = streamingAssetsRoot;
    //    buildParameters.BuildPipeline = EBuildPipeline.BuiltinBuildPipeline.ToString();
    //    buildParameters.BuildBundleType = (int)EBuildBundleType.AssetBundle; //必须指定资源包类型
    //    buildParameters.BuildTarget = buildTarget;
    //    buildParameters.PackageName = "DefaultPackage";
    //    buildParameters.PackageVersion = "1.0";
    //    buildParameters.VerifyBuildingResult = true;
    //    buildParameters.EnableSharePackRule = true; //启用共享资源构建模式，兼容1.5x版本
    //    buildParameters.FileNameStyle = EFileNameStyle.HashName;
    //    buildParameters.BuildinFileCopyOption = EBuildinFileCopyOption.None;
    //    buildParameters.BuildinFileCopyParams = string.Empty;
    //    buildParameters.EncryptionServices = new YooAsset.Editor.EncryptionNone();
    //    buildParameters.ManifestProcessServices = new YooAsset.Editor.ManifestProcessNone();
    //    buildParameters.ManifestRestoreServices = new YooAsset.Editor.ManifestRestoreNone();
    //    buildParameters.CompressOption = ECompressOption.LZ4;
    //    buildParameters.ClearBuildCacheFiles = false; //不清理构建缓存，启用增量构建，可以提高打包速度！
    //    buildParameters.UseAssetDependencyDB = true; //使用资源依赖关系数据库，可以提高打包速度！
    //    // 执行构建
    //    BuiltinBuildPipeline pipeline = new BuiltinBuildPipeline();
    //    var buildResult = pipeline.Run(buildParameters, true);
    //    if (buildResult.Success)
    //    {
    //        Debug.Log($"构建成功 : {buildResult.OutputPackageDirectory}");
    //    }
    //    else
    //    {
    //        Debug.LogError($"构建失败 : {buildResult.ErrorInfo}");
    //    }
    //}

    //打包
    public static void StartPackageBuild()
    {
        if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.Android)
        {
            string project_path = packageTempPath + "/" + Application.productName + "_" + DateTime.Now.ToString("yyyy_MM_dd_HHmmss") + ".apk";//目标目录
            string[] outScenes = { 
                "Assets/Main/Scene/Main.unity", 
                //"Assets/Plugins/CandyMatch3Kit/Scenes/LevelScene.Unity",
                "Assets/Plugins/CandyMatch3Kit/Scenes/GameScene.Unity",
            };
            BuildPipeline.BuildPlayer(outScenes, project_path, BuildTarget.Android, BuildOptions.CompressWithLz4);
        }
    }

}
