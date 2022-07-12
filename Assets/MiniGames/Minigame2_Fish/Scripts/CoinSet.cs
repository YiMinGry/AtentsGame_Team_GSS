using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CoinSet : MonoBehaviour
{
    Text coin, coinShadow;

    private void Awake()
    {
        coin = transform.Find("Coin").GetComponent<Text>();
        coinShadow = transform.Find("Coin_Shadow").GetComponent<Text>();
    }


    public void SetCoinText(int _coin)
    {
        coin.text = $"{_coin}";
        coinShadow.text = $"{_coin}";
    }
}
