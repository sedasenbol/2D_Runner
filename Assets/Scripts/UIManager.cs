using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIManager : MonoBehaviour
{
    private TMPro.TextMeshProUGUI coinText;
    private GameObject playButton;
    private GameObject replayButton;
    private GameObject pauseButton;
    private GameObject resumeButton;
    private GameManager gameManager;
    // Start is called before the first frame update
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        coinText = transform.Find("Canvas").Find("Coin_Text").GetComponent<TMPro.TextMeshProUGUI>();
        replayButton = transform.Find("Canvas").Find("Play_Button").gameObject;
        replayButton = transform.Find("Canvas").Find("Replay_Button").gameObject;
        pauseButton = transform.Find("Canvas").Find("Pause_Button").gameObject;
        resumeButton = transform.Find("Canvas").Find("Resume_Button").gameObject;
    }

    // Update is called once per frame
    private void Update()
    {
        ShowCoins();
        ShowOrHideButtons();
    }
    private void ShowCoins()
    {
        coinText.text = (gameManager.StateOfTheGame.Coins).ToString("00000");
    }
    private void ShowOrHideButtons()
    {
        switch (gameManager.StateOfTheGame.state)
        {
            case GameState.State.OnPlay:
                playButton.SetActive(false);
                replayButton.SetActive(false);
                pauseButton.SetActive(true);
                resumeButton.SetActive(false);
                coinText.gameObject.SetActive(true);
                break;
            case GameState.State.Paused:
                playButton.SetActive(false);
                replayButton.SetActive(false);
                pauseButton.SetActive(true);
                resumeButton.SetActive(false);
                break;
            case GameState.State.Resuming:
                playButton.SetActive(false);
                replayButton.SetActive(false);
                pauseButton.SetActive(false);
                resumeButton.SetActive(true);
                break;
            case GameState.State.IsDead:
                playButton.SetActive(false);
                replayButton.SetActive(true);
                pauseButton.SetActive(false);
                resumeButton.SetActive(false);
                break;
            case GameState.State.Replaying:
                playButton.SetActive(false);
                replayButton.SetActive(false);
                pauseButton.SetActive(true);
                resumeButton.SetActive(false);
                break;
            case GameState.State.GameOver:
                playButton.SetActive(true);
                replayButton.SetActive(false);
                pauseButton.SetActive(false);
                resumeButton.SetActive(false);
                break;
            case GameState.State.Restarted:
                playButton.SetActive(false);
                replayButton.SetActive(false);
                pauseButton.SetActive(true);
                resumeButton.SetActive(false);
                break;
        }
    }
}
