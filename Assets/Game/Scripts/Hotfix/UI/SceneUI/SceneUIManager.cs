using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Engine;

public class SceneUIManager : SingletonGameObject<SceneUIManager>
{
    public Transform Root { get; private set; }

    private Dictionary<string, UISceneBase> _uiMap = new Dictionary<string, UISceneBase>();
    private Stack<UISceneBase> _uiStack = new Stack<UISceneBase>();

    private void Awake()
    {
        GameObject prefab = Asset.LoadAssetSync<GameObject>("UISceneRoot");
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

    public void Register(UISceneBase ui)
    {
        if (ui == null) return;
        if (!_uiMap.ContainsKey(ui.Name))
        {
            _uiMap.Add(ui.Name, ui);
        }
        else
        {
            _uiMap[ui.Name] = ui;
        }
    }

    public void Unregister(UISceneBase ui)
    {
        if (ui == null) return;
        if (_uiMap.ContainsKey(ui.Name))
        {
            _uiMap.Remove(ui.Name);
        }
    }

    public T GetUI<T>(string name) where T : UISceneBase
    {
        if (_uiMap.TryGetValue(name, out var ui))
        {
            return ui as T;
        }
        return null;
    }
    
    public void OpenUI(string name)
    {
        if (_uiMap.TryGetValue(name, out var ui))
        {
            if (_uiStack.Count > 0)
            {
                var topUI = _uiStack.Peek();
                if (topUI == ui) return;
                
                topUI.SetActive(false);
            }

            _uiStack.Push(ui);
            UpdateRootPosition();
            ui.SetActive(true);
            ui.OnOpen();
        }
    }

    public void CloseUI(string name)
    {
        if (_uiMap.TryGetValue(name, out var ui))
        {
            if (_uiStack.Count > 0 && _uiStack.Peek() == ui)
            {
                _uiStack.Pop();
                ui.OnClose();
                ui.SetActive(false);

                if (_uiStack.Count > 0)
                {
                    var topUI = _uiStack.Peek();
                    UpdateRootPosition();
                    topUI.SetActive(true);
                    topUI.OnOpen();
                }
            }
            else
            {
                // Closing a UI that is not at the top of the stack, or just closing it generally
                // For now, if it's not the top, we might just hide it or remove it from stack (which is hard with Stack<T>)
                // If the user requirement implies strictly stack behavior, we usually only close the top.
                // But if random access close is allowed, we'd need a List instead of Stack.
                // Assuming "Close one, previous opens" implies stack operation, we'll focus on top.
                // If it's not at top, just hide it.
                ui.SetActive(false);
                ui.OnClose();
            }
        }
    }

    public T CreateUI<T>(string path, Transform parent = null) where T : UISceneBase
    {
        GameObject prefab = Asset.LoadAssetSync<GameObject>(path);
        if (prefab != null)
        {
            if (parent == null)
            {
                parent = Root;
            }
            GameObject go = Instantiate(prefab, parent);
            T ui = go.GetComponent<T>();
            if (ui != null)
            {
                Register(ui);
                return ui;
            }
        }
        return null;
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
