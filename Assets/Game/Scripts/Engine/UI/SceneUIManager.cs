using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Engine;

public class SceneUIManager : SingletonGameObject<SceneUIManager>
{
    public Transform Root { get; private set; }
    private Canvas _rootCanvas;

    private Stack<UIBase> _uiStack = new Stack<UIBase>();
    private Dictionary<string, UIBase> _persistentUIs = new Dictionary<string, UIBase>();

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
        _rootCanvas = rootGo.GetComponent<Canvas>();
    }

    public UIBase OpenUI(string name, params object[] args)
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
                ui.OnCreate(args);
                go.transform.SetAsFirstSibling();
                return ui;
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
        return null;
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
                return;
            }
        }

        if (_persistentUIs.ContainsKey(name))
        {
            var ui = _persistentUIs[name];
            ui.OnClose();
            ui.SetActive(false);
        }
    }

    public void OpenPersistentUI(string name, params object[] args)
    {
        if (_persistentUIs.ContainsKey(name))
        {
            return;
        }

        GameObject prefab = Asset.LoadAssetSync<GameObject>(name);
        if (prefab != null)
        {
            GameObject go = Instantiate(prefab, Root);
            UIBase ui = go.GetComponent<UIBase>();
            if (ui != null)
            {
                ui.Name = name;
                _persistentUIs.Add(name, ui);
                ui.SetActive(true);
                ui.OnCreate(args);
                go.transform.SetAsLastSibling();
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

    public void ClosePersistentUI(string name)
    {
        if (_persistentUIs.ContainsKey(name))
        {
            var ui = _persistentUIs[name];
            _persistentUIs.Remove(name);
            ui.OnClose();
            Destroy(ui.gameObject);
        }
    }

    public void SetRootActive(bool active)
    {
        if (Root != null)
        {
            if (active)
            {
                UpdateRootPosition();
            }
            Root.gameObject.SetActive(active);
        }
    }

    public void ToggleRootActive()
    {
        if (Root != null)
        {
            SetRootActive(!Root.gameObject.activeSelf);
        }
    }

    private void UpdateRootPosition()
    {
        if (Camera.main != null && Root != null)
        {
            Transform camTrans = Camera.main.transform;
            Vector3 eulerAngles = camTrans.rotation.eulerAngles;
            Quaternion lookRot = Quaternion.Euler(0, eulerAngles.y, 0);
            
            Root.position = camTrans.position + (lookRot * Vector3.forward) * 2.0f;
            Root.rotation = lookRot;

            if (_rootCanvas != null)
            {
                _rootCanvas.worldCamera = Camera.main;
            }
        }
    }
}
