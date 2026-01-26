using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Engine;
using RbEngine;

public class UIChatMsgItem : MonoBehaviour
{
    #region Fields

    [Header("UI Elements")]
    public Image msgImg;
    public TMP_Text msgText;
    public Image emojiImg;
    public Button chatVoiceBtn;
    public TMP_Text voiceText;

    [Header("Configuration")]
    public float maxWidth = 600f; // Max text width
    
    private const float TEXT_PADDING_X = 50f;
    private const float TEXT_PADDING_Y = 20f;
    private const float EMOJI_HEIGHT_PADDING = 60f;

    private ChatData.ChatMsg chatMsg;

    #endregion

    #region Unity Events

    private void Awake()
    {
        if (chatVoiceBtn != null)
        {
            chatVoiceBtn.AddOnPointerClick(OnBtnClick);
        }
    }

    #endregion

    #region Public Methods

    public void FreshItem(ChatData.ChatMsg data)
    {
        this.chatMsg = data;

        if (chatMsg == null) return;

        ResetUI();

        if (chatMsg.IsEmoji)
        {
            ShowEmoji();
        }
        else if (chatMsg.IsVoice)
        {
            ShowVoice();
        }
        else
        {
            ShowText();
        }
    }

    #endregion

    #region Private Methods

    private void ResetUI()
    {
        if (emojiImg) emojiImg.gameObject.SetActive(false);
        if (msgImg) msgImg.gameObject.SetActive(false);
        if (msgText) msgText.gameObject.SetActive(false);
        if (chatVoiceBtn) chatVoiceBtn.SetActive(false);
    }

    private void OnBtnClick(Button button)
    {
        if (chatMsg != null)
        {
            VoiceManager.Instance.PlayRecord(this.chatMsg.VoiceData);
        }
    }

    private void ShowEmoji()
    {
        if (emojiImg == null) return;

        emojiImg.gameObject.SetActive(true);

        int index = 0;
        int.TryParse(chatMsg.Content, out index);
        string iconPath = GetEmojiSpritePath(index);
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

    private void ShowVoice()
    {
        if (chatVoiceBtn == null) return;

        chatVoiceBtn.SetActive(true);
        float duration = VoiceManager.Instance.GetAudioDuration(chatMsg.VoiceData);
        
        if (voiceText != null)
        {
            voiceText.text = $"{Math.Ceiling(duration)}\"";
        }
    }

    private void ShowText()
    {
        if (msgImg) msgImg.gameObject.SetActive(true);
        if (msgText) msgText.gameObject.SetActive(true);

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

        // Force rebuild and sync size immediately
        LayoutRebuilder.ForceRebuildLayoutImmediate(msgText.rectTransform);
        
        UpdateTextSizeSynchronous();
    }

    private void UpdateTextSizeSynchronous()
    {
        if (msgImg != null && msgText != null)
        {
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
        if (rootRect != null && msgImg != null)
        {
            float bgHeight = msgImg.rectTransform.sizeDelta.y;
            float yOffset = Mathf.Abs(msgImg.rectTransform.anchoredPosition.y);
            rootRect.sizeDelta = new Vector2(rootRect.sizeDelta.x, bgHeight + yOffset);
        }
    }

    private string GetEmojiSpritePath(int index)
    {
        return $"RawAssets/Texture/Icon/Emoji/emoji_{(index + 1):00}";
    }

    #endregion

    #region Helpers

    private T GetOrAddComponent<T>(GameObject go) where T : Component
    {
        T comp = go.GetComponent<T>();
        if (comp == null) comp = go.AddComponent<T>();
        return comp;
    }

    #endregion
}
