using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGame5_SettingUI : MiniGame5_UI
{
    GameObject detail;
    Button detailBtn;
    bool isDetailOpen = false;

    public override void Init()
    {
        //Debug.Log($"SettingUI state = {(UIState)Id}");

        detail = transform.Find("Setting").gameObject;
        detailBtn = GetComponent<Button>();

        detailBtn.onClick.AddListener(ToggleBtn);
        detailBtn.onClick.AddListener(MiniGame5_SoundManager.Inst.PlayButtonClip);
    }

    void ToggleBtn()
    {
        isDetailOpen = !isDetailOpen;
        detail.SetActive(isDetailOpen);
        Debug.Log("Setting Toggle");
    }
}