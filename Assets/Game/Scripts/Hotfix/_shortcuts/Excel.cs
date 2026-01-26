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

    #region TbItem 物品表
    public static cfg.game.Item GetItem(int id)
    {
        return Tables.TbItem.GetOrDefault(id);
    }

    public static System.Collections.Generic.List<cfg.game.Item> GetItemDataList()
    {
        return Tables.TbItem.DataList;
    }

    public static string GetItemName(int id)
    {
        return GetItem(id)?.Name;
    }

    public static string GetItemDesc(int id)
    {
        return GetItem(id)?.Desc;
    }

    public static string GetItemIcon(int id)
    {
        return GetItem(id)?.Icon;
    }

    public static int GetItemType(int id)
    {
        var item = GetItem(id);
        return item != null ? item.Type : 0;
    }
    #endregion
}
