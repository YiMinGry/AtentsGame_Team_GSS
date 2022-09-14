using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MiniGame5_SettingUI : MiniGame5_UI
{
    GameObject detail;
    Button detailBtn;
    bool isDetailOpen = false;

    Slider bgmSlider;
    Slider effectSlider;
    Button closeBtn;
    Button mainRoomBtn;

    public override void Init()
    {
        //Debug.Log($"SettingUI state = {(UIState)Id}");

        detail = transform.Find("Setting").gameObject;
        detailBtn = GetComponent<Button>();

        detailBtn.onClick.AddListener(ToggleBtn);
        detailBtn.onClick.AddListener(MiniGame5_SoundManager.Inst.PlayButtonClip);

        bgmSlider = detail.transform.Find("BGSound").GetComponent<Slider>();
        effectSlider = detail.transform.Find("EffectSound").GetComponent<Slider>();

        bgmSlider.value = MiniGame5_SoundManager.Inst.BgmVolume;
        effectSlider.value = MiniGame5_SoundManager.Inst.EffectVolume;
        bgmSlider.onValueChanged.AddListener((value) => MiniGame5_SoundManager.Inst.BgmVolume = value);
        effectSlider.onValueChanged.AddListener((value) => MiniGame5_SoundManager.Inst.EffectVolume = value);

        closeBtn = detail.transform.Find("CloseBtn").GetComponent<Button>();
        mainRoomBtn = detail.transform.Find("MainRoomBtn").GetComponent<Button>();

        closeBtn.onClick.AddListener(MiniGame5_SoundManager.Inst.PlayButtonClip);
        mainRoomBtn.onClick.AddListener(MiniGame5_SoundManager.Inst.PlayButtonClip);
        closeBtn.onClick.AddListener(ToggleBtn);
        mainRoomBtn.onClick.AddListener(() => bl_SceneLoaderManager.LoadScene("Main_Lobby"));
    }

    void ToggleBtn()
    {
        isDetailOpen = !isDetailOpen;
        detail.SetActive(isDetailOpen);
        //Debug.Log("Setting Toggle");
        if (isDetailOpen) Time.timeScale = 0;
        else Time.timeScale = 1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleBtn();
        }
    }
}