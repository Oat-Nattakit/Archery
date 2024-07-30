using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    private static DataManager _instance;
    public static DataManager instance
    {
        get
        {
           if(_instance == null)            
                _instance = new DataManager();
                return _instance;            
        }
    }

    private GameConfig _gameConfig = new GameConfig();
    public GameConfig GameConfig { get => this._gameConfig; }

    public void InitData()
    {
        TextAsset getTextData = Resources.Load<TextAsset>("GameConfigData");
        _gameConfig.UpdateData(getTextData.ToString());
    }
    
}