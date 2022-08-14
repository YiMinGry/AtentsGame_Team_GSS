using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGame5_RankingUI : MiniGame5_UI
{
    Text[] names;
    Text[] scores;
    Button okBtn;

    public override void Init()
    {
        //Debug.Log($"RankingUI state = {(UIState)Id}");

        names = transform.Find("Name").GetComponentsInChildren<Text>();
        scores = transform.Find("Score").GetComponentsInChildren<Text>();
        okBtn = transform.Find("OkBtn").GetComponent<Button>();

        okBtn.onClick.AddListener(MiniGame5_SceneManager.Inst.OnReset);
        okBtn.onClick.AddListener(MiniGame5_SceneManager.Inst.OnOpening);
    }

    public override void StartScene()
    {
        for (int i = 0; i < names.Length; i++)
        {
            //서버에서 받아서 갱신
        }

        for (int i = 0; i < scores.Length; i++)
        {
            //서버에서 받아서 갱신
        }
    }
}