using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG2_GameManager : MonoBehaviour
{
    [SerializeField]
    MG2_UIManager _mg2_UIManager;

    [SerializeField]
    public GameObject player;
    [SerializeField]
    public GameObject enemySpawner;

    static MG2_GameManager instance;

    int coin = 5;
    int score = 0;
    int stage = 1;
    int healthPoint = 4;
    int[] stageScoreSet;

    public System.Action playerHPChange;
    public System.Action playerLevelChange;

    public static MG2_GameManager Inst
    {
        get => instance;
    }

    public MG2_UIManager mg2_UIManager
    {
        get
        {
            return _mg2_UIManager;
        }
    }

    private void Awake()
    {
        mg2_UIManager.mg2_GameManager = this;

        if (instance == null)
        {
            instance = this;
            instance.Initialize();
        }
        else
        {
            if (instance != this)
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void Start()
    {
        StartCoroutine(OnGameStart());    
    }

    IEnumerator OnGameStart()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GoToLobby();
                break;
            }
            if (Input.anyKeyDown)
            {
                StartGame();
                break;
            }
            yield return null;
        }
        yield return null;
    }

    public int Score
    {
        get => score;
        set
        {
            score = value;
            NextStage(score);
            mg2_UIManager.ScoreUpdate(score);
        }
    }

    public int Coin
    {
        get => coin;
        set
        {
            coin = value;
            mg2_UIManager.CoinUpdate(coin);
        }
    }

    void NextStage(int score)
    {
        if(Stage < 5 && score >= stageScoreSet[Stage-1])
        {
            Stage++;
        }
    }

    public int Stage
    {
        get => stage;
        set
        {
            playerLevelChange.Invoke();
            stage = value;
            stage = Mathf.Clamp(stage, 1, 6);
            Debug.Log($"Stage : {stage}");
        }
    }

    public int HealthPoint
    {
        get => healthPoint;
        set
        {
            healthPoint = value;
            healthPoint = Mathf.Clamp(healthPoint, 0, 9);
            playerHPChange.Invoke();
            if(healthPoint == 0)
            {
                GameOver();
            }
            Debug.Log($"HealthPoint : {healthPoint}");
        }
    }

    private void GameOver()
    {
        player.SetActive(false);
        enemySpawner.SetActive(false);
        mg2_UIManager.SetContinuePanel(true);
        StartCoroutine(GameOverCount());
        StartCoroutine(GameContinued());
    }

    private int count = 30;

    IEnumerator GameOverCount()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            mg2_UIManager.SetCountText(count--);
        }
    }

    bool isYes = true;

    IEnumerator GameContinued()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                if (isYes)
                {
                    StartGame();
                }
                else
                {
                    mg2_UIManager.SetRankingPanel(true);
                    //StartCoroutine(AfterGameOver()); 추가예정
                }
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                isYes = true;
                mg2_UIManager.SetYesNo(true);
            }
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                isYes = false;
                mg2_UIManager.SetYesNo(false);
            }
            yield return null;
        }
    }

    public void Initialize()
    {
        score = 0;
        stage = 1;
        healthPoint = 4;
        stageScoreSet = new int[] {1000, 2000, 3000, 4000};
    }

    public void StartGame()
    {
        HealthPoint = 4;
        player.SetActive(true);
        enemySpawner.SetActive(true);
        mg2_UIManager.SetStartPanel(false);
        mg2_UIManager.SetContinuePanel(false);
        StopCoroutine(OnGameStart());
        StopCoroutine(GameContinued());
        StopCoroutine(GameOverCount());
    }

    public void GoToLobby()
    {
        bl_SceneLoaderManager.LoadScene("Main_Lobby");
    }
}
