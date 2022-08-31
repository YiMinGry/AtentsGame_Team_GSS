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


    public void SetScoreText(int _score)
    {
        score.text = $"SCORE : {_score}";
        scoreShadow.text = $"SCORE : {_score}";
    }
}
