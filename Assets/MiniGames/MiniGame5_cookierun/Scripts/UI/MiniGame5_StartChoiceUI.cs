using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGame5_StartChoiceUI : MiniGame5_UI
{
    Button closeBtn;
    Button[] choiceTabBtn;
    Transform[] choiceCutList;

    Transform[] runnerList;
    public Transform[] RunnerList { get => runnerList; }
    
    Transform[] nextRunnerList;
    public Transform[] NextRunnerList { get => nextRunnerList; }

    Transform[] petList;
    public Transform[] PetList { get => petList; }

    public override void Init()
    {
        //Debug.Log($"StartChoiceUI state = {(UIState)Id}");

        closeBtn = transform.Find("CloseBtn").GetComponent<Button>();
        closeBtn.onClick.AddListener(CloseScene);
        closeBtn.onClick.AddListener(MiniGame5_SoundManager.Inst.PlayButtonClip);

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

        choiceTabBtn[0].onClick.AddListener(MiniGame5_SoundManager.Inst.PlayButtonClip);
        choiceTabBtn[1].onClick.AddListener(MiniGame5_SoundManager.Inst.PlayButtonClip);
        choiceTabBtn[2].onClick.AddListener(MiniGame5_SoundManager.Inst.PlayButtonClip);

        choiceTabBtn[0].onClick.AddListener(() => OpenScene(ChoiceState.RunFriend));
        choiceTabBtn[1].onClick.AddListener(() => OpenScene(ChoiceState.NextRunFriend));
        choiceTabBtn[2].onClick.AddListener(() => OpenScene(ChoiceState.BuffFriend));

        CreateMiniFriendList();
        ChangeHaveFrieHierarchy();
    }

    private void Start()
    {
        Refresh(ChoiceState.RunFriend);
        Refresh(ChoiceState.NextRunFriend);
        Refresh(ChoiceState.BuffFriend);
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

    public override void Refresh(ChoiceState state)
    {
        
        switch (state)
        {
            case ChoiceState.RunFriend:
                for (int i = 0; i < runnerList.Length; i++)
                {
                    if (runnerList[i].GetComponent<MiniGame5_ChoiceContentUI>().Data.isHave == true)
                    {
                        if (runnerList[i].GetComponent<MiniGame5_ChoiceContentUI>().Data == MiniGame5_GameManager.Inst.RunnerData)
                        {
                            runnerList[i].GetComponent<MiniGame5_ChoiceContentUI>().OnCheck(true);
                        }
                        else
                        {
                            runnerList[i].GetComponent<MiniGame5_ChoiceContentUI>().OnCheck(false);
                        }
                    }
                }
                break;
            case ChoiceState.NextRunFriend:
                for (int i = 0; i < nextRunnerList.Length; i++)
                {
                    if (nextRunnerList[i].GetComponent<MiniGame5_ChoiceContentUI>().Data.isHave == true)
                    {
                        if (nextRunnerList[i].GetComponent<MiniGame5_ChoiceContentUI>().Data == MiniGame5_GameManager.Inst.NextRunnerData)
                        {
                            nextRunnerList[i].GetComponent<MiniGame5_ChoiceContentUI>().OnCheck(true);
                        }
                        else
                        {
                            nextRunnerList[i].GetComponent<MiniGame5_ChoiceContentUI>().OnCheck(false);
                        }
                    }
                }
                break;
            case ChoiceState.BuffFriend:
                for (int i = 0; i < petList.Length; i++)
                {
                    if (petList[i].GetComponent<MiniGame5_ChoiceContentUI>().Data.isHave == true)
                    {
                        if (petList[i].GetComponent<MiniGame5_ChoiceContentUI>().Data == MiniGame5_GameManager.Inst.PetData)
                        {
                            petList[i].GetComponent<MiniGame5_ChoiceContentUI>().OnCheck(true);
                        }
                        else
                        {
                            petList[i].GetComponent<MiniGame5_ChoiceContentUI>().OnCheck(false);
                        }
                    }
                }
                break;
        }
    }

    void CreateMiniFriendList()
    {
        MiniGame5_DataManager data = MiniGame5_GameManager.Inst.MiniFriendData;

        Transform runParent = transform.Find("ActiveTabs").Find("RunFriendBtn").Find("Scroll").GetChild(0).GetChild(0);
        runnerList = new Transform[data.runnerLength];
        for (int i = 0; i < data.runnerLength; i++)
        {
            GameObject obj = Instantiate(data.choiceContentUI, runParent);
            obj.GetComponent<MiniGame5_ChoiceContentUI>().Data = data.runnerData[i];
            obj.GetComponent<MiniGame5_ChoiceContentUI>().State = ChoiceState.RunFriend;
            obj.GetComponent<MiniGame5_ChoiceContentUI>().Init();
            runnerList[i] = obj.transform;
        }

        Transform nextRunParent = transform.Find("ActiveTabs").Find("NextRunFriendBtn").Find("Scroll").GetChild(0).GetChild(0);
        nextRunnerList = new Transform[data.runnerLength];
        for (int i = 0; i < data.runnerLength; i++)
        {
            GameObject obj = Instantiate(data.choiceContentUI, nextRunParent);
            obj.GetComponent<MiniGame5_ChoiceContentUI>().Data = data.runnerData[i];
            obj.GetComponent<MiniGame5_ChoiceContentUI>().State = ChoiceState.NextRunFriend;
            obj.GetComponent<MiniGame5_ChoiceContentUI>().Init();
            nextRunnerList[i] = obj.transform;
        }

        Transform petParent = transform.Find("ActiveTabs").Find("BuffFriendBtn").Find("Scroll").GetChild(0).GetChild(0);
        petList = new Transform[data.petLength];
        for (int i = 0; i < data.runnerLength; i++)
        {
            GameObject obj = Instantiate(data.choiceContentUI, petParent);
            obj.GetComponent<MiniGame5_ChoiceContentUI>().Data = data.petData[i];
            obj.GetComponent<MiniGame5_ChoiceContentUI>().State = ChoiceState.BuffFriend;
            obj.GetComponent<MiniGame5_ChoiceContentUI>().Init();
            petList[i] = obj.transform;
        }
    }

    void ChangeHaveFrieHierarchy()
    {
        for (int i = runnerList.Length-1; i >= 0; i--)
        {
            if (runnerList[i].GetComponent<MiniGame5_ChoiceContentUI>().Data.isHave == true)
            {
                runnerList[i].transform.SetAsFirstSibling();
            }
        }
        
        for (int i = nextRunnerList.Length - 1; i >= 0; i--)
        {
            if (nextRunnerList[i].GetComponent<MiniGame5_ChoiceContentUI>().Data.isHave == true)
            {
                nextRunnerList[i].transform.SetAsFirstSibling();
            }
        }

        for (int i = petList.Length - 1; i >= 0; i--)
        {
            if (petList[i].GetComponent<MiniGame5_ChoiceContentUI>().Data.isHave == true)
            {
                petList[i].transform.SetAsFirstSibling();
            }
        }
    }
}