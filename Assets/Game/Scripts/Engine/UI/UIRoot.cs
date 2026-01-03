using System.Collections;
using System.Collections.Generic;
using Engine;
using UnityEngine;

public class UIRoot : MonoBehaviour
{
    public UIBase [] uiBases;

    private void Awake()
    {
        foreach (var uiBase in uiBases)
        {
            uiBase.gameObject.SetActive(false);
        }
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
