﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIManager : MonoBehaviour
{
    private TMPro.TextMeshProUGUI coinText;
    private TMPro.TextMeshProUGUI scoreText;
    private TMPro.TextMeshProUGUI countDownText;
    private TMPro.TextMeshProUGUI heartsText;
    private GameObject playButton;
    private GameObject replayButton;
    private GameObject pauseButton;
    private GameObject resumeButton;
    private GameManager gameManager;
    private bool isCountDownActive;
    private float timeLeft = 3;
    private void ShowCoins()
    {
        coinText.text = (gameManager.StateOfTheGame.Coins).ToString();
    }
    private void ShowScore()
    {
        scoreText.text = gameManager.StateOfTheGame.Score.ToString("0000000");
    }
    private void ShowHearts()
    {
        heartsText.text = gameManager.StateOfTheGame.Hearts.ToString();
    }
    private void ShowOrHideButtons()
    {
        switch (gameManager.StateOfTheGame.CurrentState)
        {
            case GameState.State.Start:
                countDownText.gameObject.SetActive(false);
                playButton.SetActive(true);
                replayButton.SetActive(false);
                pauseButton.SetActive(false);
                resumeButton.SetActive(false);
                coinText.gameObject.SetActive(false);
                scoreText.gameObject.SetActive(false);
                break;
            case GameState.State.CountDown:
                countDownText.gameObject.SetActive(true);
                playButton.SetActive(false);
                replayButton.SetActive(false);
                pauseButton.SetActive(false);
                resumeButton.SetActive(false);
                coinText.gameObject.SetActive(false);
                scoreText.gameObject.SetActive(false);
                break;
            case GameState.State.OnPlay:
                countDownText.gameObject.SetActive(false);
                playButton.SetActive(false);
                replayButton.SetActive(false);
                pauseButton.SetActive(true);
                resumeButton.SetActive(false);
                coinText.gameObject.SetActive(true);
                scoreText.gameObject.SetActive(true);
                break;
            case GameState.State.Paused:
                countDownText.gameObject.SetActive(false);
                playButton.SetActive(false);
                replayButton.SetActive(false);
                pauseButton.SetActive(false);
                resumeButton.SetActive(true);
                coinText.gameObject.SetActive(true);
                scoreText.gameObject.SetActive(true);
                break;
            case GameState.State.Resuming:
                countDownText.gameObject.SetActive(false);
                playButton.SetActive(false);
                replayButton.SetActive(false);
                pauseButton.SetActive(true);
                resumeButton.SetActive(false);
                coinText.gameObject.SetActive(true);
                scoreText.gameObject.SetActive(true);
                break;
            case GameState.State.IsDead:
                countDownText.gameObject.SetActive(false);
                playButton.SetActive(false);
                replayButton.SetActive(true);
                pauseButton.SetActive(false);
                resumeButton.SetActive(false);
                coinText.gameObject.SetActive(true);
                scoreText.gameObject.SetActive(true);
                break;
            case GameState.State.Replaying:
                countDownText.gameObject.SetActive(false);
                playButton.SetActive(false);
                replayButton.SetActive(false);
                pauseButton.SetActive(true);
                resumeButton.SetActive(false);
                coinText.gameObject.SetActive(true);
                scoreText.gameObject.SetActive(true);
                break;
            case GameState.State.GameOver:
                countDownText.gameObject.SetActive(false);
                playButton.SetActive(true);
                replayButton.SetActive(false);
                pauseButton.SetActive(false);
                resumeButton.SetActive(false);
                coinText.gameObject.SetActive(true);
                scoreText.gameObject.SetActive(true);
                break;
            case GameState.State.Restarted:
                countDownText.gameObject.SetActive(false);
                playButton.SetActive(false);
                replayButton.SetActive(false);
                pauseButton.SetActive(true);
                resumeButton.SetActive(false);
                coinText.gameObject.SetActive(true);
                scoreText.gameObject.SetActive(true);
                break;
            default:
                throw new Exception(String.Format("Unknown state:", gameManager.StateOfTheGame.CurrentState));
        }
    }
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        playButton = transform.Find("Canvas").Find("Play_Button").gameObject;
        countDownText = transform.Find("Canvas").Find("Count_Down_Text").GetComponent<TMPro.TextMeshProUGUI>();
        coinText = transform.Find("Canvas").Find("Coin_Text").GetComponent<TMPro.TextMeshProUGUI>();
        scoreText = transform.Find("Canvas").Find("Score_Text").GetComponent<TMPro.TextMeshProUGUI>();
        heartsText = transform.Find("Canvas").Find("Hearts_Text").GetComponent<TMPro.TextMeshProUGUI>();
        replayButton = transform.Find("Canvas").Find("Replay_Button").gameObject;
        pauseButton = transform.Find("Canvas").Find("Pause_Button").gameObject;
        resumeButton = transform.Find("Canvas").Find("Resume_Button").gameObject;
    }
    private void Update()
    {
        ShowOrHideButtons();
        if (isCountDownActive)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                gameManager.StartGame();
                isCountDownActive = false;
                timeLeft = 3;
            }
            else if (timeLeft < 1)
            {
                countDownText.text = "1";
            }
            else if (timeLeft < 2)
            {
                countDownText.text = "2";
            }
            else if (timeLeft < 3)
            {
                countDownText.text = "3";
            }
        }
        if (gameManager.StateOfTheGame.IsAlive)
        {
            ShowCoins();
            ShowHearts();
            ShowScore();
        }
    }
    public bool IsCountDownActive { set { isCountDownActive = value; } }
}
