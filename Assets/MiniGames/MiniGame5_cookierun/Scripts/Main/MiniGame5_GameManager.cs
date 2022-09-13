using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class MiniGame5_GameManager : MonoBehaviour
{
    MiniGame5_SceneManager sceneManager;
    MiniGame5_SoundManager soundManager;

    MiniGame5_Player player;
    public MiniGame5_Player Player
    {
        get => player;
        set => player = value; 
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

    bool isGameGoing;
    public bool IsGameGoing
    {
        get => isGameGoing;
        set
        {
            if (isGameGoing != value)
            {
                isGameGoing = value;
                if (isGameGoing)
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

    bool isFirstRunner = true;
    public bool IsFirstRunner { get => isFirstRunner; set => isFirstRunner = value; }

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
            life = value;
            Mathf.Clamp(life, 0f, 1f);
            OnLifeChange?.Invoke();
            if (value <= 0 && !isBonusTime)
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
    public bool[] BonusTime => bonusTime;
    public int BonusTimeIndex
    {
        set
        {
            bonusTime[value] = true;
            OnBonusTimeChange?.Invoke();
            //Debug.Log($"bonusTime length {bonusTime.Length}");

            bool isAllCollected = true;
            for (int i = 0; i < bonusTime.Length; i++)
            {
                //Debug.Log($"bonusTime {i} = {bonusTime[i]}");
                if (bonusTime[i] == false)
                {
                    isAllCollected = false;
                    break;
                }
            }

            if (isAllCollected == true)
            {
                IsBonusTime = true;
            }
        }
    }

    bool isBonusTime = false;
    public bool IsBonusTime
    {
        get => isBonusTime;
        set
        {
            isBonusTime = value;
            if (isBonusTime)
            {
                StartBonusTime?.Invoke();
            }
            else
            {
                EndBonusTime?.Invoke();
            }
        }
    }

    public System.Action OnBonusTimeChange;
    public System.Action StartBonusTime;
    public System.Action EndBonusTime;

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
        soundManager = FindObjectOfType<MiniGame5_SoundManager>();
        player = GameObject.Find("LoadScene").transform.Find("Main").Find("PlayerPos").Find("Player").GetComponent<MiniGame5_Player>();

        for (int i = 0; i < bonusTime.Length; i++)
        {
            bonusTime[i] = false;
        }

        //soundManager.Initialize();
        sceneManager.Initialize();

        NetEventManager.Regist("UpdateRanking", S2CL_UpdateRanking);
    }
    
    //==============================================================
    //==============================================================

    public void GameSet()
    {
        IsGameGoing = false;
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
        Life -= Time.deltaTime * 0.02f * (float)stageLevel;
    }

    void ScoreTimer()
    {
        Score += 1 * stageLevel;
    }

    public void SendUserData()
    {
        JObject _userData = new JObject();
        _userData.Add("cmd", "UpdateRanking");
        _userData.Add("ID", UserDataManager.instance.ID);
        _userData.Add("nickName", UserDataManager.instance.nickName);
        _userData.Add("MG_NAME", "MG_5");
        _userData.Add("Score", Score);

        NetManager.instance.CL2S_SEND(_userData);
    }

    public void S2CL_UpdateRanking(JObject _jdata)
    {
        JArray _arr = JArray.Parse(_jdata["allRankArr"].ToString());
        MiniGame5_RankingUI rankingUI = GameObject.Find("Canvas").transform.Find("RankingUI").GetComponent<MiniGame5_RankingUI>();
        for (int i = 0; i < 5; i++)
        {
            rankingUI.SetRank(i, $"{_arr[i]["nickName"]}", $"{_arr[i]["Score"]}");
        }
    }
}
