using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private LevelManager levelManager = null;

    public LevelManager LevelManager { get => this.levelManager; }

    private string _currentScene = "";
    public string CurrentScene { get => _currentScene; }
    
    [HideInInspector]
    public int CurrentPage = 0;

    public bool TestLevel = false;


    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
        initData();
    }

    public void LoadScene(string sceneName)
    {
        if (sceneName.Contains(SceneName.MainLevel))
        {
            LoadMainScene(sceneName);
        }
        else
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }
    }

    public async void LoadMainScene(string sceneName)
    {
        await loadSceneMain(sceneName);
    }

    private async UniTask loadSceneMain(string sceneName)
    {
        await SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        initSceneMain();
    }

    public async void LoadSceAsync(string sceneName)
    {
        if (sceneName.Contains(SceneName.MainLevel))
        {
            LoadMainScene(sceneName);
        }
        else
        {
            await loadSceneLevel(sceneName);
        }

    }

    private async UniTask loadSceneLevel(string sceneName)
    {        
        if (sceneName == "LevelTest")
        {
            await SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        }
        else
        {
            await SceneManager.LoadSceneAsync(SceneName.GamePlay, LoadSceneMode.Single);
        }
        initSceneLevel(sceneName);

    }

    private void initSceneLevel(string sceneName)
    {
        _currentScene = sceneName;
        levelManager = GameObject.FindAnyObjectByType<LevelManager>();
        GameConfig lvlConfig = DataManager.instance.GameConfig;
        var levelData = lvlConfig.GetDataLevel(sceneName);
        var stageList = lvlConfig.GetKeyStage();
        bool isFinal = stageList[stageList.Count - 1] == sceneName;
        levelManager.Init(levelData, isFinal);
    }

    private void initData()
    {
        DataManager.instance.InitData();
    }

    private void initSceneMain()
    {
        LoadLevel load = GameObject.FindAnyObjectByType<LoadLevel>();
        load.Init();
    }    
}
