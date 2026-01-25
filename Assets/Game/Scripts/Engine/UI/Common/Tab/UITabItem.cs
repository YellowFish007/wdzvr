using System;
using UnityEngine;
using UnityEngine.UI;

public class UITabItem : MonoBehaviour
{
    [Header("配置")]
    public GameObject viewObject; // 关联的 View

    [Header("状态物体")]
    public GameObject selectedObj; // 选中状态显示的物体
    public GameObject normalObj;   // 未选中状态显示的物体

    private int _index;
    private Action<int> _onClickCallback;
    private Button _btn;

    public void Init(int index, Action<int> onClick)
    {
        _index = index;
        _onClickCallback = onClick;

        // 自动添加 Image (如果缺失)，用于接收点击
        var img = GetComponent<Image>();
        if (img == null)
        {
            img = gameObject.AddComponent<Image>();
            img.color = new Color(0, 0, 0, 0); // 透明，只用于点击区域
        }

        // 自动添加 Button (如果缺失)
        _btn = GetComponent<Button>();
        if (_btn == null)
        {
            _btn = gameObject.AddComponent<Button>();
            _btn.transition = Selectable.Transition.None; // 默认无过渡效果
        }

        _btn.onClick.RemoveAllListeners();
        _btn.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        _onClickCallback?.Invoke(_index);
    }

    public void SetSelected(bool isSelected)
    {
        // 切换 View 显示
        if (viewObject != null)
        {
            viewObject.SetActive(isSelected);
        }

        // 切换选中态物体显示
        if (selectedObj != null)
        {
            selectedObj.SetActive(isSelected);
        }

        // 切换未选中态物体显示
        if (normalObj != null)
        {
            normalObj.SetActive(!isSelected);
        }
    }
}
