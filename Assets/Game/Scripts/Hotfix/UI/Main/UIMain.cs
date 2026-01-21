using System;
using System.Collections;
using System.Collections.Generic;
using Engine;
using RbEngine;
using UnityEngine;
using UnityEngine.UI;

public class UIMain : UIBase
{
    
    public Button headBtn;

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
        headBtn.AddOnPointerClick(OnBtnClick);

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
        if (headBtn == btn)
        {
            SceneUIManager.Instance.OpenUI(UIConfig.Head);
        }
        else if (userInfoBtn == btn)
        {
            SceneUIManager.Instance.OpenUI(UIConfig.UserInfo);
        }
        else if (friendBtn == btn)
        {
            SceneUIManager.Instance.OpenUI(UIConfig.Friend);
        }
        else if (bagBtn == btn)
        {
            SceneUIManager.Instance.OpenUI(UIConfig.Bag);
        }
        else if (skillBtn == btn)
        {
            SceneUIManager.Instance.OpenUI(UIConfig.Skill);
        }
        else if (settingBtn == btn)
        {
            SceneUIManager.Instance.OpenUI(UIConfig.Setting);
        }
        else if (chatBtn == btn)
        {
            SceneUIManager.Instance.OpenUI(UIConfig.Chat);
        }
        else if (createSceneBtn == btn)
        {
            SceneUIManager.Instance.OpenUI(UIConfig.CreateScene);
        }
        else if (closeBtn == btn)
        {
            Close();
        }        
    }
}
