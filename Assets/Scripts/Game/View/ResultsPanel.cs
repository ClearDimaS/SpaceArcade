using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultsPanel : MonoBehaviour
{
    [SerializeField]
    GameController gameController;

    [SerializeField]
    Text ResultPointsText;

    [SerializeField]
    GameObject RatingParent;

    int points = 40;

    bool ifComplete;

    public void OnDisable()
    {
        gameObject.SetActive(false);
    }

    public void gameOver(bool _ifComplete)
    {
        ifComplete = _ifComplete;

        string resultSound = ifComplete ? Constants.SND_COMPLETE : Constants.SND_FAIL;

        AudioManager.instance.Play(resultSound);

        UpdLevelRating(ifComplete);

        StartCoroutine(ChangePointsText());
    }

    private void UpdLevelRating(bool ifComplete)
    {
        Animator anim = RatingParent.GetComponentsInChildren<Animator>()[0];

        if(ifComplete)
            anim.gameObject.GetComponent<Image>().color = Color.white;
        else
            anim.gameObject.GetComponent<Image>().color = Color.grey;

        if (anim.gameObject.activeSelf)
        {
            if (anim.enabled)
                anim.Play(Constants.ANIM_LEVEL_COMPLETE);
        }
    }

    IEnumerator ChangePointsText()
    {
        float _rewardTemp = 0;

        float add = (points - _rewardTemp) / 100.0f;

        while (_rewardTemp - 1 <= points)
        {
            ResultPointsText.text = " + " + (int)(_rewardTemp);

            _rewardTemp += add;

            if (_rewardTemp > points)
                yield break;

            yield return null;
        }
    }

    public void LoadNextLevelButtonPressed() 
    {
        gameController.LoadNextLevel();
    }
}
