using System;
using System.Collections;
using System.Collections.Generic;
using Engine;
using RbEngine;
using UnityEngine;
using UnityEngine.UI;

public class UIMain : UIBase
{
    public Button userInfoBtn;
    public Button friendBtn;
    public Button shopBtn;
    public Button sceneSettingBtn;
    public Button bagBtn;
    public Button skillBtn;
    public Button chatBtn;

    public Button joinRoomBtn;
    public Button createSceneBtn;
    public Button joinSceneBtn;
    public Button hallSceneBtn;
    public Button backMainSceneBtn;

    public Button settingBtn;
    public Button closeBtn;


    public override void OnCreate()
    {
        userInfoBtn.AddOnPointerClick(OnBtnClick);
        friendBtn.AddOnPointerClick(OnBtnClick);
        shopBtn.AddOnPointerClick(OnBtnClick);
        sceneSettingBtn.AddOnPointerClick(OnBtnClick);
        bagBtn.AddOnPointerClick(OnBtnClick);
        skillBtn.AddOnPointerClick(OnBtnClick);
        chatBtn.AddOnPointerClick(OnBtnClick);

        joinRoomBtn.AddOnPointerClick(OnBtnClick);
        createSceneBtn.AddOnPointerClick(OnBtnClick);
        joinSceneBtn.AddOnPointerClick(OnBtnClick);
        hallSceneBtn.AddOnPointerClick(OnBtnClick);
        backMainSceneBtn.AddOnPointerClick(OnBtnClick);

        settingBtn.AddOnPointerClick(OnBtnClick);
        closeBtn.AddOnPointerClick(OnBtnClick);
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
        else if (skillBtn == btn)
        {
            GameEvent.Send("UIRoot", UIConfig.Skill);
        }
        else if (settingBtn == btn)
        {
            GameEvent.Send("UIRoot", UIConfig.Setting);
        }
        else if (chatBtn == btn)
        {
            GameEvent.Send("UIRoot", UIConfig.Chat);
        }
        else if (createSceneBtn == btn)
        {
            GameEvent.Send("UIRoot", UIConfig.CreateScene);
        }
        else if (closeBtn == btn)
        {
            Close();
        }        
    }
}
