using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class TapperGameManager : MonoBehaviour
{
    [SerializeField]
    private LineManager _lineManager;
    [SerializeField]
    private TapperUIManager _tapperUIManager;
    [SerializeField]
    private TapperTextBoxManager _tapperTextBoxManager;
    [SerializeField]
    private PlayerMove _playerMove;

    public LineManager lineManager
    {
        get
        {
            return _lineManager;
        }
    }

    public TapperUIManager tapperUIManager
    {
        get
        {
            return _tapperUIManager;
        }
    }
    public PlayerMove playerMove
    {
        get
        {
            return _playerMove;
        }
    }
    public TapperTextBoxManager tapperTextBoxManager
    {
        get
        {
            return _tapperTextBoxManager;
        }
    }

   public void CL2S_UpdateRanking(int _score)
    {
        JObject _userData = new JObject();
        _userData.Add("cmd", "UpdateRanking");
        _userData.Add("ID", UserDataManager.instance.ID);
        _userData.Add("nickName", UserDataManager.instance.nickName);
        _userData.Add("MG_NAME", "MG_4");
        _userData.Add("Score", _score);

        NetManager.instance.CL2S_SEND(_userData);
    }

    public void S2CL_UpdateRanking(JObject _jdata)
    {
        JArray _arr = JArray.Parse(_jdata["allRankArr"].ToString());

        tapperUIManager.SetTop10Rank(_arr);
    }

    private void Awake()
    {
        lineManager.tapperGameManager = this;
        tapperUIManager.tapperGameManager = this;
        playerMove.tapperGameManager = this;

        NetEventManager.Regist("UpdateRanking", S2CL_UpdateRanking);//서버에서 UpdateRanking 커멘드로 패킷이 올경우 실행

        tapperUIManager.Update_beerFail(beerFailCount, beerFailMaxCount);
    }


    private int beerCount = 0;//주문 성공 카운트
    private int beerFailCount = 0;//주문 실패 카운트
    private int beerFailMaxCount = 5;//최대 주문 실패 갯수
    private int tipCount = 0;//받은 팁 카운트
    private int goldAmount = 0;//벌어들인 돈(맥주값)
    private int tipAmount = 0;//받은 팁 값
    private int score = 0;

    private bool isGameOver = false;

    public void ResetCounts()
    {
        beerCount = 0;
        beerFailCount = 0;
        tipCount = 0;
        goldAmount = 0;
        tipAmount = 0;
        score = 0;

        tapperUIManager.Update_beer(beerCount);
        tapperUIManager.Update_gold(goldAmount + tipAmount);
        tapperUIManager.Update_beerFail(beerFailCount, beerFailMaxCount);
        tapperUIManager.Update_score(score);

        tapperUIManager.ResetScale();
    }

    public void AddBeer()
    {
        beerCount++;

        goldAmount = beerCount * 15;
        tapperUIManager.Update_beer(beerCount);
        tapperUIManager.Update_gold(goldAmount + tipAmount);

    }
    public void AddBeerFail()
    {
        if (isGameOver == true)
        {
            return;
        }

        beerFailCount++;

        tapperUIManager.Update_beerFail(beerFailCount, beerFailMaxCount);
    }

    public void AddTip()
    {
        tipCount++;

        tipAmount = tipCount * 20;
        tapperUIManager.Update_gold(goldAmount + tipAmount);
    }


    public void Update()
    {
        if (score < (beerCount * 5) + (tipCount * 25))
        {
            score = (beerCount * 5) + (tipCount * 25);
            tapperUIManager.Update_score(score);
        }

        if (isGameOver == false)
        {
            if (beerFailCount >= beerFailMaxCount)
            {
                GameOver();
            }
        }
    }


    public void GameOver()
    {

        CL2S_UpdateRanking(score);

        isGameOver = true;

        tapperUIManager.EndMyScore(Utill.ConvertNumberComma(score));

        tapperUIManager.SetGameOverPopUp(true);
        playerMove.enabled = false;

        StartCoroutine(GameContinued());
    }

    IEnumerator GameContinued()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
                tapperUIManager.SetGameOverPopUp(false);

                isGameOver = false;

                StartGame();
                break;
            }
            if (Input.GetKeyDown(KeyCode.N))
            {
                GoToLobby();
                break;
            }

            yield return null;

        }

        yield return null;
    }

    public void StartGame()
    {
        playerMove.enabled = true;
        tapperUIManager.SetTitle(false);
        lineManager.StartWave();
    }
    public void StartTutorial()
    {
        playerMove.enabled = true;
        tapperUIManager.SetTitle(false);
        tapperUIManager.SetTutorialPopup(true);
        tapperUIManager.SetTutorialPopupCloser(false);
        lineManager.StartTutorial();
    }

    public void GoToLobby()
    {
        bl_SceneLoaderManager.LoadScene("Main_Lobby");
    }

    private void OnDisable()
    {
        NetEventManager.UnRegist("UpdateRanking", S2CL_UpdateRanking);
    }
}
