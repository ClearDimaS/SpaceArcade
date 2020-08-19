using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [Tooltip("Enemies settings List")]
    [SerializeField] private LevelDatabase levelsDatabase;

    [SerializeField]
    private List<GameObject> LevelPages;

    private List<LevelData> lvlDataList;

    private List<LevelSelectBtnView> buttonsList;

    public static LevelController instance;

    private int LastLevelLoaded;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void OnEnable()
    {
        UpdateBtns();
    }

    private void Init()
    {
        lvlDataList = levelsDatabase.GetElementList();
        buttonsList = new List<LevelSelectBtnView>();
        foreach (GameObject page in LevelPages)
        {
            foreach (LevelSelectBtnView levelBtn in page.GetComponentsInChildren<LevelSelectBtnView>()) 
            {
                buttonsList.Add(levelBtn);
            }
        }
    }

    public void UpdateBtns() 
    {
        if (lvlDataList == null)
            Init();
        for (int i = 0; i < buttonsList.Count; i++)
        {
            if (lvlDataList.Count - 1 > i)
            {
                buttonsList[i].SetLevelNumber(lvlDataList[i].LevelNumber);

                buttonsList[i].LoadUIElements(lvlDataList[i].LevelStatus);
            }
            else 
            {
                buttonsList[i].gameObject.SetActive(false);
            }
        }
    }

    public int GetCurrentLevelEnemiesCount() 
    {
        return levelsDatabase[LastLevelLoaded].TotalEnemies;
    }

    public bool TryLoadLevel(int LoadedLevelNumber)
    {
        if (lvlDataList[LoadedLevelNumber - 1].LevelStatus == LevelStatusEnum.Locked)
        {
            return false;
        }
        else 
        {
            LastLevelLoaded = LoadedLevelNumber - 1;
            UI_Controller.SetActivePanel(UI_Controller.UI_Element.Game);
            return true;
        }
    }

    public void LoadNextLevel() 
    {
        LastLevelLoaded += 1;
        UI_Controller.SetActivePanel(UI_Controller.UI_Element.Game);
    }

    public void changeLevelStatus(bool isComplete)
    {
        if (isComplete)
        {
            lvlDataList[LastLevelLoaded].LevelStatus = LevelStatusEnum.Complete;

            if(LastLevelLoaded < lvlDataList.Count)
                lvlDataList[LastLevelLoaded].LevelStatus = LevelStatusEnum.Open;
        }
    }
}
