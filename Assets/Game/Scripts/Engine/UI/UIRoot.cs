using System;
using System.Collections;
using System.Collections.Generic;
using Engine;
using UnityEngine;

public class UIRoot : MonoBehaviour
{
    public UIBase[] uiBases;

    private void Awake()
    {

        GameEvent.AddEventListener<string>("UIRoot", OnShowUI);

        foreach (var uiBase in uiBases)
        {
            uiBase.gameObject.SetActive(false);
        }
    }

    private void OnShowUI(string name)
    {
        OpenUI(name);
    }

    public void OpenUI(string name)
    {
        foreach (var uiBase in uiBases)
        {
            if (uiBase.Name == name)
            {
                uiBase.SetActive(true);
                uiBase.OnOpen();
            }
            else
            {
                uiBase.SetActive(false);
            }
        }
    }

}
