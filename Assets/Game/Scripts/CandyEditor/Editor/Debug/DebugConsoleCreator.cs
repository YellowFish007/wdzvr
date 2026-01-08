using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using TMPro;

public class DebugConsoleCreator
{
    [MenuItem("Tools/Create Debug Console")]
    public static void CreateConsole()
    {
        // Check for EventSystem
        if (Object.FindObjectOfType<UnityEngine.EventSystems.EventSystem>() == null)
        {
            new GameObject("EventSystem", typeof(UnityEngine.EventSystems.EventSystem), typeof(UnityEngine.EventSystems.StandaloneInputModule));
        }

        // 1. Root Canvas
        GameObject root = new GameObject("DebugConsole");
        Canvas canvas = root.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;
        
        // VR sizing (small scale)
        RectTransform rootRT = root.GetComponent<RectTransform>();
        rootRT.localScale = Vector3.one * 0.001f; 
        rootRT.sizeDelta = new Vector2(800, 600);
        
        // Place in front of camera roughly
        if (Camera.main != null)
        {
            root.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 1.0f;
            root.transform.forward = Camera.main.transform.forward;
        }
        else
        {
            root.transform.position = new Vector3(0, 1.5f, 1.0f);
        }

        root.AddComponent<CanvasScaler>();
        root.AddComponent<GraphicRaycaster>();

        // 2. Background
        GameObject bg = new GameObject("Background");
        bg.transform.SetParent(root.transform, false);
        Image bgImg = bg.AddComponent<Image>();
        bgImg.color = new Color(0, 0, 0, 0.8f);
        RectTransform bgRT = bg.GetComponent<RectTransform>();
        bgRT.anchorMin = Vector2.zero;
        bgRT.anchorMax = Vector2.one;
        bgRT.sizeDelta = Vector2.zero;

        // 3. Scroll View
        GameObject scrollObj = new GameObject("Scroll View");
        scrollObj.transform.SetParent(bg.transform, false);
        ScrollRect scrollRect = scrollObj.AddComponent<ScrollRect>();
        RectTransform scrollRT = scrollObj.GetComponent<RectTransform>();
        scrollRT.anchorMin = Vector2.zero;
        scrollRT.anchorMax = Vector2.one;
        scrollRT.sizeDelta = new Vector2(-20, -20); // Padding

        // 4. Viewport
        GameObject viewport = new GameObject("Viewport");
        viewport.transform.SetParent(scrollObj.transform, false);
        Image viewportImg = viewport.AddComponent<Image>(); 
        viewportImg.color = Color.white; // Solid color for mask definition
        Mask mask = viewport.AddComponent<Mask>();
        mask.showMaskGraphic = false;
        
        RectTransform viewportRT = viewport.GetComponent<RectTransform>();
        viewportRT.anchorMin = Vector2.zero;
        viewportRT.anchorMax = Vector2.one;
        viewportRT.sizeDelta = Vector2.zero;
        viewportRT.pivot = new Vector2(0, 1);

        // 5. Content (The Text Object itself)
        GameObject textObj = new GameObject("LogText");
        textObj.transform.SetParent(viewport.transform, false);
        
        RectTransform textRT = textObj.AddComponent<RectTransform>();
        textRT.anchorMin = new Vector2(0, 1);
        textRT.anchorMax = new Vector2(1, 1);
        textRT.pivot = new Vector2(0, 1);
        textRT.sizeDelta = new Vector2(0, 0); 

        TMP_Text txt = textObj.AddComponent<TextMeshProUGUI>();
        txt.fontSize = 24;
        txt.color = Color.white;
        txt.alignment = TextAlignmentOptions.TopLeft;
        txt.enableWordWrapping = true;

        ContentSizeFitter csf = textObj.AddComponent<ContentSizeFitter>();
        csf.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
        
        // Link ScrollRect
        scrollRect.viewport = viewportRT;
        scrollRect.content = textRT;
        scrollRect.horizontal = false;
        scrollRect.vertical = true;
        scrollRect.scrollSensitivity = 20;

        // Add Logic Script
        UIDebugConsole console = root.AddComponent<UIDebugConsole>();
        console.logText = txt;
        console.scrollRect = scrollRect;

        // Select it
        Selection.activeGameObject = root;
        
        Debug.Log("Debug Console Created!");
    }
}
