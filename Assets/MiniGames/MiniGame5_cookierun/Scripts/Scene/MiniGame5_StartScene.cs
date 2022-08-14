using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGame5_StartScene : MiniGame5_Scene
{
    Animator playerAnim;
    
    public override void Init()
    {
        playerAnim = transform.Find("PlayerPos").GetChild(0).GetComponent<Animator>();
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
}
