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

    int coin = 2;           // ������ �ִ� ���� ����(continue�ϸ� 1���� ����)
    int score = 0;          // ���� ����
    int stage = 1;          // ���� ��������
    int count = 30;         // continue �� �� ī��Ʈ
    int chance = 2;         // continue �� �� �ִ� ���� Ƚ��
    int healthPoint = 4;    // ���� hp
    int[] stageScoreSet = new int[] { 2000, 4000, 8000, 16000 }; // ���� ���������� �Ѿ�� ����

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
            coin = Mathf.Clamp(value, 0, 9999);
            mg2_UIManager.CoinUpdate(coin);
        }
    }


    public int Stage
    {
        get => stage;
        set
        {
            if(stage < value) // ������ ���� ����
            {
                mg2_UIManager.SetLevelUpUI(true);
            }
            stage = value;
            stage = Mathf.Clamp(stage, 1, 6);   // �������� 1~5�����ε� 6���� �س���
            playerLevelChange.Invoke();         // �������� ���� �� ����(������ or �ٽ��ϱ�) �÷��̾� ������ ����
        }
    }

    public int HealthPoint
    {
        get => healthPoint;
        set
        {
            healthPoint = Mathf.Clamp(value, 0, 4);     // HP �ִ� 4
            playerHPChange.Invoke();                    // HP ���� �� ���� FishBone UI ����
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
        Coin = (int)UserDataManager.instance.coin1;
        AudioManager.Inst.IsMusicOn = true;
        AudioManager.Inst.IsSoundOn = true;
        onGameStart = StartCoroutine(OnGameStart());
        AudioManager.Inst.PlayBGM("Fish_BGM");
    }

    private void OnEnable()
    {
        NetEventManager.Regist("UpdateRanking", S2CL_UpdateRanking);
        //NetEventManager.Regist("ReadRanking", S2CL_ReadRanking);
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
        chance = 2;
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
        Initialize();
        StartGame();
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
                mg2_UIManager.SetDashPanel(true);
                break;
            }
            yield return null;
        }
        yield return null;
    }


    IEnumerator GameOverCount()
    {
        mg2_UIManager.CoinUpdate(Coin);
        mg2_UIManager.SetContinuePanel(true);
        yield return new WaitForSeconds(2.0f);
        while (true)
        {
            mg2_UIManager.SetCountText(count--);
            yield return new WaitForSeconds(1.0f);
        }
    }    

    IEnumerator GameContinued()
    {
        while (true)
        {
            if (count < 0 || chance < 1)
            {
                CL2S_UpdateRanking(Score);
                mg2_UIManager.SetRankingPanel(true);
                mg2_UIManager.SetResultPanel(true);
                StartCoroutine(AfterGameOver());
                break;
            }
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                if (isYes) // Continue���� Yes ����
                {
                    if (Coin > 0)   // ������ ������ ���� 1�� ���� �� ���� ����
                    {
                        chance--;
                        Coin--;
                        UserDataManager.instance.CL2S_UserCoinUpdate(0, -1);
                        StartGame();
                    }
                    else            // ������ ������
                    {
                        Debug.Log("No Coin");
                    }
                }
                else
                {
                    CL2S_UpdateRanking(Score);
                    mg2_UIManager.SetRankingPanel(true);
                    mg2_UIManager.SetResultPanel(true);
                    StartCoroutine(AfterGameOver());
                    break;
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
        yield return null;
    }

    IEnumerator AfterGameOver()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GoToLobby();
                //RestartGame(); ����׿�
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

        JObject _data = JObject.Parse(_jdata["myRkData"].ToString());
        //Debug.Log($"Update Ranking jdata : {_data}");
        mg2_UIManager.ResultUpdate(Score, Coin, _data);
    }

    //public void S2CL_ReadRanking(JObject _jdata)
    //{
    //    Debug.Log($"Read Ranking jdata : {_jdata}");
    //    JArray _arr = JArray.Parse(_jdata["Top10"].ToString());

    //    mg2_UIManager.SetTop10Rank(_arr);
    //}

    public void GoToLobby()
    {
        AudioManager.Inst.IsMusicOn = false;
        AudioManager.Inst.IsSoundOn = false;
        TimeStop(false);
        StopAllCoroutines();
        //bl_SceneLoaderManager.LoadScene("Main_Lobby");    // ���ηκ�
        bl_SceneLoaderManager.LoadScene("Dev_Lobby");       // ����׿� �κ�
    }
    private void OnDisable()
    {
        //NetEventManager.UnRegist("ReadRanking", S2CL_ReadRanking);
        NetEventManager.UnRegist("UpdateRanking", S2CL_UpdateRanking);
    }
}
