using System.Collections;
using System.Collections.Generic;
using Engine;
using UnityEngine;
using UnityTimer;

public class Scene1001 : SceneBase
{
    private void Awake()
    {
        // Initialize VR Input Manager
        VRInputManager.Instance.Init();

        GameEvent.AddEventListener(EventConfig.PICO_GRIP_PRESS, OnGripPress);

        Sound.PlayMusic("music_bg1001");

        Debug.Log("Sound.IsGameMusicOpen() : " + Sound.IsGameMusicOpen());

        this.AttachTimer(1.0f, delegate ()
        {
            GameManager.Instance.InitTables();
            GameManager.Instance.InitChatTestData();
            GameManager.Instance.InitFriendTestData();

            Data.Load<LocalData>();

            SceneUIManager.Instance.OpenPersistentUI(UIConfig.FlyTip);

            SceneUIManager.Instance.OpenUI(UIConfig.Login);
        });
    }

    private void OnGripPress()
    {
        Debug.Log("Scene1001: Grip button pressed!");
        SceneUIManager.Instance.ToggleRootActive();
    }

    private void OnDestroy()
    {
        GameEvent.RemoveEventListener(EventConfig.PICO_GRIP_PRESS, OnGripPress);
    }

}
