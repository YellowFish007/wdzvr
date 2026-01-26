using System.Collections;
using System.Collections.Generic;
using Engine;
using UnityEngine;

public class BagData : Singleton<BagData>
{
    [System.Serializable]
    public class ItemInfo
    {
        public int Id;          // 物品配置ID
        public int Count;       // 数量

        public ItemInfo() { }

        public ItemInfo(int id, int count)
        {
            this.Id = id;
            this.Count = count;
        }
    }

    // 存储物品 Key: ConfigID
    private Dictionary<int, ItemInfo> itemMap = new Dictionary<int, ItemInfo>();

    /// <summary>
    /// 添加物品
    /// </summary>
    public void AddItem(int id, int count)
    {
        if (itemMap.ContainsKey(id))
        {
            itemMap[id].Count += count;
        }
        else
        {
            itemMap.Add(id, new ItemInfo(id, count));
        }
    }

    /// <summary>
    /// 移除物品
    /// </summary>
    public bool RemoveItem(int id, int count)
    {
        if (itemMap.ContainsKey(id))
        {
            if (itemMap[id].Count >= count)
            {
                itemMap[id].Count -= count;
                if (itemMap[id].Count <= 0)
                {
                    itemMap.Remove(id);
                }
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// 获取物品数量
    /// </summary>
    public int GetItemCount(int id)
    {
        if (itemMap.ContainsKey(id))
        {
            return itemMap[id].Count;
        }
        return 0;
    }

    /// <summary>
    /// 获取所有物品列表
    /// </summary>
    public List<ItemInfo> GetAllItems()
    {
        return new List<ItemInfo>(itemMap.Values);
    }
}
