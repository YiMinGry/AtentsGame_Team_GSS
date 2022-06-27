using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchivementUI : MonoBehaviour
{
    public GameObject contentsCard = null;
    public GameObject iconBar = null;
    private GameObject[] cards = new GameObject[100];
    private GameObject[] icons = new GameObject[10];

    private void Awake()
    {
        for (int i = 0; i < contentsCard.transform.childCount; i++)
        {
            cards[i] = contentsCard.transform.GetChild(i).gameObject;

        }
        for(int i=0; i<iconBar.transform.childCount;i++)
        {
            icons[i] = iconBar.transform.GetChild(i).gameObject;

        }

        OnClickButtonMain();
    }

    private void OnClickButton(string tagName)
    {
        for (int i = 0; i < contentsCard.transform.childCount; i++)
        {
            if (!(cards[i].CompareTag(tagName)))
                cards[i].SetActive(false);
            else
                cards[i].SetActive(true);
        }
        for (int i = 0; i < iconBar.transform.childCount; i++)
        {
            if ((icons[i].CompareTag(tagName)))
                icons[i].transform.localScale = new Vector3(1.3f, 1.3f, 1);
            else
                icons[i].transform.localScale = new Vector3(1, 1, 1);
        }
    }
    public void OnClickButtonMain()
    {
        OnClickButton("card_main");
    }
    public void OnClickButtonFish()
    {
        OnClickButton("card_fish");
    }
    public void OnClickButtonCookie()
    {
        OnClickButton("card_cookie");
    }
    public void OnClickButtonWar()
    {
        OnClickButton("card_war");
    }
    public void OnClickButtonRhythm()
    {
        OnClickButton("card_rhythm");
    }
    public void OnClickButtonBeer()
    {
        OnClickButton("card_beer");
    }
}
