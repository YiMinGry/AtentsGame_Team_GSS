using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGame5_StartUI : MiniGame5_UI
{
    MiniGame5_StartChoiceUI choiceUI;
    MiniGame5_StartScene startScene;

    Button runFriendBtn;
    Button nextRunFriendBtn;
    Button buffFriendBtn;

    Text runFriendBuffTxt;
    Image nextRunFriendImg;

    Text bestScore;
    Button practiceBtn;

    Button playBtn;

    public override void Init()
    {
        //Debug.Log($"StartUI state = {(UIState)Id}");

        runFriendBtn = transform.Find("RunFriendBtn").GetComponent<Button>();
        nextRunFriendBtn = transform.Find("NextRunBtn").GetComponent<Button>();
        buffFriendBtn = transform.Find("BuffFriendBtn").GetComponent<Button>();

        runFriendBuffTxt = transform.Find("CharactorBuffTxt").GetComponent<Text>();
        nextRunFriendImg = transform.Find("NextRunBtn").Find("Mask").GetComponentInChildren<Image>();

        bestScore = transform.Find("ScoreBoard").Find("BestScore").GetComponent<Text>();
        practiceBtn = transform.Find("ScoreBoard").Find("PracticeBtn").GetComponent<Button>();

        playBtn = transform.Find("PlayBtn").GetComponent<Button>();

        choiceUI = transform.parent.Find("StartChoiceUI").GetComponent<MiniGame5_StartChoiceUI>();

        // Settings =========================================================================

        runFriendBtn.onClick.AddListener(() => choiceUI.OpenScene(ChoiceState.RunFriend));
        nextRunFriendBtn.onClick.AddListener(() => choiceUI.OpenScene(ChoiceState.NextRunFriend));
        buffFriendBtn.onClick.AddListener(() => choiceUI.OpenScene(ChoiceState.BuffFriend));

        practiceBtn.onClick.AddListener(() => MiniGame5_SceneManager.Inst.OnPlay(true));
        playBtn.onClick.AddListener(() => MiniGame5_SceneManager.Inst.OnPlay(false));
    }

    public void Refresh_BestScore()
    {
        // bestScore = 서버에서 MiniGame5 캐릭터의 최고점수 받아와서 적용
    }

    public void Refresh_RunFriendBuffTxt()
    {
        // runFriendBuffTxt = 선택한 플레이어 효과 MiniGame5_GameManager - 미니친구 스크립터블오브젝트 정보에서 받아와서 변경
    }

    public void Refresh_NextRunImg()
    {
        // nextRunFriendImg = 이어달리기 바뀌면 MiniGame5_GameManager 에서 받아와서 수정
    }

    public override void ReSet()
    {
        Refresh_BestScore();
    }
}
