using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGame5_OpeningUI : MiniGame5_UI
{
    Button startBtn;

    public override void Init()
    {
        //Debug.Log($"OpeningUI state = {(UIState)Id}");

        startBtn = transform.Find("StartBtn").GetComponent<Button>();
        startBtn.onClick.AddListener(MiniGame5_SceneManager.Inst.OnStart);
    }
}