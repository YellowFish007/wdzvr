using System.Collections;
using System.Collections.Generic;
using Engine;
using UnityEngine;

public class ChatData : Singleton<ChatData>
{
    public enum ChatType
    {
        Text = 0,   // 字符串数据
        Voice = 1,  // 语音数据
        Emoji = 2   // 表情
    }

    [System.Serializable]
    public class ChatMsg 
    {
        public ChatType Type;
        
        // 字符串内容 (文本消息) 或 表情ID/名称 (表情消息)
        public string Content;
        
        // 语音数据 (如果是语音类型)
        public byte[] VoiceData;
        
        // 语音时长 (秒)
        public float VoiceDuration;

        // 发送者ID (可选)
        public int SenderId;

        // 辅助属性
        public bool IsEmoji => Type == ChatType.Emoji;
        public bool IsVoice => Type == ChatType.Voice;
        public bool IsText => Type == ChatType.Text;
        
        // 构造函数
        public ChatMsg() { }
        
        public static ChatMsg CreateText(int senderId, string text)
        {
            return new ChatMsg { Type = ChatType.Text, SenderId = senderId, Content = text };
        }
        
        public static ChatMsg CreateEmoji(int senderId, string emojiId)
        {
            return new ChatMsg { Type = ChatType.Emoji, SenderId = senderId, Content = emojiId };
        }
        
        public static ChatMsg CreateVoice(int senderId, byte[] data, float duration)
        {
            return new ChatMsg { Type = ChatType.Voice, SenderId = senderId, VoiceData = data, VoiceDuration = duration };
        }
    }

    // 存储好友ID对应的聊天记录
    // Key: 好友ID, Value: 该好友的聊天消息列表
    private Dictionary<int, List<ChatMsg>> friendChats = new Dictionary<int, List<ChatMsg>>();

    /// <summary>
    /// 获取所有聊天记录字典
    /// </summary>
    public Dictionary<int, List<ChatMsg>> GetAllChats()
    {
        return friendChats;
    }

    /// <summary>
    /// 获取指定好友的聊天记录
    /// </summary>
    public List<ChatMsg> GetFriendMessages(int friendId)
    {
        if (!friendChats.ContainsKey(friendId))
        {
            friendChats[friendId] = new List<ChatMsg>();
        }
        return friendChats[friendId];
    }

    /// <summary>
    /// 添加一条消息到指定好友的聊天记录中
    /// </summary>
    public void AddMessage(int friendId, ChatMsg msg)
    {
        var list = GetFriendMessages(friendId);
        list.Add(msg);
    }
}
