using System;
using System.Collections;
using System.Collections.Generic;
using Engine;
using RbEngine;
using UnityEngine;
using UnityEngine.UI;

public class UIMain : UIBase
{
    public override string Name => "UIMain";

    public Button userInfoBtn;
    public Button friendBtn;
    public Button shopBtn;
    public Button sceneSettingBtn;
    public Button bagBtn;
    public Button skillBtn;
    public Button joinRoomBtn;
    public Button createSceneBtn;
    public Button joinSceneBtn;
    public Button hallSceneBtn;
    public Button backMainSceneBtn;



    private void Awake()
    {
        userInfoBtn.AddOnPointerSoundClick(OnBtnClick);
    }

    private void OnBtnClick(Button btn)
    {
        if (userInfoBtn == btn)
        {
            GameEvent.Send("UIRoot", UIConfig.UserInfo); 
        }
        else if (friendBtn == btn)
        {
            GameEvent.Send("UIRoot", UIConfig.Friend);
        }
        else if (bagBtn == btn)
        {
            GameEvent.Send("UIRoot", UIConfig.Bag);
        }
    }

    public override void OnOpen()
    {

    }

}
