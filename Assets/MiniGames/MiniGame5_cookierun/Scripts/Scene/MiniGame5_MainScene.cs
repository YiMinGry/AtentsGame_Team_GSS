using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGame5_MainScene : MiniGame5_Scene
{
    Animator playerAnim;

    GameObject bonusMapObj;
    GameObject bonusBound;

    Transform playerPos;
    Transform petPos;

    Vector3 playerDiePos;

    MiniGame5_MapScroll mainMapScroll;
    MiniGame5_MapScroll bonusMapScroll;

    GameObject mainCam;
    GameObject bonusCam;

    Coroutine coBonus;

    public override void Init()
    {
        playerAnim = MiniGame5_GameManager.Inst.Player.transform.GetComponent<Animator>();

        bonusMapObj = transform.Find("BonusTimeMap").gameObject;
        bonusBound = bonusMapObj.transform.Find("Bound").gameObject;

        playerPos = transform.Find("PlayerPos");
        petPos = transform.Find("PetPos");

        mainMapScroll = transform.Find("Map").GetComponentInChildren<MiniGame5_MapScroll>();
        bonusMapScroll = bonusMapObj.GetComponentInChildren<MiniGame5_MapScroll>();

        mainCam = transform.Find("MainCamera").gameObject;
        bonusCam = bonusMapObj.transform.Find("BonusCamera").gameObject;

        bonusMapObj.SetActive(false);
        bonusBound.SetActive(false); 

        MiniGame5_GameManager.Inst.StartBonusTime += StartBonusTime;
        MiniGame5_GameManager.Inst.EndBonusTime += EndBonusTime;
    }

    public override void StartScene()
    {
        mainMapScroll.isStop = false;

        petPos.localPosition = new(0f, 0f, -0.4f);
        //petPos.GetComponentInChildren<CapsuleCollider>().enabled = true;
    }

    public override void EndScene()
    {
        MiniGame5_GameManager.Inst.Player.Die();
        playerDiePos = playerPos.localPosition;

        waitSeconds = playerAnim.GetCurrentAnimatorStateInfo(0).length + 1.0f;

        mainMapScroll.isStop = true;
        mainCam.GetComponent<MiniGame5_FollowingTarget>().enabled = false;
    }

    public override void ReSet()
    {
        MiniGame5_GameManager.Inst.Player.Init();

        mainMapScroll.ResetMap();
        bonusMapScroll.ResetMap();
    }

    public override void ChangeCharator(ChoiceState state)
    {
        switch (state)
        {
            case ChoiceState.RunFriend:
                ChangeRunner();
                break;
            case ChoiceState.NextRunFriend:
                ChangeNextRunner();
                break;
            case ChoiceState.BuffFriend:
                ChangePet();
                break;
        }
    }

    void ChangeRunner()
    {
        for (int i = 0; i < playerPos.childCount; i++)
        {
            Destroy(playerPos.GetChild(i).gameObject);
        }
        GameObject obj = Instantiate(MiniGame5_GameManager.Inst.RunnerData.prefab, playerPos);

        obj.AddComponent<MiniGame5_Player>();

        obj.GetComponent<Animator>().runtimeAnimatorController = MiniGame5_GameManager.Inst.MiniFriendData.mainAnimCon;
        playerAnim = obj.GetComponent<Animator>();
        
        MiniGame5_GameManager.Inst.Player = obj.GetComponent<MiniGame5_Player>();
        MiniGame5_GameManager.Inst.Player = MiniGame5_GameManager.Inst.Player;
    }

    void ChangeNextRunner()
    {
        playerPos.GetChild(0).parent = transform.Find("Map").Find("Main");

        GameObject obj = Instantiate(MiniGame5_GameManager.Inst.NextRunnerData.prefab, playerPos);
        obj.AddComponent<MiniGame5_Player>();

        obj.GetComponent<Animator>().runtimeAnimatorController = MiniGame5_GameManager.Inst.MiniFriendData.mainAnimCon;
        playerAnim = obj.GetComponent<Animator>();

        MiniGame5_GameManager.Inst.Player = obj.GetComponent<MiniGame5_Player>();
        MiniGame5_GameManager.Inst.Player = MiniGame5_GameManager.Inst.Player;

        obj.transform.localPosition = new(obj.transform.localPosition.x, obj.transform.localPosition.y, playerDiePos.z + 1f);
        mainCam.GetComponent<MiniGame5_FollowingTarget>().enabled = true;
        mainCam.GetComponent<MiniGame5_FollowingTarget>().Init(obj.transform);
        petPos.GetComponent<MiniGame5_FollowingTarget>().Init(obj.transform);
    }

    void ChangePet()
    {
        for (int i = 0; i < petPos.childCount; i++)
        {
            Destroy(petPos.GetChild(i).gameObject);
        }
        GameObject obj = Instantiate(MiniGame5_GameManager.Inst.PetData.prefab, petPos);
        Utill.ChangeLayersRecursively(obj.transform, 6);
    }

    public void StartBonusTime()
    {
        MiniGame5_SoundManager.Inst.PlayBonusTimeClip();
        MiniGame5_SoundManager.Inst.BonusTimeBGM();
        coBonus = StartCoroutine(CoStartBonusTime());
        MiniGame5_GameManager.Inst.Player.StartBonusTime();
    }

    IEnumerator CoStartBonusTime()
    {
        mainMapScroll.isStop = true;
        petPos.GetComponentInChildren<CapsuleCollider>().enabled = false;
        petPos.localPosition = new(0f, 0.5f, 0f);
        bonusBound.SetActive(true);
        
        yield return new WaitForSeconds(0.5f);

        bonusMapObj.gameObject.SetActive(true);
        mainCam.SetActive(false);
        bonusMapScroll.isStop = false;
    }

    public void EndBonusTime()
    {
        coBonus = StartCoroutine(CoEndBonusTime());
        MiniGame5_GameManager.Inst.Player.EndBonusTime();
    }

    IEnumerator CoEndBonusTime()
    {
        bonusBound.SetActive(false); 
        mainCam.SetActive(true);
        bonusCam.SetActive(false);

        yield return new WaitForSeconds(1f);

        bonusMapObj.gameObject.SetActive(false);
        bonusCam.SetActive(true);

        yield return new WaitForSeconds(0.1f);
        StopCoroutine(coBonus);
    }
}
