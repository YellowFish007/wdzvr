using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Engine;

public class SceneUIManager : SingletonGameObject<SceneUIManager>
{
    public Transform Root { get; private set; }

    private Stack<UIBase> _uiStack = new Stack<UIBase>();

    private void Awake()
    {
        GameObject prefab = Asset.LoadAssetSync<GameObject>("Prefabs/UI/Root/UISceneRoot");
        GameObject rootGo;
        if (prefab != null)
        {
            rootGo = Instantiate(prefab);
            rootGo.name = "Root";
        }
        else
        {
            rootGo = new GameObject("Root");
        }
        Root = rootGo.transform;
        Root.SetParent(transform, false);
    }

    public void OpenUI(string name)
    {
        if (_uiStack.Count > 0)
        {
            var topUI = _uiStack.Peek();
            topUI.SetActive(false);
        }

        GameObject prefab = Asset.LoadAssetSync<GameObject>(name);
        if (prefab != null)
        {
            GameObject go = Instantiate(prefab, Root);
            UIBase ui = go.GetComponent<UIBase>();
            if (ui != null)
            {
                ui.Name = name;
                _uiStack.Push(ui);
                UpdateRootPosition();
                ui.SetActive(true);
                ui.OnCreate();
            }
            else
            {
                Debug.LogError($"Prefab {name} does not have UIBase component");
                Destroy(go);
            }
        }
        else
        {
            Debug.LogError($"Failed to load UI prefab: {name}");
        }
    }

    public void CloseUI(string name)
    {
        if (_uiStack.Count > 0)
        {
            var topUI = _uiStack.Peek();
            if (topUI.Name == name)
            {
                _uiStack.Pop();
                topUI.OnClose();
                Destroy(topUI.gameObject);

                if (_uiStack.Count > 0)
                {
                    var newTop = _uiStack.Peek();
                    UpdateRootPosition();
                    newTop.SetActive(true);
                }
            }
        }
    }

    public void SetRootActive(bool active)
    {
        if (Root != null)
        {
            Root.gameObject.SetActive(active);
        }
    }

    private void UpdateRootPosition()
    {
        if (Camera.main != null && Root != null)
        {
            Transform camTrans = Camera.main.transform;
            Root.position = camTrans.position + camTrans.forward * 2.0f;
            Root.rotation = camTrans.rotation;
        }
    }
}
