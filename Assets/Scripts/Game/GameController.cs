using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    Player playerObj;

    [SerializeField]
    PlayerUI playerUI;

    [SerializeField]
    ResultsPanel resultsPanel;

    [SerializeField]
    GameOverlay gameOverlay;

    int PointsForVictory;

    public Action OnGameOver;

    private int points;

    private void Start()
    {
        playerObj.onLoseHealth += UpdateHealth;
        Bullet.OnBulletHitEnemy += AddPoints;
    }

    private void OnEnable()
    {
        if (LevelController.instance != null) 
        {
            PointsForVictory = LevelController.instance.GetCurrentLevelEnemiesCount();
            UpdateHealth(3);
            points = -1;
            AddPoints();
        }
    }

    void UpdateHealth(int _health) 
    {
        if(_health <= 0)
            gameOver(false);

        playerUI.UpdateHealth(_health);
    }

    public void AddPoints() 
    {
        points++;

        UpdatePoints();
    }

    void UpdatePoints()
    {
        if (points >= PointsForVictory)
            gameOver(true);

        playerUI.UpdatePoints(points, PointsForVictory);
    }

    void gameOver(bool ifWin) 
    {
        if(ifWin)
            LevelController.instance.changeLevelStatus(ifWin);

        OnGameOver?.Invoke();

        gameObject.SetActive(true);

        gameOverlay.ShowGameOverPanel();

        resultsPanel.gameOver(ifWin);

        Pause(true);
    }

    public void Pause(bool ifPause)
    {
        Time.timeScale = ifPause ? 0.0f : 1.0f;
    }

    public void LoadNextLevel() 
    {
        LevelController.instance.LoadNextLevel();
        Pause(false);
    }
}
