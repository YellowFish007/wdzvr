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
        });
    }
    public override void OnCreate(params object[] args)
    {
    }

}
