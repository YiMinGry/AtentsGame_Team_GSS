using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MiniGame5_GameManager : MonoBehaviour
{
    MiniGame5_SceneManager sceneManager;

    MiniGame5_Player player;
    public MiniGame5_Player Player => player;

    bool isGameStart;
    public bool IsGameStart
    {
        get => isGameStart;
        set
        {
            if (isGameStart != value)
            {
                isGameStart = value;
                if (isGameStart)
                {
                    OnGameStart += LifeTimer;
                }
                else
                {
                    OnGameStart = null;
                }
            }
        }
    }

    public System.Action OnGameStart;

    int stageLevel = 1;
    public int StageLevel { get => stageLevel; set => stageLevel = value; }

    int score = 0;
    public int Score
    {
        get => score;
        set
        {
            score = value;
            OnScoreChange?.Invoke();
        }
    }

    int coin = 0;
    public int Coin
    {
        get => coin;
        set
        {
            coin = value;
            OnCoinChange?.Invoke();
        }
    }

    int credit = 0;
    public int Credit
    {
        get => credit;
        set
        {
            credit = value;
            OnCreditChange?.Invoke();
        }
    }

    float life = 1f;
    public float Life
    {
        get => life;
        set
        {
            if (value > 0)
            {
                life = value;
                Mathf.Clamp(life, 0f, 1f);
                OnLifeChange?.Invoke();
            }
            else
            {
                sceneManager.OnGameEnd();
            }
        }
    }

    public System.Action OnScoreChange;
    public System.Action OnCoinChange;
    public System.Action OnCreditChange;
    public System.Action OnLifeChange;

    bool[] bonusTime = new bool[9];
    public bool[] BonusTime
    {
        get => bonusTime;
        set
        {
            bonusTime = value;
            OnBonusTimeChange?.Invoke();
        }
    }

    public System.Action OnBonusTimeChange;

    static MiniGame5_GameManager instance;
    public static MiniGame5_GameManager Inst { get => instance; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            instance.Inintialize();
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            //씬에 게임매니저가 여러번 생성됐을 경우
            if (instance != this)
            {
                Destroy(this.gameObject);
            }
        }
    }

    void Inintialize()
    {
        sceneManager = FindObjectOfType<MiniGame5_SceneManager>();
        player = GameObject.Find("LoadScene").transform.Find("Main").Find("Player").GetComponent<MiniGame5_Player>();

        sceneManager.Inintialize();
        GameSet();
    }

    public void GameSet()
    {
        IsGameStart = false;
        stageLevel = 1;
        score = 0;
        life = 1f;
        coin = 0;
        for (int i = 0; i < bonusTime.Length; i++)
        {
            bonusTime[i] = false;
        }

        Debug.Log("GameManager Init");
    }

    void LifeTimer()
    {
        Life -= Time.deltaTime * 0.05f * (float)stageLevel;
    }
}
