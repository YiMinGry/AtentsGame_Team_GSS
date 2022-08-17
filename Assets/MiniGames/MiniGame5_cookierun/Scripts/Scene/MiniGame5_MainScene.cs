using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGame5_MainScene : MiniGame5_Scene
{
    MiniGame5_MapScroll mapScroll;

    Transform playerPos;
    Transform petPos;

    Animator playerAnim;
    Vector3 mapPos;

    public override void Init()
    {
        mapScroll = GetComponentInChildren<MiniGame5_MapScroll>();

        playerPos = transform.Find("PlayerPos");
        petPos = transform.Find("PetPos");

        playerAnim = MiniGame5_GameManager.Inst.Player.transform.GetComponent<Animator>();
        mapPos = mapScroll.transform.localPosition;
    }

    public override void StartScene()
    {
        mapScroll.isStop = false;
    }

    public override void EndScene()
    {
        MiniGame5_GameManager.Inst.Player.Die();
        waitSeconds = playerAnim.GetCurrentAnimatorStateInfo(0).length + 1.0f;

        mapScroll.isStop = true;
    }

    public override void ReSet()
    {
        MiniGame5_GameManager.Inst.Player.transform.localPosition = MiniGame5_GameManager.Inst.Player.startPosY * Vector3.up;

        mapScroll.isStop = true;
        mapScroll.transform.localPosition = mapPos;
    }

    public override void ChangeCharator(ChoiceState state)
    {
        switch (state)
        {
            case ChoiceState.RunFriend:
                ChangeRunner();
                break;
            case ChoiceState.NextRunFriend:
                break;
            case ChoiceState.BuffFriend:
                ChangePet();
                break;
        }
    }

    public void ChangeRunner()
    {
        for (int i = 0; i < playerPos.childCount; i++)
        {
            Destroy(playerPos.GetChild(i).gameObject);
        }
        GameObject obj = Instantiate(MiniGame5_GameManager.Inst.RunnerData.prefab, playerPos);

        obj.AddComponent<MiniGame5_Player>();
        MiniGame5_GameManager.Inst.Player = obj.GetComponent<MiniGame5_Player>();
        MiniGame5_GameManager.Inst.Player = MiniGame5_GameManager.Inst.Player;

        obj.GetComponent<Animator>().runtimeAnimatorController = MiniGame5_GameManager.Inst.MiniFriendData.startAnimCon;
        playerAnim = obj.GetComponent<Animator>();

        obj.AddComponent<CapsuleCollider>();
        obj.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.27f, 0.04f);
        obj.GetComponent<CapsuleCollider>().radius = 0.19f;
        obj.GetComponent<CapsuleCollider>().height = 0.54f;

        obj.AddComponent<Rigidbody>();
        obj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
        obj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
    }

    public void ChangePet()
    {
        for (int i = 0; i < petPos.childCount; i++)
        {
            Destroy(petPos.GetChild(i).gameObject);
        }
        GameObject obj = Instantiate(MiniGame5_GameManager.Inst.PetData.prefab, petPos);
    }
}
