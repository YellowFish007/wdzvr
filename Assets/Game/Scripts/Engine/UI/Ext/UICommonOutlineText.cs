using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UICommonOutlineText : MonoBehaviour
{
    public TMP_Text outlineText;
    public TMP_Text baseText;

    public void SetText(int text)
    {
        SetText(text.ToString());
    }

    public void SetText(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return;
        }
        var cleanText = System.Text.RegularExpressions.Regex.Replace(text, "<.*?>", string.Empty);

        outlineText.text = cleanText;
        baseText.text = text;
    }

    public void SetFontSize(int size)
    {
        outlineText.fontSize = size;
        baseText.fontSize = size;
    }

}
