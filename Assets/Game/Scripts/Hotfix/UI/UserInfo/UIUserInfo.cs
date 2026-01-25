using System.Collections;
using System.Collections.Generic;
using Engine;
using RbEngine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIUserInfo : UIBase
{
    public TMP_Text lvText;
    public TMP_Text idText;
    public Image headImg;

    public Button closeBtn;

    public override void OnCreate(params object[] args)
    {
        closeBtn.AddOnPointerClick(OnBtnClick);

        RefreshView();
    }

    private void OnBtnClick(Button btn)
    {
        if (btn == closeBtn)
        {
            Close();
        }
    }

    private void RefreshView()
    {
        lvText.text = $"Lv.{AccountData.Instance.GetLevel()}";
        idText.text = $"ID:{AccountData.Instance.GetId()}";
        headImg.SetHeadSprite(AccountData.Instance.GetHeadIcon());
    }
}
