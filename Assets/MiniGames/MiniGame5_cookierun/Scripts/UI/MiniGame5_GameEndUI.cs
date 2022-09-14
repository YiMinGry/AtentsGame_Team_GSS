using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGame5_GameEndUI : MiniGame5_UI
{
    Text scoreTxt;
    GameObject isBest;
    Text coinTxt;
    Button okBtn;

    public override void Init()
    {
        //Debug.Log($"GameEndUI state = {(UIState)Id}");

        scoreTxt = transform.Find("Score").GetComponent<Text>();
        isBest = transform.Find("IsBest").gameObject;
        coinTxt = transform.Find("Coin").GetComponent<Text>();
        okBtn = transform.Find("OKBtn").GetComponent<Button>();

        okBtn.onClick.AddListener(MiniGame5_SceneManager.Inst.OnRanking);
        okBtn.onClick.AddListener(MiniGame5_SoundManager.Inst.PlayButtonClip);

        isBest.SetActive(false);
    }

    public override void StartScene()
    {
        scoreTxt.text = MiniGame5_GameManager.Inst.Score.ToString("N0");
        coinTxt.text = MiniGame5_GameManager.Inst.Coin.ToString();
        // 자신의 최고점수보다 클경우
        if (UserDataManager.instance.MG5PlayData._maxScore < MiniGame5_GameManager.Inst.Score)
        {
            isBest.SetActive(true);
        }

        //보상지급
        UserDataManager.instance.CL2S_UserCoinUpdate(1, MiniGame5_GameManager.Inst.Coin);

        //미리 랭킹에 플레이어 정보 보내놓기
        MiniGame5_GameManager.Inst.SendUserData();
        StartCoroutine(UserDataManager.instance.AchivementCheck(5));
    }
}