using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGame5_MainPlayingUI : MiniGame5_UI
{
    public bool isPractice = false;

    Text coinTxt;
    Text scoreTxt;
    Slider lifeBar;
    Animator lifeBarHandleAnim;
    Transform[] bonusTime;

    Coroutine coBonusTime;

    public override void Init()
    {
        //Debug.Log($"MainPlayingUI state = {(UIState)Id}");

        coinTxt = transform.Find("Coin").GetComponent<Text>();
        scoreTxt = transform.Find("Score").GetComponent<Text>();
        lifeBar = GetComponentInChildren<Slider>();
        lifeBarHandleAnim = GetComponentInChildren<Animator>();
        bonusTime = new Transform[transform.Find("BnsTime_get").childCount];
        for (int i = 0; i < bonusTime.Length; i++)
        {
            bonusTime[i] = transform.Find("BnsTime_get").GetChild(i).GetComponent<Transform>();
        }

        MiniGame5_GameManager.Inst.OnCoinChange += RefreshCoinUI;
        MiniGame5_GameManager.Inst.OnScoreChange += RefreshScoreUI;
        MiniGame5_GameManager.Inst.OnLifeChange += RefreshLifeBarUI;
        MiniGame5_GameManager.Inst.OnBonusTimeChange += RefreshBonusTime;
        MiniGame5_GameManager.Inst.StartBonusTime += CountBonusTime;
        MiniGame5_GameManager.Inst.EndBonusTime += () => StopCoroutine(coBonusTime);

        ReSet();
    }

    public override void StartScene()
    {
        MiniGame5_GameManager.Inst.IsGameGoing = true;
        lifeBarHandleAnim.StopPlayback();
    }

    public override void EndScene()
    {
        lifeBarHandleAnim.StartPlayback();
    }

    public override void ReSet()
    {
        RefreshCoinUI();
        RefreshScoreUI();
        RefreshLifeBarUI();
        RefreshBonusTime();
    }

    private void Update()
    {
        MiniGame5_GameManager.Inst.OnGameStart?.Invoke();
    }

    void RefreshCoinUI()
    {
        coinTxt.text = MiniGame5_GameManager.Inst.Coin.ToString();
    }
    void RefreshScoreUI()
    {
        scoreTxt.text = MiniGame5_GameManager.Inst.Score.ToString("N0");
    }
    void RefreshLifeBarUI()
    {
        lifeBar.value = MiniGame5_GameManager.Inst.Life / MiniGame5_GameManager.Inst.maxLife;
    }

    void RefreshBonusTime()
    {
        for (int i = 0; i < bonusTime.Length; i++)
        {
            bonusTime[i].gameObject.SetActive(MiniGame5_GameManager.Inst.BonusTime[i]);
        }
    }

    void CountBonusTime()
    {
        coBonusTime = StartCoroutine(CoCountBonusTime());
    }

    IEnumerator CoCountBonusTime()
    {
        for (int i = MiniGame5_GameManager.Inst.BonusTime.Length - 1; i >= 0 ; i--)
        {
            yield return new WaitForSeconds(1f);
            bonusTime[i].gameObject.SetActive(false);
            if (i == 0) MiniGame5_GameManager.Inst.IsBonusTime = false;
        }
    }
}