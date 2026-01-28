using System.Collections;
using System.Collections.Generic;
using Engine;
using UnityEngine;

public class SceneHistoryData : Singleton<SceneHistoryData>
{
    [System.Serializable]
    public class HistoryInfo
    {
        public int SceneId;
        public string Time;
        
        public HistoryInfo() {}
        public HistoryInfo(int sceneId, string time)
        {
            this.SceneId = sceneId;
            this.Time = time;
        }
    }

    public List<HistoryInfo> historyList = new List<HistoryInfo>();

    public void AddHistory(int sceneId)
    {
        HistoryInfo info = new HistoryInfo();
        info.SceneId = sceneId;
        info.Time = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        
        // Add to beginning
        historyList.Insert(0, info);
        
        // Save data
        DataManager.Instance.SaveData<SceneHistoryData>();
    }
    
    public List<HistoryInfo> GetAllHistory()
    {
        return historyList;
    }
}
