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
    private GameObject[] soundOnOff = new GameObject[2]; // 0��°�� On, 1��°�� Off
    [SerializeField]
    private GameObject playerArrow; // �÷��̾� ���� ���ִ� ȭ��ǥ

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
    /// PausePanel ���� ������Ʈ�� Ȱ��ȭ
    /// </summary>
    /// <param name="_tf">true�� ������ Ȱ��ȭ, false�� ������ ��Ȱ��ȭ</param>
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
    /// PausePanel�� Ȱ��ȭ������ �� ��Ȱ��ȭ, ��Ȱ��ȭ������ �� Ȱ��ȭ
    /// </summary>
    /// <returns>PausePanel�� ��Ȱ��ȭ �ϸ� false, Ȱ��ȭ �ϸ� true ����</returns>
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
            rankInfos[i].SetRank(($"{_arr[i]["Rank"]}st"), ($"{_arr[i]["nickName"]}"), ($"{_arr[i]["Score"]}��"));
        }
    }

}
