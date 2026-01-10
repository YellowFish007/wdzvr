using System.Collections;
using System.Collections.Generic;
using Engine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIChatRoleItem : UIScollItem
{
    public TMP_Text nameText;
    public Image headImage;

    public GameObject selectImg;

    public void FreshItem(string name, string avatar,bool showSelect)
    {
        nameText.text = name;
        headImage.SetSprite("RawAssets/Texture/Icon/Avatar/" + avatar, false);
        selectImg.SetActive(showSelect);
    }
}
