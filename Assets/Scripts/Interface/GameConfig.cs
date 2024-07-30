using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConfig
{
    private Dictionary<string, LevelData> DataLevel = new Dictionary<string, LevelData>();
    private int _limitCapter = 0;
    public int LimitCapter { get => _limitCapter; }

    public void UpdateData(string data)
    {
        var Data = JsonUtility.FromJson<GameConfigData>(data);
        var LevelData = Data.GameConfig;
        _limitCapter = Data.LimitCapter;
        _setupDataDictionary(LevelData);
    }

    private void _setupDataDictionary(List<LevelData> LevelData)
    {
        LevelData.ForEach((value) =>
        {
            DataLevel.Add(value.KeyLevel, value);
        });
    }

    public LevelData GetDataLevel(string Key)
    {
        LevelData _leveldata = new LevelData();
        if (DataLevel.ContainsKey(Key))
        {
            _leveldata = DataLevel[Key];
        }
        return _leveldata;
    }



    public int GetCountData()
    {
        return DataLevel.Count;
    }

    public List<string> GetKeyStage()
    {
        List<string> StageKey = new List<string>();

        foreach (var (key, value) in DataLevel)
        {
            StageKey.Add(key);
        }
        return StageKey;
    }
    public string GetNextLevel(string Key)
    {
        var stageList = GetKeyStage();
        int index = stageList.IndexOf(Key);
        string nextLevelKey = stageList[index + 1];
        return nextLevelKey;
    }

}

[System.Serializable]
public class GameConfigData
{
    public int LimitCapter;
    public List<LevelData> GameConfig;
}

[System.Serializable]
public class LevelData
{
    public string KeyLevel;
    public LevelConfig Config;
    public RatingConfig Rating;
}

[System.Serializable]
public class LevelConfig
{
    public int Arrow;
    public int Enemy;
}

[System.Serializable]
public class RatingConfig
{
    public int MaxStar;
    public int HalfStar;
    public int MinStar;
}