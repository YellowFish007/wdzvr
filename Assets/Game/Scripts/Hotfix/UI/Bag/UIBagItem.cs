using System.Collections;
using System.Collections.Generic;
using Engine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIBagItem : MonoBehaviour
{
    public Image propImg;

    public TMP_Text countText;

    public void FreshItem(int itemId)
    {
        propImg.SetItemSprite(Excel.GetItemIcon(itemId));
        countText.text = BagData.Instance.GetItemCount(itemId).ToString();
    }
}
