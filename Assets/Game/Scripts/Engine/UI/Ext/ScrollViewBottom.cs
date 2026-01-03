using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewBottom : MonoBehaviour
{
    IEnumerator Start()
    {
        // 等待一帧，让UI布局（如Content大小）完成计算[citation:1]
        yield return new WaitForEndOfFrame();

        ScrollRect scrollRect = GetComponent<ScrollRect>();
        scrollRect.normalizedPosition = new Vector2(0, 0);
    }
}