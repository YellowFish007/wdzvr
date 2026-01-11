using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Engine;
using System;
using RbEngine;

public class UIChatMsgItem : MonoBehaviour
{
    // UI Elements
    public Image msgImg;
    public TMP_Text msgText;
    public Image emojiImg;
    public Button chatVoiceBtn;

    // Configuration
    public float maxWidth = 600f; // Max text width
    private const float TEXT_PADDING_X = 50f;
    private const float TEXT_PADDING_Y = 20f;
    private const float EMOJI_HEIGHT_PADDING = 60f;

    private ChatData.ChatMsg chatMsg;
    private void Awake()
    {
        chatVoiceBtn.AddOnPointerClick(OnBtnClick);
    }

    private void OnBtnClick(Button button)
    {
        VoiceManager.Instance.PlayRecord(this.chatMsg.VoiceData);
    }

    public void FreshItem(ChatData.ChatMsg chatMsg)
    {
        this.chatMsg = chatMsg;

        if (chatMsg == null) return;

        ResetUI();

        if (chatMsg.IsEmoji)
        {
            ShowEmoji(chatMsg);
        }
        else if (chatMsg.IsVoice)
        {
            ShowVoice(chatMsg);
        }
        else
        {
            ShowText(chatMsg);
        }
    }

    private void ResetUI()
    {
        emojiImg.gameObject.SetActive(false);
        msgImg.gameObject.SetActive(false);
        msgText.gameObject.SetActive(false);
        chatVoiceBtn.SetActive(false);
    }

    private void ShowEmoji(ChatData.ChatMsg chatMsg)
    {
        emojiImg.gameObject.SetActive(true);
        
        int index = 0;
        int.TryParse(chatMsg.Content, out index); // Safer parsing
        string iconPath = GetEmojjSpritePath(index);
        emojiImg.SetSprite(iconPath, false);

        UpdateEmojiLayout();
    }

    private void UpdateEmojiLayout()
    {
        var rootRect = transform as RectTransform;
        if (rootRect != null && emojiImg != null)
        {
            float height = emojiImg.rectTransform.sizeDelta.y;
            rootRect.sizeDelta = new Vector2(rootRect.sizeDelta.x, height + EMOJI_HEIGHT_PADDING);
        }
    }

    private void ShowVoice(ChatData.ChatMsg chatMsg)
    {
        chatVoiceBtn.SetActive(true);
        // Voice layout handling if needed
    }

    private void ShowText(ChatData.ChatMsg chatMsg)
    {
        msgImg.gameObject.SetActive(true);
        msgText.gameObject.SetActive(true);
        
        SetTextContent(chatMsg.Content);
    }

    private void SetTextContent(string content)
    {
        if (msgText == null) return;

        msgText.text = content;
        
        // Reset wrapping to calculate preferred width
        msgText.enableWordWrapping = false;
        msgText.ForceMeshUpdate();

        float preferredWidth = msgText.preferredWidth;
        
        // Handle LayoutElement based on width
        var layoutElement = GetOrAddComponent<LayoutElement>(msgText.gameObject);
        
        if (preferredWidth > maxWidth)
        {
            msgText.enableWordWrapping = true;
            layoutElement.preferredWidth = maxWidth;
        }
        else
        {
            msgText.enableWordWrapping = false;
            layoutElement.preferredWidth = -1; // Disable override
        }

        // Force rebuild and sync size
        LayoutRebuilder.ForceRebuildLayoutImmediate(transform as RectTransform);
        StartCoroutine(SyncTextSizeNextFrame());
    }

    private T GetOrAddComponent<T>(GameObject go) where T : Component
    {
        T comp = go.GetComponent<T>();
        if (comp == null) comp = go.AddComponent<T>();
        return comp;
    }

    private IEnumerator SyncTextSizeNextFrame()
    {
        yield return null;

        if (msgImg != null && msgText != null)
        {
            // Wait for valid dimensions
            float timer = 0f;
            while ((msgText.rectTransform.rect.width <= 0 || msgText.rectTransform.rect.height <= 0) && timer < 0.5f)
            {
                yield return null;
                timer += Time.deltaTime;
            }

            var textWidth = msgText.rectTransform.rect.width;
            var textHeight = msgText.rectTransform.rect.height;

            // Update Background Size
            msgImg.rectTransform.sizeDelta = new Vector2(textWidth + TEXT_PADDING_X, textHeight + TEXT_PADDING_Y);

            // Update Root Height
            UpdateRootHeightForText();
        }
    }

    private void UpdateRootHeightForText()
    {
        var rootRect = transform as RectTransform;
        if (rootRect != null)
        {
            float bgHeight = msgImg.rectTransform.sizeDelta.y;
            float yOffset = Mathf.Abs(msgImg.rectTransform.anchoredPosition.y);
            rootRect.sizeDelta = new Vector2(rootRect.sizeDelta.x, bgHeight + yOffset);
        }
    }

    private string GetEmojjSpritePath(int index)
    {
        return "RawAssets/Texture/Icon/Emoji/emoji_" + (index + 1).ToString("00");
    }
}
