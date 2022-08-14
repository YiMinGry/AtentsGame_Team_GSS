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
        // bestScore = �������� MiniGame5 ĳ������ �ְ����� �޾ƿͼ� ����
    }

    public void Refresh_RunFriendBuffTxt()
    {
        // runFriendBuffTxt = ������ �÷��̾� ȿ�� MiniGame5_GameManager - �̴�ģ�� ��ũ���ͺ������Ʈ �������� �޾ƿͼ� ����
    }

    public void Refresh_NextRunImg()
    {
        // nextRunFriendImg = �̾�޸��� �ٲ�� MiniGame5_GameManager ���� �޾ƿͼ� ����
    }

    public override void ReSet()
    {
        Refresh_BestScore();
    }
}
