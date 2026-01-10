using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Engine;
using System;

public class UIChatMsgItem : MonoBehaviour
{
    [Header("Components")]
    public Image msgImg;
    public TMP_Text msgText;

    [Header("Settings")]
    public float maxWidth = 600f; // 最大宽度限制

    /// <summary>
    /// 设置内容并动态适配
    /// </summary>
    /// <param name="content"></param>
    public void SetContent(string content)
    {
        if (msgText == null) return;

        msgText.text = content;

        // 1. 先重置状态，允许不换行来计算理想宽度
        msgText.enableWordWrapping = false;

        // 强制更新网格以获取最新的 preferredWidth
        msgText.ForceMeshUpdate();

        float preferredWidth = msgText.preferredWidth;

        // 2. 判断是否超过最大宽度
        if (preferredWidth > maxWidth)
        {
            // 超过最大宽度，启用换行，并限制布局元素的宽度
            msgText.enableWordWrapping = true;

            var layoutElement = msgText.GetComponent<LayoutElement>();
            if (layoutElement == null)
            {
                layoutElement = msgText.gameObject.AddComponent<LayoutElement>();
            }
            layoutElement.preferredWidth = maxWidth;
        }
        else
        {
            // 未超过最大宽度，直接使用内容宽度（或者禁用LayoutElement的限制让ContentSizeFitter处理）
            msgText.enableWordWrapping = false; // 或者保持true但宽度不够自然不会换行

            var layoutElement = msgText.GetComponent<LayoutElement>();
            if (layoutElement != null)
            {
                layoutElement.preferredWidth = -1; // -1 表示不使用 preferredWidth 覆盖
                // 或者 layoutElement.enabled = false;
            }
        }

        // 3. 强制刷新父级布局，确保背景图跟随变化
        // 有时候需要刷新两次或者刷新整个LayoutGroup
        LayoutRebuilder.ForceRebuildLayoutImmediate(transform as RectTransform);

        // 4. 在下一帧同步 msgImg 的宽高，等待 Layout 系统计算完成
        StartCoroutine(SyncSizeNextFrame());
    }

    private IEnumerator SyncSizeNextFrame()
    {
        // 等待一帧，让LayoutGroup和ContentSizeFitter完成计算
        yield return null;

        if (msgImg != null && msgText != null)
        {
            var width = msgText.rectTransform.rect.width;
            var height = msgText.rectTransform.rect.height;

            // 如果宽或高为0，可能还在计算，等待直到有尺寸（最多等待0.5秒）
            float timer = 0f;
            while ((width <= 0 || height <= 0) && timer < 0.5f)
            {
                yield return null;
                timer += Time.deltaTime;
                width = msgText.rectTransform.rect.width;
                height = msgText.rectTransform.rect.height;
            }

            msgImg.rectTransform.sizeDelta = new Vector2(width + 50, height + 20);

            // 修改本身游戏物体的高度，使其能包住 msgImg
            var rootRect = transform as RectTransform;
            if (rootRect != null)
            {
                // 高度 = bgImg的高度 + bgImg的Y坐标绝对值 (处理顶部偏移)
                float bgHeight = msgImg.rectTransform.sizeDelta.y;
                float yOffset = Mathf.Abs(msgImg.rectTransform.anchoredPosition.y);
                rootRect.sizeDelta = new Vector2(rootRect.sizeDelta.x, bgHeight + yOffset);
            }
        }
    }



}
