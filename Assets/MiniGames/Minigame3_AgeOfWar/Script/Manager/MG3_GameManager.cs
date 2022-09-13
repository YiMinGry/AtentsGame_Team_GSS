using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class MG3_GameManager : MonoBehaviour
{
    MG3_UnitDataManager unitDataMgr;
    MG3_TurretDataManager turretDataMgr;
    [SerializeField] private int gold = 0;
    [SerializeField] private int exp = 0;
    private int revolution = 0;
    private int numTurretSlot = 0;
    private int[] addSlotCosts = new int[] { 3000, 8000 };
    private int[] revolExps = new int[] { 4000, 10000 };
    private GameObject[] bases = new GameObject[3];
    public GameObject[] enemyBases;
    public GameObject[] enemyTurretSlot;
    private MG3_Base myBase;
    private MG3_Base enemyBase;
    private Text getcoinText;

    MG3_Menu menu;
    private int enemyRevol = 0;
    public int score = 0;
    float playTime = 0;
    [SerializeField] private GameObject gameoverUI;
    bool isGameover = false;
    [SerializeField] Text gameOverMyScore;

    [SerializeField]
    List<TapperRankInfo> rankInfos = new List<TapperRankInfo>();
    

    //프로퍼티 -----------------------------------------------------
    public float PlayTime=>playTime;

    public bool IsGameover => isGameover;
    public int Gold { get { return gold; } set { gold = value; } }
    public int Exp 
    { 
        get { return exp; } 
        set 
        {
            exp = value;
            if (enemyRevol < 2 && MG3_GameManager.Inst.Exp > MG3_GameManager.Inst.RevolExps[enemyRevol] * 0.9f)
            {
                enemyBases[enemyRevol].SetActive(false);
                enemyRevol++;
                enemyBases[enemyRevol].SetActive(true);
                MG3_TurretSlot slot = enemyTurretSlot[enemyRevol].GetComponent<MG3_TurretSlot>();
                slot.SetTurret(enemyRevol,false);
                enemyBase.HpMax = enemyBase.HpMax * 2;
            }
        } 
    }
    public int EnemyRevol { get => enemyRevol; }
    public int Revolution { get => revolution; }
    public int NumTurretSlot => numTurretSlot;
    public MG3_UnitDataManager UnitDataMgr => unitDataMgr;
    public MG3_TurretDataManager TurretDataMgr => turretDataMgr;
    public MG3_Menu Menu => menu;
    public int AddSlotCost => addSlotCosts[Math.Min(1, numTurretSlot)];
    public int[] RevolExps => revolExps;
    //싱글톤--------------------------------------------------------------
    static MG3_GameManager instance = null;
    public static MG3_GameManager Inst
    {
        get => instance;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            instance.Initialize();
        }
        //else
        //{
        //    if (instance != this)
        //    {
        //        Destroy(this.gameObject);
        //    }
        //}
        NetEventManager.Regist("UpdateRanking", S2CL_UpdateRanking);//서버에서 UpdateRanking 커멘드로 패킷이 올경우 실행
        getcoinText = gameoverUI.transform.Find("GetCoin").GetComponent<Text>();
        Debug.Log(UserDataManager.instance.MG3PlayData._playCount);
    }
    
    //--------------------------------------------------------------------------
    private void Initialize()
    {
        
        menu=FindObjectOfType<MG3_Menu>();
        turretDataMgr=GetComponent<MG3_TurretDataManager>();
        unitDataMgr=GetComponent<MG3_UnitDataManager>();
        MG3_Base[] baseparents = FindObjectsOfType<MG3_Base>();
        GameObject baseparent;
        for (int i=0;i<baseparents.Length;i++)
        {
            if(baseparents[i].CompareTag("Unit"))
            {
                myBase=baseparents[i];
                baseparent = baseparents[i].transform.Find("Base").gameObject;
                for(int j=0;j<bases.Length;j++)
                {
                    bases[j] = baseparent.transform.GetChild(j).gameObject;
                    if(j>0)
                    {
                        bases[j].gameObject.SetActive(false);
                    }
                }
            }
            else
            {
                enemyBase=baseparents[i];
            }
        }
        MG3_TurretSlot slot = enemyTurretSlot[0].GetComponent<MG3_TurretSlot>();
        slot.SetTurret(0,false);
    }
    public void Revol()
    {
        if(revolution<2)
        {
            if (exp >= revolExps[revolution])
            {
                bases[revolution].SetActive(false);
                revolution++;
                bases[revolution].SetActive(true);
                myBase.HpMax = myBase.HpMax * 2;
            }
        }
        
    }
    
    public void AddTurretSlot()
    {if(numTurretSlot<2&&gold>=AddSlotCost)
        {
            gold -= AddSlotCost;
            
            numTurretSlot++;
        }
    }


    public void CL2S_UpdateRanking(int _score)
    {
        JObject _userData = new JObject();
        _userData.Add("cmd", "UpdateRanking");
        _userData.Add("ID", UserDataManager.instance.ID);
        _userData.Add("nickName", UserDataManager.instance.nickName);
        _userData.Add("MG_NAME", "MG_3");
        _userData.Add("Score", _score);

        NetManager.instance.CL2S_SEND(_userData);
    }

    public void S2CL_UpdateRanking(JObject _jdata)
    {
        JArray _arr = JArray.Parse(_jdata["allRankArr"].ToString());
        SetTop10Rank(_arr);
    }

    public void GameOver()
    {
        if (!isGameover)
        {
            CL2S_UpdateRanking(score);

            isGameover = true;
            
            //tapperUIManager.EndMyScore(Utill.ConvertNumberComma((int)score);
            gameOverMyScore.text = string.Format($"My Score : {score}");//위에꺼 푼거
            int getCoin = 1 + score / 1000;
            UserDataManager.instance.CL2S_UserCoinUpdate(0, getCoin);
            Debug.Log(UserDataManager.instance.MG3PlayData._playCount);


            
            StartCoroutine(UserDataManager.instance.AchivementCheck(3));

            getcoinText.text = $"Get  x{getCoin}";
            

            gameoverUI.SetActive(true);
            //Time.timeScale = 0;

            StartCoroutine(GameContinued());
        }
    }
    IEnumerator CheckAchieveCondition()
    {
        yield return new WaitForSeconds(0.5f);

        Debug.Log(UserDataManager.instance.MG3PlayData._playCount);


    }
    

    IEnumerator GameContinued()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
                //tapperUIManager.SetGameOverPopUp(false);
                gameoverUI.SetActive(false);
                Time.timeScale = 1;
                isGameover = false;
                StartGame();

                
                break;
            }
            if (Input.GetKeyDown(KeyCode.N))
            {

                Time.timeScale = 1;
                GoToLobby();

                
                break;
            }

            yield return null;

        }

        yield return null;
    }

    public void StartGame()
    {

        //tapperUIManager.SetTitle(false);
        //lineManager.StartWave();
        //bl_SceneLoaderManager.LoadScene("MG_S_03");
        UnityEngine.SceneManagement.SceneManager.LoadScene("MG_S_03");
    }

    public void GoToLobby()
    {

        MG3_SoundManager.instance.BgmStop();
        bl_SceneLoaderManager.LoadScene("Main_Lobby");
    }

    private void OnDisable()
    {
        NetEventManager.UnRegist("UpdateRanking", S2CL_UpdateRanking);
    }
    public void SetTop10Rank(JArray _arr)
    {
        for (int i = 0; i < _arr.Count; i++)
        {
            rankInfos[i].SetRank(($"{_arr[i]["Rank"]}등"), ($"{_arr[i]["nickName"]}"), ($"{_arr[i]["Score"]}점"));
        }
    }
    private void Update()
    {
        playTime += Time.deltaTime;
    }
}
