using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageFitToScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        RectTransform _rectTrans = GetComponent<RectTransform>();

        CanvasScaler _scaler = _rectTrans.GetComponentInParent<CanvasScaler>();
        RectTransform _rectTransform = _scaler.GetComponent<RectTransform>();
        float _width = _rectTransform.sizeDelta.x;
        float _height = _rectTransform.sizeDelta.y;

        // 添加零值检查，避免除零错误
        if (_rectTrans.sizeDelta.x <= 0 || _rectTrans.sizeDelta.y <= 0)
        {
            Debug.LogWarning($"ImageFitToScreen: {_rectTrans.name} 的 sizeDelta 为零或负数，跳过缩放计算");
            return;
        }

        float _xScale = _width / _rectTrans.sizeDelta.x;
        float _yScale = _height / _rectTrans.sizeDelta.y;
        
        // 检查计算结果是否有效
        if (float.IsInfinity(_xScale) || float.IsNaN(_xScale) || 
            float.IsInfinity(_yScale) || float.IsNaN(_yScale))
        {
            Debug.LogWarning($"ImageFitToScreen: {_rectTrans.name} 计算出无效的缩放值，跳过缩放");
            return;
        }
        
        var _scale = (_xScale > _yScale) ? _xScale : _yScale;
        
        // 最终检查缩放值
        if (float.IsInfinity(_scale) || float.IsNaN(_scale) || _scale <= 0)
        {
            Debug.LogWarning($"ImageFitToScreen: {_rectTrans.name} 最终缩放值无效，使用默认值1");
            _scale = 1f;
        }

        _rectTrans.localScale = new Vector3(_scale, _scale, 1f);

        Debug.Log($"ImageFitToScreen: {_rectTrans.name} 缩放设置为 {_scale}");
    }
}
