using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGame5_OpeningUI : MiniGame5_UI
{
    Button startBtn;
    Text startInfoTxt;
    Text creditTxt;

    public override void Init()
    {
        //Debug.Log($"OpeningUI state = {(UIState)Id}");

        startBtn = transform.Find("StartBtn").GetComponent<Button>();
        startInfoTxt = transform.Find("StartCountText").GetComponent<Text>();
        creditTxt = transform.Find("Credit").GetComponent<Text>();

        startBtn.onClick.AddListener(MiniGame5_SoundManager.Inst.PlayStartButtonClip);
        startBtn.onClick.AddListener(CheckCoin);

        MiniGame5_GameManager.Inst.OnCreditChange += () => { creditTxt.text = MiniGame5_GameManager.Inst.Credit.ToString(); };
    }

    public override void StartScene()
    {
        MiniGame5_SoundManager.Inst.loadingBGM();
    }

    void CheckCoin()
    {
        if (UserDataManager.instance.coin1 <= 0)
        {
            startInfoTxt.text = "보유한 코인이 없습니다.";
            startBtn.gameObject.SetActive(false);
            startInfoTxt.gameObject.SetActive(true);
        }
        else
        {
            UserDataManager.instance.coin1--;
            MiniGame5_GameManager.Inst.Credit++;

            startBtn.gameObject.SetActive(false);
            startInfoTxt.gameObject.SetActive(true);
            StartCoroutine(CoStartCount());
        }

        //테스트
        //MiniGame5_GameManager.Inst.Credit++;

        //startBtn.gameObject.SetActive(false);
        //startInfoTxt.gameObject.SetActive(true);
        //StartCoroutine(CoStartCount());
    }

    IEnumerator CoStartCount()
    {
        int count = 5;
        while (count > 0)
        {
            startInfoTxt.text = $"START... ({count})";
            yield return new WaitForSeconds(1f);
            count--;
        }

        if (count <= 0)
        {
            MiniGame5_SceneManager.Inst.OnStart();
        }
    }
}