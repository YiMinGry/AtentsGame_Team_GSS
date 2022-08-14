using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGame5_StartChoiceUI : MiniGame5_UI
{
    Button closeBtn;
    Button[] choiceTabBtn;
    Transform[] choiceCutList;

    public override void Init()
    {
        //Debug.Log($"StartChoiceUI state = {(UIState)Id}");

        closeBtn = transform.Find("CloseBtn").GetComponent<Button>();
        closeBtn.onClick.AddListener(CloseScene);

        choiceTabBtn = new Button[transform.Find("NonActiveTabs").childCount];
        for (int i = 0; i < choiceTabBtn.Length; i++)
        {
            choiceTabBtn[i] = transform.Find("NonActiveTabs").GetChild(i).GetComponent<Button>();
        }

        choiceCutList = new Transform[transform.Find("ActiveTabs").childCount];
        for (int i = 0; i < choiceCutList.Length; i++)
        {
            choiceCutList[i] = transform.Find("ActiveTabs").GetChild(i).GetComponent<Transform>();
        }

        choiceTabBtn[0].onClick.AddListener(() => OpenScene(ChoiceState.RunFriend));
        choiceTabBtn[1].onClick.AddListener(() => OpenScene(ChoiceState.NextRunFriend));
        choiceTabBtn[2].onClick.AddListener(() => OpenScene(ChoiceState.BuffFriend));
    }

    public void OpenScene(ChoiceState state)
    {
        gameObject.SetActive(true);

        //모든 탭 끄고
        foreach (var trn in choiceCutList)
        {
            trn.gameObject.SetActive(false);
        }

        //state에 해당하는 부분만 다시 active
        choiceCutList[(int)state].gameObject.SetActive(true);
    }

    // 이후 캐릭터 선택시 게임메니저나 씬매니저에 연결해서 씬&게임 화면에도 선택한 캐릭터가 적용되도록
    // 캐릭터들을 스크립터블 오브젝트로 만들 필요성
}