using Engine;
using UnityEngine;
using YooAsset;
using System.Collections;
using System;

public class Main : MonoBehaviour
{
    public EPlayMode playMode = EPlayMode.EditorSimulateMode;

    void Start()
    {
        GameEvent.AddEventListener(AssetConfig.EVENT_START_GAME, OnStartGame);

        YooAssetManager.Instance.Init(playMode);
    }

    private void OnDestroy()
    {
        GameEvent.RemoveEventListener(AssetConfig.EVENT_START_GAME, OnStartGame);
    }

    private void OnStartGame()
    {
        Procedure.Change<ProcedureStartGame>();
    }
}