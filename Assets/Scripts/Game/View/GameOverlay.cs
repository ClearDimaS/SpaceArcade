using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverlay : MonoBehaviour
{
    [SerializeField]
    GameObject PausePanel;

    [SerializeField]
    GameObject GameOverPanel;

    [SerializeField]
    GameController gameController;

    private void OnDisable()
    {
        HideUserPanels();
    }

    public void PausePressed()
    {
        if (GameOverPanel.activeSelf == false) 
        {
            PausePanel.SetActive(true);
            gameController.Pause(true);
        }
    }

    private void HideUserPanels()
    {
        PausePanel.SetActive(false);
        GameOverPanel.SetActive(false);
    }

    public void ExitGame() 
    {
        gameController.OnGameOver();
        gameController.Pause(false);
        UI_Controller.Instance.SetActivePanel(0);
    }

    public void ContinueGame() 
    {
        gameController.Pause(false);
        HideUserPanels();
    }

    public void ShowGameOverPanel() 
    {
        GameOverPanel.SetActive(true);
    }
}
