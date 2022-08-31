using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MG2_RankInfo : MonoBehaviour
{    
    [SerializeField]
    Text[] infos;

    private void Awake()
    {
        if (infos == null)
        {
            infos = new Text[3];
            infos = GetComponentsInChildren<Text>();
        }
    }

    public void SetRank(string _rate, string _nick, string _score)
    {
        infos[0].text = _rate;
        infos[1].text = _nick;
        infos[2].text = _score;
    }
}
