using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    // --------------------------------------
}
