using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSet : MonoBehaviour
{
    // Start is called before the first frame update
    Text score, scoreShadow;

    private void Awake()
    {
        score = transform.Find("Score").GetComponent<Text>();
        scoreShadow = transform.Find("Score_Shadow").GetComponent<Text>();
    }


    private void Update()
    {
        score.text = $"SCORE : {MG2_GameManager.Inst.Score}";
        scoreShadow.text = $"SCORE : {MG2_GameManager.Inst.Score}";
    }
}
