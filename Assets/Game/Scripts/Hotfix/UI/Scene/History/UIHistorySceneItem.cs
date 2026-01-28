using System.Collections;
using System.Collections.Generic;
using Engine;
using RbEngine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIHistorySceneItem : UIScollItem
{
    public TMP_Text nameText;
    public TMP_Text timeText;
    public Button loadBtn;

    private int _sceneId;
    private bool _isBtnInit = false;

    private void Awake()
    {
        loadBtn.AddOnPointerClick(OnLoadBtnClick);

    }
    
    public void FreshItem(SceneHistoryData.HistoryInfo info)
    {
        _sceneId = info.SceneId;
        nameText.text = Excel.GetSceneName(_sceneId);
        timeText.text = info.Time;
    }

    private void OnLoadBtnClick(Button btn)
    {
        string sceneResName = Excel.GetSceneRes(_sceneId);
        if (!string.IsNullOrEmpty(sceneResName))
        {
            //SceneManager.Instance.LoadSceneAsync(sceneResName);
        }
        else
        {
            Debug.LogError($"Scene resource name is empty for scene id: {_sceneId}");
        }
    }
}
