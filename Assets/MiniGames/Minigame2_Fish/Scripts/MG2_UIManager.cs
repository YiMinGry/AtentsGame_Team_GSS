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
    private GameObject pausePanel;
    [SerializeField]
    private GameObject dashPanel;
    [SerializeField]
    private GameObject levelUpUI;

    [SerializeField]
    private GameObject[] soundOnOff = new GameObject[2]; // 0번째가 On, 1번째가 Off
    [SerializeField]
    private GameObject playerArrow; // 플레이어 위에 떠있는 화살표

    [SerializeField]
    private ScoreSet scoreSet;
    [SerializeField]
    private CoinSet[] coinSet;

    [SerializeField]
    private Text count;
    [SerializeField]
    private Text yes;
    [SerializeField]
    private Text no;

    [SerializeField]
    List<MG2_RankInfo> rankInfos = new List<MG2_RankInfo>();

    public void LocatePlayerArrow(Vector3 pos)
    {
        playerArrow.transform.position = pos;
    }

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
    /// <summary>
    /// PausePanel 게임 오브젝트를 활성화
    /// </summary>
    /// <param name="_tf">true를 받으면 활성화, false를 받으면 비활성화</param>
    public void SetPausePanel(bool _tf)
    {
        pausePanel.SetActive(_tf);
    }
    public void SetDashPanel(bool _tf)
    {
        dashPanel.SetActive(_tf);
    }
    public void SetLevelUpUI(bool _tf)
    {
        levelUpUI.SetActive(_tf);
    }


    /// <summary>
    /// PausePanel이 활성화상태일 때 비활성화, 비활성화상태일 때 활성화
    /// </summary>
    /// <returns>PausePanel을 비활성화 하면 false, 활성화 하면 true 리턴</returns>
    public bool SetPausePanel()
    {
        bool result;
        if (pausePanel.activeSelf)
        {
            pausePanel.SetActive(false);
            result = false;
        }
        else
        {
            pausePanel.SetActive(true);
            result = true;
        }
        return result;
    }

    public void SetSoundOnOffImage(bool _tf)
    {
        if (_tf)
        {
            soundOnOff[0].SetActive(true);
            soundOnOff[1].SetActive(false);
        }
        else
        {
            soundOnOff[0].SetActive(false);
            soundOnOff[1].SetActive(true);
        }
    }

    public void ScoreUpdate(int _score)
    {
        scoreSet.SetScoreText(_score);
    }

    public void CoinUpdate(int _coin)
    {
        foreach (var coin in coinSet)
        {
            coin.SetCoinText(_coin);
        }
    }

    // Continue Panel -----------------------------------------------------------

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
            rankInfos[i].SetRank(($"{_arr[i]["Rank"]}st"), ($"{_arr[i]["nickName"]}"), ($"{_arr[i]["Score"]}점"));
        }
    }

}
