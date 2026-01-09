using cfg;
using cfg.game;
using System.Collections.Generic;
using System.Linq;
public static partial class Excel
{
    public static Tables Tables 
    { 
        get 
        { 
            var tables = GameManager.Instance.GetTables();
            if (tables == null)
            {
                UnityEngine.Debug.LogError("Excel.Tables is null! Make sure GameManager.Init() has been called and Tables are initialized.");
            }
            return tables; 
        } 
    }

    #region TbScene 场景表
    public static cfg.game.Scene GetScene(int id)
    {
        return Tables.TbScene.GetOrDefault(id);
    }

    public static System.Collections.Generic.List<cfg.game.Scene> GetSceneDataList()
    {
        return Tables.TbScene.DataList;
    }

    public static string GetSceneName(int id)
    {
        return GetScene(id)?.NameId;
    }
    
    public static string GetSceneRes(int id)
    {
        return GetScene(id)?.SceneRes;
    }

    public static string GetSceneDesc(int id)
    {
        return GetScene(id)?.Desc;
    }

    public static string GetSceneIcon(int id)
    {
        return GetScene(id)?.Icon;
    }

    public static string GetSceneVideo(int id)
    {
        return GetScene(id)?.Video;
    }
    #endregion
}
