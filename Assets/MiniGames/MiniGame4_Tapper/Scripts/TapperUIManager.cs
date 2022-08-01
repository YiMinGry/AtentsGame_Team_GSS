using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class TapperUIManager : MonoBehaviour
{
    public TapperGameManager tapperGameManager;
    [SerializeField]
    private GameObject title;

    [SerializeField]
    private GameObject TutorialPopup;
    [SerializeField]
    private GameObject TutorialPopupCloser;
    [SerializeField]
    private GameObject gameOverPopUp;

    [SerializeField]
    private Text beerText;
    [SerializeField]
    private Text beerFailText;
    [SerializeField]
    private Text goldText;
    [SerializeField]
    private Text scoreText;
    //성공한 맥주 카운트 업데이트 함수
    //실패한 맥주 카운트 업데이트 함수
    //벌어들인 금액 업데이트 함수(맥주값 + 팁)
    //받은 맥주값 연출 함수
    //받은 팁 연출 함수


    [SerializeField]
    private Text gameOverMyScore;
    [SerializeField]
    List<TapperRankInfo> rankInfos = new List<TapperRankInfo>();

    public void ResetScale()
    {
        beerText.transform.localScale = Vector3.one;
        beerFailText.transform.localScale = Vector3.one;
        goldText.transform.localScale = Vector3.one;
        scoreText.transform.localScale = Vector3.one;
    }

    public void Update_beer(int _count)
    {
        beerText.text = Utill.ConvertNumberComma(_count);

        beerText.transform.DOPunchScale(Vector3.one, 0.25f);
    }
    public void Update_beerFail(int _count, int _maxCount)
    {
        beerFailText.text = string.Format($"{_count} / {_maxCount}");
        beerFailText.transform.DOPunchScale(Vector3.one, 0.25f);
    }
    public void Update_gold(int _count)
    {
        goldText.text = Utill.ConvertNumberComma(_count);
        goldText.transform.DOPunchScale(Vector3.one, 0.25f);

    }
    public void Update_score(int _count)
    {
        scoreText.text = Utill.ConvertNumberComma(_count);
        scoreText.transform.DOPunchScale(Vector3.one, 0.25f);
    }

    public bool IsTutorial()
    {
        return TutorialPopup.activeSelf;
    }

    public void SetTitle(bool _tf)
    {
        title.SetActive(_tf);
    }
    public void SetTutorialPopup(bool _tf)
    {
        TutorialPopup.SetActive(_tf);
    }
    public void SetTutorialPopupCloser(bool _tf)
    {
        TutorialPopupCloser.SetActive(_tf);
    }
    public void SetGameOverPopUp(bool _tf)
    {
        gameOverPopUp.SetActive(_tf);
    }

    public void EndMyScore(string _score) 
    {
        gameOverMyScore.text = string.Format($"My Score : {_score}");
    }

    public void SetTop10Rank(JArray _arr) 
    {
        for (int i = 0; i < _arr.Count; i++)
        {
            rankInfos[i].SetRank(($"{_arr[i]["Rank"]}등"), ($"{_arr[i]["nickName"]}"), ($"{_arr[i]["Score"]}점"));
        }
    }
}
