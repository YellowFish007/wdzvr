using System;
using System.Collections;
using System.Collections.Generic;
using Engine;
using RbEngine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIConfirm : UIBase
{
    public TMP_Text contentText;

    public Button sureBtn;

    public Button closeBtn;
    
    private Action _onConfirm;

    public override void OnCreate(params object[] args)
    {
        sureBtn.onClick.RemoveAllListeners();
        closeBtn.onClick.RemoveAllListeners();

        sureBtn.onClick.AddListener(OnSureClick);
        closeBtn.onClick.AddListener(OnCloseClick);

        if (args != null && args.Length > 0)
        {
            string content = args[0] as string;
            Action onConfirm = null;

            if (args.Length > 1)
            {
                onConfirm = args[1] as Action;
            }

            Setup(content, onConfirm);
        }
    }

    public void Setup(string content, Action onConfirm)
    {
        if (contentText != null)
        {
            contentText.text = content;
        }
        _onConfirm = onConfirm;
    }

    private void OnSureClick()
    {
        _onConfirm?.Invoke();
        SceneUIManager.Instance.ClosePersistentUI(Name);
    }

    private void OnCloseClick()
    {
        SceneUIManager.Instance.ClosePersistentUI(Name);
    }
}
