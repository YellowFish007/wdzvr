using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Engine
{
    public static class RectTransformUtils
    {
        public static Vector2 ScreenPointToLocalPointInRectangle(RectTransform parentTransform, Vector3 screenPos, Camera uiCamera)
        {
            Vector2 locPos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(parentTransform, screenPos, uiCamera, out locPos);
            return locPos;
        }
    }
}