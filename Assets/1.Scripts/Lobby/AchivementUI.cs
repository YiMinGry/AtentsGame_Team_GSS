using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchivementUI : MonoBehaviour
{
    public GameObject contentsCard = null;
    public GameObject iconBar = null;
    private GameObject[] cards = new GameObject[100];

    private void Awake()
    {
        for (int i = 0; i < contentsCard.transform.childCount; i++)
        {
            cards[i] = contentsCard.transform.GetChild(i).gameObject;

        }

        OnClickButtonMain();
    }
    public void OnClickButtonMain()
    {
        
        for (int i=0;i<contentsCard.transform.childCount;i++)
        {
            if (!(cards[i].CompareTag("card_main")))
                cards[i].SetActive(false);
            else
                cards[i].SetActive(true);
        }
    }
    public void OnClickButtonFish()
    {
        for (int i = 0; i < contentsCard.transform.childCount; i++)
        {
            if (!(cards[i].CompareTag("card_fish")))
                cards[i].SetActive(false);
            else
                cards[i].SetActive(true);
        }
    }
    public void OnClickButtonCookie()
    {
        for (int i = 0; i < contentsCard.transform.childCount; i++)
        {
            if (!(cards[i].CompareTag("card_cookie")))
                cards[i].SetActive(false);
            else
                cards[i].SetActive(true);
           
        }
    }
    public void OnClickButtonWar()
    {
        for (int i = 0; i < contentsCard.transform.childCount; i++)
        {
            if (!(cards[i].CompareTag("card_war")))
                cards[i].SetActive(false);
            else
                cards[i].SetActive(true);
        }
    }
    public void OnClickButtonRhythm()
    {
        for (int i = 0; i < contentsCard.transform.childCount; i++)
        {
            if (!(cards[i].CompareTag("card_rhythm")))
                cards[i].SetActive(false);
            else
                cards[i].SetActive(true);
        }
    }
    public void OnClickButtonBeer()
    {
        for (int i = 0; i < contentsCard.transform.childCount; i++)
        {
            if (!(cards[i].CompareTag("card_beer")))
                cards[i].SetActive(false);
            else
                cards[i].SetActive(true);
        }
    }
}
