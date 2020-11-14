using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIManager : MonoBehaviour
{
    private TMPro.TextMeshProUGUI coinText;
    // Start is called before the first frame update
    void Start()
    {
        coinText = gameObject.transform.Find("Coin_Text").GetComponent<TMPro.TextMeshProUGUI>();
        coinText.gameObject.SetActive(true);
        coinText.text = 0.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShowCoins()
    {
        coinText.text = (int.Parse(coinText.text) + 1).ToString();
        Debug.Log(int.Parse(coinText.text) + 1);
    }
}
