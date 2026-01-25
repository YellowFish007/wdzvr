using System.Collections;
using System.Collections.Generic;
using Engine;
using UnityEngine;

public class FriendData : Singleton<FriendData>
{
    [System.Serializable]
    public class FriendInfo
    {
        public int Id;              // 好友ID
        public string Name;         // 昵称
        public string HeadIcon;       // 头像图标路径或ID
        public bool IsOnline;       // 是否在线
        public int Level;           // 等级
        public string Signature;    // 个性签名

        public FriendInfo() { }

        public FriendInfo(int id, string name, string headIcon, int level, bool isOnline = false, string signature = "")
        {
            this.Id = id;
            this.Name = name;
            this.HeadIcon = headIcon;
            this.Level = level;
            this.IsOnline = isOnline;
            this.Signature = signature;
        }
    }

    // 存储所有好友信息 Key: ID, Value: Info
    private Dictionary<int, FriendInfo> friendMap = new Dictionary<int, FriendInfo>();

    // 存储好友申请列表
    private List<FriendInfo> friendApplyList = new List<FriendInfo>();

    /// <summary>
    /// 获取好友数量
    /// </summary>
    public int GetFriendCount()
    {
        return friendMap.Count;
    }

    /// <summary>
    /// 添加或更新好友
    /// </summary>
    public void AddFriend(FriendInfo friend)
    {
        if (friendMap.ContainsKey(friend.Id))
        {
            friendMap[friend.Id] = friend;
        }
        else
        {
            friendMap.Add(friend.Id, friend);
        }
    }

    /// <summary>
    /// 移除好友
    /// </summary>
    public void RemoveFriend(int id)
    {
        if (friendMap.ContainsKey(id))
        {
            friendMap.Remove(id);
        }
    }

    /// <summary>
    /// 获取好友信息
    /// </summary>
    public FriendInfo GetFriend(int id)
    {
        if (friendMap.ContainsKey(id))
        {
            return friendMap[id];
        }
        return null;
    }

    /// <summary>
    /// 获取所有好友列表
    /// </summary>
    public List<FriendInfo> GetAllFriends()
    {
        return new List<FriendInfo>(friendMap.Values);
    }
    
    /// <summary>
    /// 检查是否是好友
    /// </summary>
    public bool IsFriend(int id)
    {
        return friendMap.ContainsKey(id);
    }

    /// <summary>
    /// 获取申请列表
    /// </summary>
    public List<FriendInfo> GetApplyList()
    {
        return friendApplyList;
    }

    /// <summary>
    /// 添加申请
    /// </summary>
    public void AddApply(FriendInfo info)
    {
        // 去重
        for (int i = 0; i < friendApplyList.Count; i++)
        {
            if (friendApplyList[i].Id == info.Id)
            {
                friendApplyList[i] = info;
                return;
            }
        }
        friendApplyList.Add(info);
    }

    /// <summary>
    /// 移除申请
    /// </summary>
    public void RemoveApply(int id)
    {
        for (int i = friendApplyList.Count - 1; i >= 0; i--)
        {
            if (friendApplyList[i].Id == id)
            {
                friendApplyList.RemoveAt(i);
                break;
            }
        }
    }
}
