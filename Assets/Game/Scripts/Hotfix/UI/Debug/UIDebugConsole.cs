using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;

public class UIDebugConsole : MonoBehaviour
{
    [Header("UI References")]
    public TMP_Text logText;
    public ScrollRect scrollRect;
    
    [Header("Settings")]
    public int maxLogCount = 100;
    public bool showStackTrace = false;

    private Queue<string> logQueue = new Queue<string>();
    private StringBuilder sb = new StringBuilder();

    void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        if (type == LogType.Warning)
        {
            return;
        }

        string color = "white";
        switch (type)
        {
            case LogType.Error:
            case LogType.Exception:
            case LogType.Assert:
                color = "red";
                break;
            case LogType.Warning:
                color = "yellow";
                break;
        }

        string finalLog = $"<color={color}>[{System.DateTime.Now:HH:mm:ss}] {logString}</color>";
        
        if (showStackTrace || type == LogType.Exception || type == LogType.Error)
        {
             if (!string.IsNullOrEmpty(stackTrace))
                finalLog += $"\n<color={color}><size=80%>{stackTrace}</size></color>";
        }

        logQueue.Enqueue(finalLog);

        while (logQueue.Count > maxLogCount)
        {
            logQueue.Dequeue();
        }

        UpdateUI();
    }

    void UpdateUI()
    {
        if (logText == null) return;

        sb.Clear();
        foreach (var log in logQueue)
        {
            sb.AppendLine(log);
        }
        logText.text = sb.ToString();

        // Auto scroll to bottom
        if (scrollRect != null)
        {
            // Wait for end of frame or force update to ensure content height is recalculated
            Canvas.ForceUpdateCanvases();
            scrollRect.verticalNormalizedPosition = 0f;
        }
    }

    public void ClearLogs()
    {
        logQueue.Clear();
        UpdateUI();
    }
}
