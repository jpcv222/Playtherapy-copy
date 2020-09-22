using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextGameScreenManager : MonoBehaviour
{
    public Text nextGameText;
    public Text timeText;
    public Button pauseButton;
    public Button resumeButton;
    public Button startNowButton;

    private void Start()
    {
        resumeButton.interactable = false;
    }

    public void OnPauseButtonClicked()
    {
        Time.timeScale = 0;
    }

    public void OnResumeButtonClicked()
    {
        Time.timeScale = 1;
    }

    public void OnStartNowButtonClicked()
    {
        PlaylistManager.pm.StartNextMinigameNow();
    }
}
