using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class MG2_GameManager : MonoBehaviour
{
    [SerializeField]
    MG2_UIManager _mg2_UIManager;

    [SerializeField]
    MG2_EffectManager _mg2_EffectManager;

    [SerializeField]
    private Fish_Player _player;

    [SerializeField]
    public GameObject enemySpawner;

    static MG2_GameManager instance;

    // ���� ----------------------------------------------------------------------------------------

    int coin = 5;
    int score = 0;
    int stage = 1; 
    int count = 30;
    int healthPoint = 4;
    int[] stageScoreSet = new int[] { 1000, 2000, 3000, 4000 };

    bool isYes = true; // GameOver ȭ�鿡�� Continue �� ���� ���� ����

    Coroutine gameOverCount, gameContinued, onGameStart;

    // ��������Ʈ ----------------------------------------------------------------------------------------

    public System.Action playerHPChange;
    public System.Action playerLevelChange;

    // ������Ƽ ----------------------------------------------------------------------------------------
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
            coin = Mathf.Clamp(value, 0, 99);
            mg2_UIManager.CoinUpdate(coin);
        }
    }
    public int Stage
    {
        get => stage;
        set
        {
            stage = value;
            stage = Mathf.Clamp(stage, 1, 6);
            playerLevelChange.Invoke();
        }
    }
    public int HealthPoint
    {
        get => healthPoint;
        set
        {
            healthPoint = Mathf.Clamp(value, 0, 9);
            playerHPChange.Invoke();
            if (healthPoint == 0)
            {
                GameOver();
            }
        }
    }
    public static MG2_GameManager Inst
    {
        get => instance;
    }
    public Fish_Player Player
    {
        get => _player;
    }
    public MG2_UIManager mg2_UIManager
    {
        get
        {
            return _mg2_UIManager;
        }
    }
    public MG2_EffectManager mg2_EffectManager
    {
        get
        {
            return _mg2_EffectManager;
        }
    }

    // private �Լ� --------------------------------------------------------------------------------------------------------------

    private void Awake()
    {
        mg2_UIManager.mg2_GameManager = this;
        mg2_EffectManager.mg2_GameManager = this;
        NetEventManager.Regist("UpdateRanking", S2CL_UpdateRanking);

        if (instance == null)
        {
            instance = this;
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
        onGameStart = StartCoroutine(OnGameStart());
        AudioManager.Inst.PlayBGM("FishBGM");
    }       

    void NextStage(int score)
    {
        if(Stage < 5 && score >= stageScoreSet[Stage-1])
        {
            Stage++;
            HealthPoint++;
        }
    }

    private void Initialize()
    {
        Score = 0;
        Stage = 1;
        HealthPoint = 4;
        count = 30;
    }    

    private void StartGame() // ó�� ������ ��, Continue �� �� ����
    {
        if (gameOverCount != null)
            StopCoroutine(gameOverCount);
        if (onGameStart != null)
            StopCoroutine(onGameStart);
        if (gameContinued != null)
            StopCoroutine(gameContinued);

        HealthPoint = 4; // Continue �� �� HP Ǯ�� ����
        Player.gameObject.SetActive(true);
        Player.transform.position = new Vector3(0, 0, 0); // �÷��̾� ��ġ �ʱ�ȭ

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("MG2_Fish_Enemy"); // ���� ���� �����ϴ� ��� enemy fish ����
        foreach(var enemy in enemies)
        {
            Destroy(enemy);
        }

        PauseGame(false);
        enemySpawner.SetActive(true);
        mg2_UIManager.SetStartPanel(false);
        mg2_UIManager.SetContinuePanel(false);
    }    

    private void GameOver()
    {
        Player.gameObject.SetActive(false);
        enemySpawner.SetActive(false);
        gameOverCount = StartCoroutine(GameOverCount());
        gameContinued = StartCoroutine(GameContinued());
    }
    /// <summary>
    /// true�� �ð� ����, false�� �ð� �������
    /// </summary>
    /// <param name="_tf"></param>
    private void TimeStop(bool _tf)
    {
        if (_tf)
        {
            Time.timeScale = 0.0f;
        }
        else
        {
            Time.timeScale = 1.0f;
        }
    }

    // public �Լ� ---------------------------------------------------------------------------------------

    /// <summary>
    /// ���� �Ͻ����� �Ǵ� �Ͻ����� ���� ��� �Լ�
    /// </summary>
    public void PauseGame()
    {
        if (mg2_UIManager.SetPausePanel()) // true�� �Ͻ� ���� �г��� Ȱ��ȭ �� ����
        {
            TimeStop(true);
        }
        else
        {
            TimeStop(false);
        }
    }

    /// <summary>
    /// ���� �Ͻ����� �Լ�
    /// </summary>
    /// <param name="_tf">true�� �Ͻ�����, false�� �Ͻ����� ����</param>
    public void PauseGame(bool _tf)
    {
        if (_tf)
        {
            mg2_UIManager.SetPausePanel(true);
            TimeStop(true);
        }
        else
        {
            mg2_UIManager.SetPausePanel(false);
            TimeStop(false);
        }
    }

    public void RestartGame()
    {
        StartGame();
        Initialize();
    }
    public void SoundOnOff()
    {
        if (AudioManager.Inst.IsMusicOn)
        {
            AudioManager.Inst.IsMusicOn = false;
            AudioManager.Inst.IsSoundOn = false;
            _mg2_UIManager.SetSoundOnOffImage(false);
        }
        else
        {
            AudioManager.Inst.IsMusicOn = true;
            AudioManager.Inst.IsSoundOn = true;
            _mg2_UIManager.SetSoundOnOffImage(true);
        }
    }

    // �ڷ�ƾ --------------------------------------------------------------------------------------------

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
    IEnumerator GameOverCount()
    {
        yield return new WaitForSeconds(1.0f);
        mg2_UIManager.SetContinuePanel(true);
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            mg2_UIManager.SetCountText(count--);
        }
    }    

    IEnumerator GameContinued()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                if (isYes) // Continue���� Yes �����ϸ� ���� 1�� ���� �� ���� ����
                {
                    Coin--;
                    StartGame();
                }
                else
                {
                    CL2S_UpdateRanking(Score);
                    mg2_UIManager.SetRankingPanel(true);
                    StartCoroutine(AfterGameOver());
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

    IEnumerator AfterGameOver()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GoToLobby();
                break;
            }
            yield return null;
        }
        yield return null;
    }

    // --------------------------------------------------------------------------------------------

    public void CL2S_UpdateRanking(int _score)
    {
        JObject _userData = new JObject();
        _userData.Add("cmd", "UpdateRanking");
        _userData.Add("ID", UserDataManager.instance.ID);
        _userData.Add("nickName", UserDataManager.instance.nickName);
        _userData.Add("MG_NAME", "MG_2");
        _userData.Add("Score", _score);

        NetManager.instance.CL2S_SEND(_userData);
    }

    public void S2CL_UpdateRanking(JObject _jdata)
    {
        JArray _arr = JArray.Parse(_jdata["allRankArr"].ToString());

        mg2_UIManager.SetTop10Rank(_arr);
    }    

    public void GoToLobby()
    {
        TimeStop(false);
        StopAllCoroutines();
        bl_SceneLoaderManager.LoadScene("Main_Lobby");
    }
    private void OnDisable()
    {
        NetEventManager.UnRegist("UpdateRanking", S2CL_UpdateRanking);
    }
}
