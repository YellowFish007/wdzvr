using UnityEngine;
using Engine;
using cfg;
using SimpleJSON;
using System;
using System.Threading.Tasks;

public class GameManager : SingletonGameObject<GameManager>
{

    private Tables mTables;

    public void Init()
    {
        Application.targetFrameRate = 60;

        Debug.Log("InitGame Start");

        //初始化Excel
        Debug.Log("InitGame: InitTables...");

        InitTables();
        
        LoadSceneAsync<Scene1001>();
        //UnityEngine.SceneManagement.SceneManager.LoadScene("SceneTest");
    }

    /// <summary>
    /// 初始化表
    /// </summary>
    private void InitTables()
    {
        JSONNode LoadJsonFile(string file)
        {
            Debug.Log($"InitTables: Loading file {file}");
            var asset = Asset.LoadAssetSync<TextAsset>(file);
            if (asset == null)
            {
                Debug.LogError($"InitTables: Failed to load asset {file}, returned null!");
                return null;
            }
            string str = asset.text;
            return JSON.Parse(str);
        }
        mTables = new(LoadJsonFile);
    }

    public Tables GetTables()
    {
        return mTables;
    }

    /// <summary>
    /// 异步加载场景
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="args"></param>
    public void LoadSceneAsync<T>(params object[] args) where T : SceneBase
    {
        Scene.LoadSceneAsync<T>(null, null, args);
    }

}
