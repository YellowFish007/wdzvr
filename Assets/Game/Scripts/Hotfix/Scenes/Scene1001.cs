using System.Collections;
using System.Collections.Generic;
using Engine;
using UnityEngine;

public class Scene1001 : SceneBase
{
    //[SerializeField]
    //[Tooltip("Menu offset placement")]
    //private Vector3 menuOffset = new Vector3(0, 0, 3);
    //[SerializeField]
    //[Tooltip("Delay of the window opening and closing")]
    //private float menuDelay = 3f;

    public override void OnCreate(params object[] args) 
    {
        UIManager.Instance.SetCamera(Camera.main);

        UIManager.Instance.OpenUI(UIConfig.Login);
    }
}
