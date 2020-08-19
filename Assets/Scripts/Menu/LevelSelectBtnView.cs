using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectBtnView : MonoBehaviour
{

    int lvlNumber;
    LevelStatusEnum levelStatus;

    public void SetLevelNumber(int _lvlNumber) 
    {
        lvlNumber = _lvlNumber;
    }

    public void TryLoadLevel()   // Switches scene to the game
    {
        if (loadLevel() == false)
            cancelLoadLevel();
    }

    private void cancelLoadLevel()
    {
        gameObject.GetComponent<Animator>().Play(Constants.ANIM_LEVEL_LOCKED);
    }

    private bool loadLevel()
    {
        return LevelController.instance.TryLoadLevel(lvlNumber);
    }

    public void LoadUIElements(LevelStatusEnum LevelStatus)    // Loads UI elements of the lvl select btn
    {
        if (LevelStatus != LevelStatusEnum.Locked) 
        {
            GetComponentsInChildren<Image>()[1].color = Color.clear;

            GetComponentsInChildren<Image>()[2].color = Color.white;

            if (LevelStatus == LevelStatusEnum.Complete)
                GetComponentsInChildren<Image>()[2].sprite = Resources.Load<Sprite>(Constants.LEVEL_RATING + 3);
            else
                GetComponentsInChildren<Image>()[2].sprite = Resources.Load<Sprite>(Constants.LEVEL_RATING + 0);
        }
        else 
        {
            GetComponentsInChildren<Image>()[1].color = Color.black;

            GetComponentsInChildren<Image>()[2].color = Color.clear;
        }

        GetComponentInChildren<Text>().text = lvlNumber.ToString();
    }
}
