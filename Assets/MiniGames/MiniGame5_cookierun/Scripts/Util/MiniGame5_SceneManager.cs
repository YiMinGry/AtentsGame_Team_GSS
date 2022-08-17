using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MiniGame5_SceneManager : MonoBehaviour
{
    // Scene �̵��� ���� ���� �ʰ� ��Ȳ�� ���� ������Ʈ SetActive �ϴ� ������ ����
    MiniGame5_UI[] uiList;
    public MiniGame5_UI[] UIList { get; }

    MiniGame5_Scene[] sceneList;
    public MiniGame5_Scene[] SceneList { get; }

    //UIState uiState = UIState.OpeningUI;
    //SceneState sceneState = SceneState.StartScene;

    static MiniGame5_SceneManager instance;
    public static MiniGame5_SceneManager Inst { get => instance; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            if (instance != this)
            {
                Destroy(this.gameObject);
            }
        }
    }

    public void Inintialize()
    {
        // GetComponent, FindTypeOf<> : ��Ȱ��ȭ �� ������Ʈ�� ã�� �� ����
        // GetComponentsInChildren : ��� �ڽ��� ã�ƿ� (������)
        Transform canvas = GameObject.Find("Canvas").transform;
        uiList = new MiniGame5_UI[canvas.childCount];
        for (int i = 0; i < uiList.Length; i++)
        {
            uiList[i] = canvas.GetChild(i).GetComponent<MiniGame5_UI>();
        }

        //Debug.Log($"uiList size = {uiList.Length}");

        Transform loadScene = GameObject.Find("LoadScene").transform;
        sceneList = new MiniGame5_Scene[loadScene.childCount];
        for (int i = 0; i < sceneList.Length; i++)
        {
            sceneList[i] = loadScene.GetChild(i).GetComponent<MiniGame5_Scene>();
        }
        //Debug.Log($"sceneList size = {sceneList.Length}");

        for (int i = 0; i < uiList.Length; i++)
        {
            uiList[i].Id = i;
            uiList[i].Init();
        }
        for (int i = 0; i < sceneList.Length; i++)
        {
            sceneList[i].Id = i;
            sceneList[i].Init();
        }

        OnOpening();
    }

    public void OnOpening()
    {
        uiList[(int)UIState.OpeningUI].OpenScene();
        uiList[(int)UIState.StartUI].CloseScene();
        uiList[(int)UIState.StartChoiceUI].CloseScene();
        uiList[(int)UIState.MainPlayingUI].CloseScene();
        uiList[(int)UIState.GameEndUI].CloseScene();
        uiList[(int)UIState.RankingUI].CloseScene();

        sceneList[(int)SceneState.StartScene].OpenScene();
        sceneList[(int)SceneState.MainScene].CloseScene();
    }

    public void OnStart()
    {
        uiList[(int)UIState.StartUI].OpenScene();
        uiList[(int)UIState.OpeningUI].CloseScene();
    }

    public void OnChoice(ChoiceState state, uint id)
    {

    }

    public void OnPlay(bool isPractice = false)
    {
        uiList[(int)UIState.MainPlayingUI].gameObject.GetComponent<MiniGame5_MainPlayingUI>().isPractice = isPractice;

        StartCoroutine(CoBeforePlayAnim());
    }

    IEnumerator CoBeforePlayAnim()
    {
        sceneList[(int)SceneState.StartScene].StartScene();

        yield return new WaitForSeconds(sceneList[(int)SceneState.StartScene].WaitSeconds);

        uiList[(int)UIState.StartUI].CloseScene();
        uiList[(int)UIState.StartChoiceUI].CloseScene();
        sceneList[(int)SceneState.StartScene].CloseScene();

        uiList[(int)UIState.MainPlayingUI].OpenScene();
        sceneList[(int)SceneState.MainScene].OpenScene();

        yield return new WaitForSeconds(0.1f);
    }

    public void OnPlayStart()
    {
        uiList[(int)UIState.MainPlayingUI].StartScene();
        sceneList[(int)SceneState.MainScene].StartScene();
    }

    public void OnGameEnd()
    {
        StartCoroutine(CoGameEnd());
    }

    IEnumerator CoGameEnd()
    {
        sceneList[(int)SceneState.MainScene].EndScene();
        yield return new WaitForSeconds(sceneList[(int)SceneState.MainScene].WaitSeconds);
        
        uiList[(int)UIState.GameEndUI].OpenScene();
        uiList[(int)UIState.MainPlayingUI].CloseScene();
    }

    public void OnRanking()
    {
        StopAllCoroutines();
        sceneList[(int)SceneState.MainScene].CloseScene();
        sceneList[(int)SceneState.StartScene].OpenScene();

        uiList[(int)UIState.RankingUI].OpenScene();
        uiList[(int)UIState.RankingUI].StartScene();
        uiList[(int)UIState.GameEndUI].CloseScene();
    }

    public void OnReset()
    {
        MiniGame5_GameManager.Inst.GameSet();
        MiniGame5_GameManager.Inst.Player.Init();

        foreach (var ui in uiList)
        {
            ui.ReSet();
        }

        foreach (var scene in sceneList)
        {
            scene.ReSet();
        }
    }

    public void OnGoMainRoom()
    {
        SceneManager.LoadScene(0);
    }

    public void ChangeRunner()
    {
        sceneList[(int)SceneState.StartScene].ChangeCharator(ChoiceState.RunFriend);
        sceneList[(int)SceneState.MainScene].ChangeCharator(ChoiceState.RunFriend);

        uiList[(int)UIState.StartChoiceUI].Refresh(ChoiceState.RunFriend);
        uiList[(int)UIState.StartUI].Refresh(ChoiceState.RunFriend);
    }
    public void ChangeNextRunner()
    {
        uiList[(int)UIState.StartChoiceUI].Refresh(ChoiceState.NextRunFriend);
        uiList[(int)UIState.StartUI].Refresh(ChoiceState.NextRunFriend);
    }
    public void ChangePet()
    {
        sceneList[(int)SceneState.StartScene].ChangeCharator(ChoiceState.BuffFriend);
        sceneList[(int)SceneState.MainScene].ChangeCharator(ChoiceState.BuffFriend);

        uiList[(int)UIState.StartChoiceUI].Refresh(ChoiceState.BuffFriend);
        uiList[(int)UIState.StartUI].Refresh(ChoiceState.BuffFriend);
    }
}
