using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MiniGame5_GameManager : MonoBehaviour
{
    MiniGame5_SceneManager sceneManager;

    MiniGame5_Player player;
    public MiniGame5_Player Player
    {
        get => player;
        set { player = value; }
    }


    //==============================================================
    //==============================================================

    MiniGame5_DataManager miniFriendData;
    public MiniGame5_DataManager MiniFriendData => miniFriendData;

    MiniFriendData runnerData;
    public MiniFriendData RunnerData
    {
        get => runnerData;
        set
        {
            if (value != runnerData || runnerData == null)
            {
                //Debug.Log("RunnerData Refreshed");
                runnerData = value;
                //데이터가 바뀌었을 시 씬에서 갱신하는 함수
                MiniGame5_SceneManager.Inst.ChangeRunner();

            }
        }
    }

    MiniFriendData nextRunnerData;
    public MiniFriendData NextRunnerData
    {
        get => nextRunnerData;
        set
        {
            if (value != nextRunnerData || nextRunnerData == null)
            {
                nextRunnerData = value;
                //데이터가 바뀌었을 시 씬에서 갱신하는 함수
                MiniGame5_SceneManager.Inst.ChangeNextRunner();
            }
        }
    }

    MiniFriendData petData;
    public MiniFriendData PetData
    {
        get => petData;
        set
        {
            if (value != petData || petData == null)
            {
                petData = value;
                //데이터가 바뀌었을 시 씬에서 갱신하는 함수
                MiniGame5_SceneManager.Inst.ChangePet();
            }
        }
    }

    //==============================================================
    //==============================================================

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
                    OnGameStart += ScoreTimer;
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

    //==============================================================
    //==============================================================

    static MiniGame5_GameManager instance;
    public static MiniGame5_GameManager Inst { get => instance; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
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

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        Initialize();
    }

    private void Start()
    {
        GameSet();
    }

    void Initialize()
    {
        miniFriendData = GetComponent<MiniGame5_DataManager>();

        sceneManager = FindObjectOfType<MiniGame5_SceneManager>();
        player = GameObject.Find("LoadScene").transform.Find("Main").Find("PlayerPos").Find("Player").GetComponent<MiniGame5_Player>();

        sceneManager.Inintialize();
    }
    
    //==============================================================
    //==============================================================

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

        for (int i = 0; i < miniFriendData.runnerLength; i++)
        {
            if (miniFriendData.runnerData[i].isHave)
            {
                RunnerData = miniFriendData.runnerData[i];
                break;
            }
        }
        for (int i = 0; i < miniFriendData.runnerLength; i++)
        {
            if (miniFriendData.runnerData[i].isHave && miniFriendData.runnerData[i] != runnerData)
            {
                NextRunnerData = miniFriendData.runnerData[i];
                break;
            }
        }
        for (int i = 0; i < miniFriendData.petLength; i++)
        {
            if (miniFriendData.petData[i].isHave)
            {
                PetData = miniFriendData.petData[i];
                break;
            }
        }
    }

    void LifeTimer()
    {
        Life -= Time.deltaTime * 0.05f * (float)stageLevel;
    }

    void ScoreTimer()
    {
        Score += 1 * stageLevel;
    }
}
