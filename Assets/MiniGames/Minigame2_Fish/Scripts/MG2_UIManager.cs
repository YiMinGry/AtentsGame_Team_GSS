using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class MG2_UIManager : MonoBehaviour
{
    public MG2_GameManager mg2_GameManager;

    [SerializeField]
    private GameObject startPanel;
    [SerializeField]
    private GameObject continuePanel;
    [SerializeField]
    private GameObject rankingPanel;

    [SerializeField]
    private ScoreSet scoreSet;
    [SerializeField]
    private CoinSet coinSet;

    [SerializeField]
    private Text count;
    [SerializeField]
    private Text yes;
    [SerializeField]
    private Text no;

    [SerializeField]
    List<MG2_RankInfo> rankInfos = new List<MG2_RankInfo>();

    public void SetStartPanel(bool _tf)
    {
        startPanel.SetActive(_tf);
    }

    public void SetContinuePanel(bool _tf)
    {
        continuePanel.SetActive(_tf);
    }
    public void SetRankingPanel(bool _tf)
    {
        rankingPanel.SetActive(_tf);
    }

    public void ScoreUpdate(int _score)
    {
        scoreSet.SetScoreText(_score);
    }

    public void CoinUpdate(int _coin)
    {
        coinSet.SetCoinText(_coin);
    }

    // Continue Panel -----------------------

    public void SetCountText(int _count)
    {
        count.text = $"{_count}";
    }

    public void SetYesNo(bool _tf)
    {
        if (_tf)
        {
            yes.text = ">YES";
            no.text = " NO";
        }
        else
        {
            yes.text = " YES";
            no.text = ">NO";
        }
    }

    public void SetTop10Rank(JArray _arr)
    {
        for (int i = 0; i < _arr.Count; i++)
        {
            rankInfos[i].SetRank(($"{_arr[i]["Rank"]}st"), ($"{_arr[i]["nickName"]}"), ($"{_arr[i]["Score"]}Á¡"));
        }
    }

}
