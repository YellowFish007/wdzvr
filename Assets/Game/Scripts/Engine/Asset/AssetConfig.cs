using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YooAsset;
namespace Engine
{
    public class AssetConfig
    {
        public static EPlayMode PlayMode = EPlayMode.EditorSimulateMode;
        public static string PackageName = "DefaultPackage";
        public static string PackageVersion = "";

        public static string ServerUrl = "";

        public static ResourceDownloaderOperation Downloader;

        public static string EVENT_INIT_FAILED = "EVENT_INIT_FAILED";

        public static string EVENT_UPDATE_PACKAGE_VERSION_FAILED = "EVENT_UPDATE_PACKAGE_VERSION_FAILED";
        public static string EVENT_UPDATE_PACKAGE_MANIFEST_FAILED = "EVENT_UPDATE_PACKAGE_MANIFEST_FAILED";

        public static string EVENT_DOWNLOAD_INFO = "EVENT_DOWNLOAD_INFO";
        public static string EVENT_DOWNLOAD_ERROR = "EVENT_DOWNLOAD_ERROR";
        public static string EVENT_DOWNLOAD_UPDATE = "EVENT_DOWNLOAD_UPDATE";

        public static string EVENT_START_GAME = "EVENT_START_GAME";

    }
}