using System;
using System.Collections;
using System.Collections.Generic;
using Engine;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSoundEffect : MonoBehaviour
{
    private void Awake()
    {
        Button btn = gameObject.GetComponent<Button>();
        Toggle toggle = gameObject.GetComponent<Toggle>();

        if (btn != null)
        {
            btn.onClick.AddListener(OnBtnClick);
        }
        if (toggle != null)
        {
            toggle.onValueChanged.AddListener(OnToggleClick);
        }

    }

    private void OnBtnClick()
    {
        Sound.PlayShot("sound_click");
    }

    private void OnToggleClick(bool show)
    {
        Sound.PlayShot("sound_click");
    }
}
