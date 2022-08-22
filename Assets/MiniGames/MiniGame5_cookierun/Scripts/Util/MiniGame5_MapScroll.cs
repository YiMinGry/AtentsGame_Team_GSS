using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGame5_MapScroll : MonoBehaviour
{
    Transform bg;
    Transform main;

    public Vector3 bgScrollSpeed = Vector3.zero;
    public Vector3 mainScrollSpeed = Vector3.zero;
    public bool isStop = false;

    private void Awake()
    {
        bg = transform.Find("BG");
        main = transform.Find("Main");
    }

    private void Update()
    {
        if (isStop == false)
        {
            bg.Translate(bgScrollSpeed * Time.deltaTime, Space.Self);
            main.Translate(mainScrollSpeed * Time.deltaTime, Space.Self);
        }
    }
}
