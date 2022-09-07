using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGame5_MapScroll : MonoBehaviour
{
    Transform bg;
    Transform main;

    public Vector3 bgPos = Vector3.zero;
    public Vector3 mainPos = Vector3.zero;

    public Vector3 bgScrollSpeed = Vector3.zero;
    public Vector3 mainScrollSpeed = Vector3.zero;
    
    public bool isStop = false;

    private void Awake()
    {
        bg = transform.Find("BG");
        main = transform.Find("Main");

        if (bg != null) bgPos = bg.position;
        if (main != null) mainPos = main.position;
    }

    private void Update()
    {
        if (isStop == false)
        {
            if (bg != null) bg.Translate(bgScrollSpeed * Time.deltaTime, Space.Self);
            if (main != null) main.Translate(mainScrollSpeed * Time.deltaTime, Space.Self);
        }
    }

    public void ResetMap()
    {
        isStop = true;
        if (bg != null) bg.position = new(bgPos.x, bgPos.y, bgPos.z);
        if (main != null) main.position = new(mainPos.x, mainPos.y, mainPos.z);
    }
}
