using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadLevel : MonoBehaviour
{
    public PageScrolling pageScrolling = null;
    public GameObject PrefabsPanel;
    public GameObject PrefabsButton;
    public GameObject ParentPanel;

    private List<GameObject> parentButton = new List<GameObject>();
    private List<Button> LevelBtn = new List<Button>();
    private List<int> LevelStar = new List<int>();

    public Sprite[] imgIsEnabledOrNo;
    public Sprite[] Rating;
    public string TextLoad;  

    public void Init()
    {
        var GameConfig = DataManager.instance.GameConfig;
        int limitCapter = GameConfig.LimitCapter;
        List<string> levelName = GameConfig.GetKeyStage();
        createPanelPage(limitCapter, levelName.Count);
        createButton(limitCapter, levelName);
        pageScrolling.Init();
        initButtonLevel(levelName);
    }


    private void createPanelPage(int limitCapter, int totalStage)
    {
        int panelCount = Mathf.RoundToInt(totalStage / limitCapter) + 1;
        for (int i = 0; i < panelCount; i++)
        {
            GameObject page = GameObject.Instantiate(PrefabsPanel, ParentPanel.transform, false);
            page.name = "PanelLevel" + (i + 1);
            parentButton.Add(page);
        }
    }

    private void createButton(int limitCapter, List<string> levelName)
    {
        for (int i = 0; i < levelName.Count; i++)
        {
            int indexParent = (int)Mathf.Round(i / limitCapter);
            GameObject newBtn = GameObject.Instantiate(PrefabsButton, parentButton[indexParent].transform, false);
            newBtn.name = levelName[i];
            Button lvlBtn = newBtn.GetComponent<Button>();
            LevelBtn.Add(lvlBtn);
            
            if (GameManager.instance.TestLevel)
            {
                PlayerPrefs.SetInt(levelName[i], 1);
            }
        }
    }
    private void initButtonLevel(List<string> levelName)
    {
        int count = 0;
        for (int i = 0; i < levelName.Count; i++)
        {
            string lvlName = levelName[i];
            SelectScene selectBtn = LevelBtn[i].gameObject.GetComponent<SelectScene>();
            selectBtn.sceneName = lvlName;
            selectBtn.textLevel.text = (i + 1).ToString("00");
            selectBtn.InitButton(false);
            int starCount = PlayerPrefs.GetInt(lvlName);
            LevelStar.Add(starCount);

            if (starCount > 0)
            {
                for (int index = 0; index < starCount; index++)
                {
                    selectBtn.rating.enabled = true;
                    selectBtn.rating.sprite = Rating[index];
                }
                selectBtn.InitButton(true);
                count++;
            }
            else
            {
                int targetIndex = (i - 1);
                if (targetIndex >= 0)
                {
                    bool beforPass = PlayerPrefs.HasKey(levelName[targetIndex]);
                    selectBtn.InitButton(beforPass);
                }
            }
        }
        if (count <= 0)
        {
            SelectScene selectLevel1 = LevelBtn[0].gameObject.GetComponent<SelectScene>();
            selectLevel1.InitButton(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad9))
        {
            PlayerPrefs.DeleteAll();
        }
    }
}
