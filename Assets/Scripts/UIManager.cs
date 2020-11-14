using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIManager : MonoBehaviour
{
    private TMPro.TextMeshProUGUI coinText;
    private GameObject replayButton;
    private GameObject pauseButton;
    private GameObject resumeButton;
    private bool isAlive = true;
    // Start is called before the first frame update
    private void Start()
    {
        coinText = transform.Find("Canvas").Find("Coin_Text").GetComponent<TMPro.TextMeshProUGUI>();
        coinText.gameObject.SetActive(true);
        coinText.text = 0.ToString();

        replayButton = transform.Find("Canvas").Find("Replay_Button").gameObject;
        pauseButton = transform.Find("Canvas").Find("Pause_Button").gameObject;
        resumeButton = transform.Find("Canvas").Find("Resume_Button").gameObject;

        replayButton.SetActive(false);
        pauseButton.SetActive(true);
        resumeButton.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
    public void ShowCoins()
    {
        coinText.text = (int.Parse(coinText.text) + 1).ToString("00000");
    }
    public void IsPlayerDead()
    {
        replayButton.SetActive(true);
        pauseButton.SetActive(false);
        resumeButton.SetActive(false);
    }
    public void PauseSwitch()
    {
        isAlive = !isAlive;
        pauseButton.SetActive(isAlive);
        resumeButton.SetActive(!isAlive);
    }
}
