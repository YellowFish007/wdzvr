using UnityEngine;

public class DynamicCameraSize : MonoBehaviour
{
    public float referenceWidth = 1080f; // 参考宽度，对应美术设计的宽度
    public float referenceHeight = 2400f; // 参考高度，对应美术设计的高度
    public float pixelsPerUnit = 100f; // 每单位像素数

    private Camera mainCamera;

    void Awake()
    {
        mainCamera = GetComponent<Camera>();
        if (mainCamera.orthographic)
        {
            AdjustCameraSize();
        }
    }

    void AdjustCameraSize()
    {
        float referenceAspect = referenceWidth / referenceHeight;
        float currentAspect = (float)Screen.width / Screen.height;

        if (currentAspect > referenceAspect)
        {
            // 屏幕更宽，按高度适配
            float targetHeightInUnits = referenceHeight / pixelsPerUnit;
            mainCamera.orthographicSize = targetHeightInUnits / 2f;
        }
        else
        {
            // 屏幕更高，按宽度适配
            float targetWidthInUnits = referenceWidth / pixelsPerUnit;
            mainCamera.orthographicSize = targetWidthInUnits / (2f * currentAspect);
        }
    }
}