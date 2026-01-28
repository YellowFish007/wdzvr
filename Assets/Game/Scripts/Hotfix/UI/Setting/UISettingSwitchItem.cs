using System;
using System.Collections;
using System.Collections.Generic;
using RbEngine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Engine;

public class UISettingSwitchItem : MonoBehaviour
{
    public Image ui_offImg;
    public Image ui_onImg;
    public Transform ui_yellowObj;

    public TMP_Text ui_onText;
    public TMP_Text ui_offText;

    public Button ui_switchBtn;

    private bool isOn = false;
    private Vector3 offPosition;
    private Vector3 onPosition;

    public bool isMusic = false;

    void Awake()
    {
        ui_switchBtn.AddOnPointerSoundClick(OnBtnClick);

        // 初始化位置
        offPosition = ui_yellowObj.localPosition;
        onPosition = offPosition + new Vector3(200, 0, 0);

        if (isMusic)
        {
            SetShowStatus(Sound.IsGameMusicOpen());
        }
        else
        {
            SetShowStatus(Sound.IsGameSoundOpen());
        }

    }
    public void SetShowStatus(bool isOn)
    {
        this.isOn = isOn;
        // 更新文本
        ui_onText.SetActive(isOn);
        ui_offText.SetActive(!isOn);
        ui_yellowObj.localPosition = isOn ? onPosition : offPosition;
        ui_onImg.color = isOn ? new Color(1, 1, 1, 1) : new Color(1, 1, 1, 0);
        ui_offImg.color = !isOn ? new Color(1, 1, 1, 1) : new Color(1, 1, 1, 0);
    }

    public void Switch()
    {
        isOn = !isOn;
        AnimateSwitch();
    }

    private void AnimateSwitch()
    {
        // 停止所有Dotween动画
        DOTween.Kill(ui_offImg);
        DOTween.Kill(ui_onImg);
        DOTween.Kill(ui_yellowObj);

        float duration = 0.2f;

        // 透明度动画
        ui_offImg.DOFade(isOn ? 0 : 1, duration);
        ui_onImg.DOFade(isOn ? 1 : 0, duration);

        // 位置动画
        ui_yellowObj.DOLocalMove(isOn ? onPosition : offPosition, duration);

        // 更新文本
        ui_onText.SetActive(isOn);
        ui_offText.SetActive(!isOn);
    }

    private void OnBtnClick(Button btn)
    {
        if (btn == ui_switchBtn)
        {
            Switch();

            if (isOn)
            {
            }
            else
            {
            }
        }
    }
}