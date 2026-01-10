using System.Collections;
using System.Collections.Generic;
using Engine;
using UnityEngine;
using UnityEngine.UI;

public class UIChatEmojiItem : UIScollItem
{
    public Image emojiImg;

    public void FreshItem(int index)
    {
        string iconPath = GetEmojjSpritePath(index);
        emojiImg.SetSprite(iconPath, false);
    }

    private string GetEmojjSpritePath(int index)
    {
        return "RawAssets/Texture/Icon/Emoji/emoji_" + (index + 1).ToString("00");
    }
}
