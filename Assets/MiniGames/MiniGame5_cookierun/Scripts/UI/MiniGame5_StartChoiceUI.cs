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

        //��� �� ����
        foreach (var trn in choiceCutList)
        {
            trn.gameObject.SetActive(false);
        }

        //state�� �ش��ϴ� �κи� �ٽ� active
        choiceCutList[(int)state].gameObject.SetActive(true);
    }

    // ���� ĳ���� ���ý� ���Ӹ޴����� ���Ŵ����� �����ؼ� ��&���� ȭ�鿡�� ������ ĳ���Ͱ� ����ǵ���
    // ĳ���͵��� ��ũ���ͺ� ������Ʈ�� ���� �ʿ伺
}