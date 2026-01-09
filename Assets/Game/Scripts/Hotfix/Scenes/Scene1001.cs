using System.Collections;
using System.Collections.Generic;
using Engine;
using UnityEngine;
using UnityTimer;

public class Scene1001 : SceneBase
{
    public UIRoot uiRoot;

    private void Awake()
    {
        this.AttachTimer(1.0f, delegate ()
        {
            uiRoot.OpenUI(UIConfig.Login);

            GameManager.Instance.InitTables();

            string icon = Excel.GetSceneIcon(1001);
            Debug.Log("UILogin icon : " + icon);

        });
    }
    public override void OnCreate(params object[] args)
    {
    }

}
