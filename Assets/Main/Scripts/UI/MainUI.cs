using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    public Text coin1;
    public Text coin2;

    private void Awake()
    {
        if (coin1 == null) coin1 = transform.Find("CoinIcon").Find("Coin1").GetComponent<Text>();
        if (coin2 == null) coin1 = transform.Find("CoinIcon").Find("Coin2").GetComponent<Text>();
    }

    private void Start()
    {
        coin1.text = UserDataManager.instance.coin1.ToString("N0");
        coin2.text = UserDataManager.instance.coin2.ToString("N0");
    }
}
