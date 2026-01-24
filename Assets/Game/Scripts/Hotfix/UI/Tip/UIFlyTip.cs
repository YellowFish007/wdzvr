using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityTimer;
using cfg;
using RbEngine;
using TMPro;
using Engine;

public class UIFlyTip : UIBase
{
    public GameObject ui_tipNode;

    public override void OnCreate(params object[] args)
    {
        GameEvent.AddEventListener<string>(EventConfig.UI_SHOW_FLYTIP, ShowFlyTip);
    }

    public override void OnClose()
    {
        GameEvent.RemoveEventListener<string>(EventConfig.UI_SHOW_FLYTIP, ShowFlyTip);
    }
    
    public void ShowFlyTip(string arg)
    {
        GameObject tipNodeObj = Instantiate(ui_tipNode);
        tipNodeObj.SetParent(gameObject);
        tipNodeObj.SetActive(true);

        TMP_Text tipText = tipNodeObj.transform.Find("tipText").GetComponent<TMP_Text>();
        tipText.text = arg;

        RectTransform rectTransform = tipNodeObj.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(0, 100);

        this.AttachTimer(1.5f, () =>
        {
            rectTransform.DOLocalMoveY(300, 1.0f);

            tipNodeObj.GetComponent<Image>().DOFade(0, 1);
            tipText.DOFade(0.0f, 1.0f);
        });

        this.AttachTimer(2.5f, () =>
             Destroy(tipNodeObj)
        );

    }
}