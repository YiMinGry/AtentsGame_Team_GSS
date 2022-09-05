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

    // 변수 ----------------------------------------------------------------------------------------

    int coin = 2;           // 가지고 있는 코인 개수(continue하면 1개씩 감소)
    int score = 0;          // 현재 점수
    int stage = 1;          // 현재 스테이지
    int count = 30;         // continue 할 때 카운트
    int chance = 2;         // continue 할 수 있는 제한 횟수
    int healthPoint = 4;    // 현재 hp
    int[] stageScoreSet = new int[] { 2000, 4000, 8000, 16000 }; // 다음 스테이지로 넘어가는 점수

    bool isYes = true; // GameOver 화면에서 Continue 할 건지 묻는 변수

    Coroutine gameOverCount, gameContinued, onGameStart;

    // 델리게이트 ----------------------------------------------------------------------------------------

    public System.Action playerHPChange;
    public System.Action playerLevelChange;

    // 프로퍼티 ----------------------------------------------------------------------------------------
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
            if(stage < value) // 레벨업 했을 때만
            {
                mg2_UIManager.SetLevelUpUI(true);
            }
            stage = value;
            stage = Mathf.Clamp(stage, 1, 6);   // 스테이지 1~5까지인데 6으로 해놓음
            playerLevelChange.Invoke();         // 스테이지 변할 때 마다(레벨업 or 다시하기) 플레이어 프리팹 변경
        }
    }

    public int HealthPoint
    {
        get => healthPoint;
        set
        {
            healthPoint = Mathf.Clamp(value, 0, 4);     // HP 최대 4
            playerHPChange.Invoke();                    // HP 변할 때 마다 FishBone UI 변경
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

    // private 함수 --------------------------------------------------------------------------------------------------------------

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

    private void StartGame() // 처음 시작할 때, Continue 할 때 실행
    {
        if (gameOverCount != null)
            StopCoroutine(gameOverCount);
        if (onGameStart != null)
            StopCoroutine(onGameStart);
        if (gameContinued != null)
            StopCoroutine(gameContinued);

        HealthPoint = 4; // Continue 할 때 HP 풀로 실행
        Player.gameObject.SetActive(true);
        Player.transform.position = new Vector3(0, 0, 0); // 플레이어 위치 초기화

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("MG2_Fish_Enemy"); // 게임 내에 존재하는 모든 enemy fish 삭제
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
    /// true면 시간 정지, false면 시간 원래대로
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

    // public 함수 ---------------------------------------------------------------------------------------

    /// <summary>
    /// 게임 일시정지 또는 일시정지 해제 토글 함수
    /// </summary>
    public void PauseGame()
    {
        if (mg2_UIManager.SetPausePanel()) // true면 일시 정지 패널이 활성화 된 상태
        {
            TimeStop(true);
        }
        else
        {
            TimeStop(false);
        }
    }

    /// <summary>
    /// 게임 일시정지 함수
    /// </summary>
    /// <param name="_tf">true면 일시정지, false면 일시정지 해제</param>
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

    // 코루틴 --------------------------------------------------------------------------------------------

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
                if (isYes) // Continue에서 Yes 선택
                {
                    if (Coin > 0)   // 코인이 있으면 코인 1개 감소 후 게임 시작
                    {
                        chance--;
                        Coin--;
                        UserDataManager.instance.CL2S_UserCoinUpdate(0, -1);
                        StartGame();
                    }
                    else            // 코인이 없으면
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
                //RestartGame(); 디버그용
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
        //bl_SceneLoaderManager.LoadScene("Main_Lobby");    // 메인로비
        bl_SceneLoaderManager.LoadScene("Dev_Lobby");       // 디버그용 로비
    }
    private void OnDisable()
    {
        //NetEventManager.UnRegist("ReadRanking", S2CL_ReadRanking);
        NetEventManager.UnRegist("UpdateRanking", S2CL_UpdateRanking);
    }
}
