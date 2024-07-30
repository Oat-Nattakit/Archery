using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public Rating Rating = null;
    public TimeStart timeStart = null;
    public LevelAsset levelAsset = null;

    public NextSceneOrQuit NextSceneOrQuit = null;
    public Quiver quiver;

    private int MaxArrows;
    private int MaxEnemies;

    public bool GameEnd = true;
    public bool option = false;
    private bool finalLevel = false;

    public Text MunitionText;
    public Text LevelText;
    [HideInInspector] public TextMesh powerText;

    public GameObject WinWindow;
    public GameObject LoseWindow;
    public GameObject windowOption;
    public GameObject btnOption;
    public GameObject[] starForRating;
    public AudioClip Winner;
    public AudioClip Loser;

    private int CurrentArrows;
    private int HitedEnemies = 0;
    private string level;

    public void Init(LevelData data, bool isFinal)
    {
        initLevelData(data, isFinal);
        initMapLevel();
        initQuiver();
        setNextLevel();
    }

    private void initLevelData(LevelData data, bool isFinal)
    {
        level = data.KeyLevel;
        LevelText.text = level;
        MaxArrows = data.Config.Arrow;
        CurrentArrows = MaxArrows;
        MaxEnemies = data.Config.Enemy;
        MunitionText.text = CurrentArrows.ToString();
        finalLevel = isFinal;
    }

    private void initMapLevel()
    {   
        levelAsset.InitLevel(level);
        timeStart.Hand = FireController.m_instance.Hand;
        powerText = FireController.m_instance.AngleText;
    }

    private void initQuiver()
    {
        quiver.Init(MaxArrows);
    }

    private void setNextLevel()
    {
        string nextLevel = SceneName.MainLevel;
        if (!finalLevel)
        {
            nextLevel = DataManager.instance.GameConfig.GetNextLevel(level);
        }
        NextSceneOrQuit.NextsceneName = nextLevel;
    }

    public void IncreateHitEnemies(int value)
    {
        HitedEnemies += value;
    }

    public void UpdateCurrentArrow(int value)
    {
        CurrentArrows += value;
        quiver.DecreaseArrow();
        quiver.UpdateCurrentArrowText(CurrentArrows);
    }

    public void verify()
    {
        int rate = Rating.rateLevel(level, CurrentArrows);
        AudioSource audio = Camera.main.gameObject.GetComponent<AudioSource>();
        if (CurrentArrows == 0)
        {
            if (HitedEnemies < MaxEnemies)
            {

                WinWindow.SetActive(false);
                LoseWindow.SetActive(true);
                audio.Stop();
                audio.PlayOneShot(Loser);
                audio.loop = false;
                GameEnd = true;
            }
            else if (HitedEnemies == MaxEnemies)
            {
                int comparStar = PlayerPrefs.GetInt(level);
                WinWindow.SetActive(true);
                LoseWindow.SetActive(false);
                audio.Stop();
                audio.PlayOneShot(Winner);
                audio.loop = false;
                if (rate > comparStar)
                {
                    PlayerPrefs.SetInt(level, rate);
                }
                else
                {
                    PlayerPrefs.SetInt(level, comparStar);
                }
                for (int i = 0; i < rate; i++)
                {
                    starForRating[i].SetActive(true);
                }
                GameEnd = true;
            }
        }
        else
        {
            if (HitedEnemies == MaxEnemies)
            {
                int comparStar = PlayerPrefs.GetInt(level);
                WinWindow.SetActive(true);
                LoseWindow.SetActive(false);
                audio.Stop();
                audio.PlayOneShot(Winner);
                audio.loop = false;
                if (rate > comparStar)
                {
                    PlayerPrefs.SetInt(level, rate);
                }
                else
                {
                    PlayerPrefs.SetInt(level, comparStar);
                }
                for (int i = 0; i < rate; i++)
                {
                    starForRating[i].SetActive(true);
                }
                GameEnd = true;
            }
        }
    }

    public void ShowOption()
    {
        if (windowOption.activeSelf)
        {
            windowOption.SetActive(false);
            option = false;
        }
        else
        {
            windowOption.SetActive(true);
            option = true;
        }
    }
}
