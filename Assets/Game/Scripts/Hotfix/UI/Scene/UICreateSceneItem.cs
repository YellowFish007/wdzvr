using System.Collections;
using System.Collections.Generic;
using Engine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICreateSceneItem : UIScollItem
{
    public Image headImg;

    public TMP_Text desText;

    public TMP_Text nameText;

    public void FreshItem(int id)
    {
        headImg.SetSprite("RawAssets/Texture/Icon/Scene/" + Excel.GetSceneIcon(id), false);

        desText.text = Excel.GetSceneDesc(id);

        nameText.text = Excel.GetSceneName(id);
    }
}
