using Engine;
using UnityEngine;
using YooAsset;
using System.Collections;
using System;

public class Main : MonoBehaviour
{
    void Start()
    {
        YooAssetManager.Instance.Init(OnLoadRes);
    }

    private void OnLoadRes()
    {
        GameManager.Instance.Init();
        GameManager.Instance.LoadSceneAsync<Scene1001>();
    }
}