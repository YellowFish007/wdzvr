using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
namespace Engine
{
    public class UIManager : SingletonGameObject<UIManager>
    {
        //UI节点
        private GameObject m_UIRoot;
        //所有的UIBase列表
        private List<UIBase> m_UIList;
        //摄像机
        private Camera m_Camera;

        public bool IsInit = false;
        public void Init()
        {
            LoadUIRoot();

            m_UIList = new List<UIBase>();
        }
        /// <summary>
        /// 获取UI节点
        /// </summary>
        /// <returns></returns>
        private void LoadUIRoot()
        {
            Asset.LoadPrefabAsync("UI/Base/UIRoot", delegate (GameObject prefab)
            {
                GameObject uiRoot = GameObject.Instantiate(prefab);
                GameObject.DontDestroyOnLoad(uiRoot);
                uiRoot.name = "UIRoot";
                //设置父节点
                uiRoot.transform.SetParent(transform);

                m_UIRoot = uiRoot;

                m_Camera = m_UIRoot.Find("Camera").GetComponent<Camera>();

                IsInit = true;
            });
        }

        public Camera GetCamera()
        {
            return m_Camera;
        }

        /// <summary>
        /// 获取UIbase,没有就New一个
        /// </summary>
        /// <param name="uiPath"></param>
        public UIBase GetUI(string uiPath)
        {
            string id = uiPath;

            for (int i = 0; i < m_UIList.Count; i++)
            {
                if (m_UIList[i].id == id)
                {
                    return m_UIList[i];
                }
            }
            return null;
        }

        private void LoadUIBase(string uiPath, Action<UIBase> callBack)
        {
            UIBase uiBase = GetUI(uiPath);

            if (uiBase == null)
            {
                //实例化一个预制

                Asset.LoadPrefabAsync(uiPath, delegate (GameObject prefab)
                {

                    GameObject obj = Instantiate(prefab);
                    obj.name = GetLastNameFromPath(uiPath);

                    //设置父节点
                    obj.transform.SetParent(m_UIRoot.transform);

                    //得到UIbase
                    uiBase = obj.GetComponent<UIBase>();
                    uiBase.id = uiPath;
                    //添加Canvas
                    uiBase.AddBaseCanvas();

                    //重置位置
                    uiBase.ResetTransform();
                    //添加一个UI
                    m_UIList.Add(uiBase);

                    //设置层级
                    ResetCanvasDepth(uiBase);

                    callBack(uiBase);

                });
            }
            else
            {
                callBack(uiBase);
            }
        }

        /// <summary>
        /// 打开一个界面
        /// </summary>
        /// <param name="uiPath"></param>
        /// <param name="arg"></param>
        public void OpenUI(string uiPath, params object[] args)
        {
            //得到UIbase
            LoadUIBase(uiPath, delegate (UIBase uiBase)
            {
                //已经存在则直接打开
                if (uiBase.IsInit)
                {
                    uiBase.OnEnter(args);
                    uiBase.OnFreshLanguage();
                }
                else
                {
                    uiBase.IsInit = true;

                    uiBase.OnPreload(delegate ()
                    {
                        uiBase.OnCreate(args);
                        uiBase.OnEnter(args);
                        uiBase.OnFreshLanguage();
                    });
                }
            });
        }

        /// <summary>
        /// 关闭界面
        /// </summary>
        /// <param name="uiConfig"></param>
        public void CloseUI(string uiPath)
        {
            CloseUIByID(uiPath);
        }

        /// <summary>
        /// 通过ID关闭界面
        /// </summary>
        /// <param name="id"></param>
        public void CloseUIByID(string id)
        {
            UIBase uiBase = null;
            for (int i = 0; i < m_UIList.Count; i++)
            {
                if (id == m_UIList[i].id)
                {
                    uiBase = m_UIList[i];
                    break;
                }
            }

            if (uiBase != null)
            {
                //是常驻的就不删除直接返回
                if (uiBase.isDontDestroy)
                {
                    return;
                }

                uiBase.OnExit();
                uiBase.OnClose();

                //销毁对象
                Destroy(uiBase.gameObject);

                m_UIList.Remove(uiBase);
            }
        }

        /// <summary>
        /// 关闭所有界面
        /// </summary>
        public void CloseAllUI()
        {
            int dontDestroyCount = 0;

            //遍历所有的UI找到需要删除的UI去删除并且保存常驻的
            for (int i = 0; i < m_UIList.Count; i++)
            {
                UIBase uiBase = m_UIList[i];

                string id = uiBase.id;
                bool isDontDestroy = uiBase.isDontDestroy;
                if (isDontDestroy)
                {
                    dontDestroyCount++;
                }
                else
                {
                    CloseUIByID(id);
                    break;
                }
            }

            if (m_UIList.Count > 0)
            {
                //如果这次删除了就再执行一次，不然就没删除
                if (dontDestroyCount != m_UIList.Count)
                {
                    CloseAllUI();
                }
            }

        }
        /// <summary>
        /// 刷新UI
        /// </summary>
        /// <param name="uiConfig"></param>
        public void FreshAllUI()
        {

            //遍历所有的UI找到需要删除的UI去删除并且保存常驻的
            for (int i = 0; i < m_UIList.Count; i++)
            {
                UIBase uiBase = m_UIList[i];
                uiBase.OnFresh(null);
            }
        }
        /// <summary>
        /// 刷新UI
        /// </summary>
        /// <param name="uiConfig"></param>
        public void FreshUI(string uiPath, params object[] args)
        {
            UIBase uiBase = GetUI(uiPath);

            if (uiBase != null)
            {
                uiBase.OnFresh(args);
            }
        }

        /// <summary>
        /// 设置层级
        /// </summary>
        private void ResetCanvasDepth(UIBase showUIBase)
        {
            //移除旧数据
            int index = -1;
            for (int i = 0; i < m_UIList.Count; i++)
            {
                if (showUIBase.id == m_UIList[i].id)
                {
                    index = i;
                }
            }

            if (index >= 0)
            {
                m_UIList.RemoveAt(index);
            }
            m_UIList.Add(showUIBase);

            //初始化层级
            for (int i = 0; i < m_UIList.Count; i++)
            {
                UIBase uiBase = m_UIList[i];
                Canvas canvas = uiBase.GetComponent<Canvas>();
                canvas.overrideSorting = true;
                int depth = (int)uiBase.uiType * 1000 + i * 10;
                canvas.sortingOrder = depth;

                //重置粒子层级
                ResetParticleDepth(canvas, depth);
            }
        }

        private void ResetParticleDepth(Canvas canvas, int depth)
        {
            ParticleSystem[] particles = canvas.GetComponentsInChildren<ParticleSystem>(true);
            foreach (ParticleSystem particle in particles)
            {
                var renderer = particle.GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.sortingOrder = depth + 1;
                }
            }
        }

        /// <summary>
        /// 获取路径中的最后一个名称（以'/'分割）
        /// </summary>
        /// <param name="path">输入路径</param>
        /// <returns>最后一个分割后的字符串</returns>
        public static string GetLastNameFromPath(string path)
        {
            if (string.IsNullOrEmpty(path))
                return string.Empty;

            string[] parts = path.Split('/');
            return parts.Length > 0 ? parts[parts.Length - 1] : string.Empty;
        }

        /// <summary>
        /// 显示Reporter
        /// </summary>
        public void ShowReporter()
        {
            Asset.LoadPrefabAsync("UI/Base/Reporter", delegate (GameObject prefab)
            {
                GameObject reporter = Instantiate(prefab);
                DontDestroyOnLoad(reporter);
            });
        }
    }
}