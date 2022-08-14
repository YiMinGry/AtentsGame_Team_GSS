using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGame5_MainScene : MiniGame5_Scene
{
    MiniGame5_Player player;
    Animator playerAnim;
    MiniGame5_MapScroll mapScroll;
    Vector3 mapPos;

    public override void Init()
    {
        player = GetComponentInChildren<MiniGame5_Player>();
        playerAnim = player.transform.GetComponent<Animator>();
        mapScroll = GetComponentInChildren<MiniGame5_MapScroll>();
        mapPos = mapScroll.transform.localPosition;
    }

    public override void StartScene()
    {
        mapScroll.isStop = false;
    }

    public override void EndScene()
    {
        player.Die();
        waitSeconds = playerAnim.GetCurrentAnimatorStateInfo(0).length + 1.0f;

        mapScroll.isStop = true;
    }

    public override void ReSet()
    {
        player.transform.localPosition = player.startPosY * Vector3.up;

        mapScroll.isStop = true;
        mapScroll.transform.localPosition = mapPos;
    }
}
