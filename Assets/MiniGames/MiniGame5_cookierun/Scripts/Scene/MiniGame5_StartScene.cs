using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGame5_StartScene : MiniGame5_Scene
{
    Transform playerPos;
    Transform petPos;

    Animator playerAnim;

    public override void Init()
    {
        playerPos = transform.Find("PlayerPos");
        petPos = transform.Find("PetPos");
        playerAnim = playerPos.GetChild(0).GetComponent<Animator>();
    }

    public override void CloseScene()
    {
        EndScene();
        base.CloseScene();
    }

    public override void StartScene()
    {
        playerAnim.SetBool("isPlayBtnPressed", true);
        waitSeconds = playerAnim.GetCurrentAnimatorStateInfo(0).length + 1.0f;
    }

    public override void ReSet()
    {
        playerAnim.SetBool("isPlayBtnPressed", false);
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
        obj.GetComponent<Animator>().runtimeAnimatorController = MiniGame5_GameManager.Inst.MiniFriendData.startAnimCon;
        playerAnim = obj.GetComponent<Animator>();
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
