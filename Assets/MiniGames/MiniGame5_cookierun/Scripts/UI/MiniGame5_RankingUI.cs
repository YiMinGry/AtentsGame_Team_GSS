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

        okBtn.onClick.AddListener(MiniGame5_SoundManager.Inst.PlayButtonClip);

        okBtn.onClick.AddListener(MiniGame5_SceneManager.Inst.OnReset);
        okBtn.onClick.AddListener(MiniGame5_SceneManager.Inst.OnOpening);
    }

    public void SetRank(int index, string _name, string _score)
    {
        names[index].text = _name;
        scores[index].text = _score.ToString();
    }
}