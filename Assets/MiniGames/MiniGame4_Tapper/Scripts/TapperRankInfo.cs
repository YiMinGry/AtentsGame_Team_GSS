using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TapperRankInfo : MonoBehaviour
{
    [SerializeField]
    Text[] infos;
    public void SetRank(string _rate, string _nick, string _score) 
    {
        infos[0].text = _rate;
        infos[1].text = _nick;
        infos[2].text = _score;
    }

}
