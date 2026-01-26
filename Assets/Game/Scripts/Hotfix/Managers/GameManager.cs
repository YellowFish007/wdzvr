using UnityEngine;
using Engine;
using cfg;
using SimpleJSON;
using System;
using System.Threading.Tasks;
using UnityTimer;

public class GameManager : SingletonGameObject<GameManager>
{

    private Tables mTables;

    public void Init()
    {
        Application.targetFrameRate = 60;

        SoundManager.Instance.Init();

        Debug.Log("InitGame Start");

        //初始化Excel
        Debug.Log("InitGame: InitTables...");
        InitTables();

        // 初始化测试数据
        InitChatTestData();
        InitFriendTestData();
        InitBagTestData();

        this.AttachTimer(1.0f, delegate ()
        {

            LoadSceneAsync<Scene1001>();

        });


        //UnityEngine.SceneManagement.SceneManager.LoadScene("SceneTest");
    }

    /// <summary>
    /// 初始化表
    /// </summary>
    public void InitTables()
    {
        JSONNode LoadJsonFile(string file)
        {
            Debug.Log($"InitTables: Loading file {file}");
            var asset = Asset.LoadAssetSync<TextAsset>("RawAssets/Text/Excel/" + file);
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

    public void InitChatTestData()
    {
        int friendId = 1001; // 测试好友ID
        ChatData.Instance.AddMessage(friendId, ChatData.ChatMsg.CreateText("你好，最近怎么样？"));
        ChatData.Instance.AddMessage(friendId, ChatData.ChatMsg.CreateText("我正在测试聊天功能。"));
        ChatData.Instance.AddMessage(friendId, ChatData.ChatMsg.CreateText("这是一个非常长的文本消息，用来测试聊天气泡的自动换行和高度适配功能是否正常工作。如果一切正常，背景图应该会随着文字内容自动拉伸。"));
        ChatData.Instance.AddMessage(friendId, ChatData.ChatMsg.CreateEmoji("1"));

        Debug.Log("Chat test data initialized.");
    }

    public void InitFriendTestData()
    {
        // 添加几个测试好友
        FriendData.Instance.AddFriend(new FriendData.FriendInfo(1001, "Alice", "icon_avatar_01", 10, true, "Hello World!"));
        FriendData.Instance.AddFriend(new FriendData.FriendInfo(1002, "Bob", "icon_avatar_02", 5, false, "Busy..."));
        FriendData.Instance.AddFriend(new FriendData.FriendInfo(1003, "Charlie", "icon_avatar_03", 20, true, "Gaming time"));
        FriendData.Instance.AddFriend(new FriendData.FriendInfo(1004, "David", "icon_avatar_04", 1, false, "Newbie"));

        FriendData.Instance.AddApply(new FriendData.FriendInfo(1005, "David1", "icon_avatar_04", 1, true, "Newbie2"));
        FriendData.Instance.AddApply(new FriendData.FriendInfo(1006, "David2", "icon_avatar_04", 1, false, "Newbie1"));

        Debug.Log("Friend test data initialized.");
    }

    public void InitBagTestData()
    {
        // 添加测试背包数据
        for (int i = 200001; i <= 200029; i++)
        {
            // 随机数量 1-100
            BagData.Instance.AddItem(i, UnityEngine.Random.Range(1, 101));
        }

        Debug.Log("Bag test data initialized.");
    }

    /// <summary>
    /// 显示飘字提示
    /// </summary>
    /// <param name="content">提示内容</param>
    public void ShowFlyTip(string content)
    {
        GameEvent.Send(EventConfig.UI_SHOW_FLYTIP, content);
    }

    public void ShowConfirm(string content, Action action)
    {
        SceneUIManager.Instance.OpenPersistentUI(UIConfig.Confirm, content, action);
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
