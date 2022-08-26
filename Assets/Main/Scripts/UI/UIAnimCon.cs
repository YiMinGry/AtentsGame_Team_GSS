using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIAnimCon : MonoBehaviour
{
    public AnimConChanger animCon;

    public Transform mainUI;
    Transform mainUI_Coin;
    Transform mainUI_Icon;
    public Transform phoneUI;
    Transform phoneUI_Root;
    Transform phoneUI_Undo;

    Vector2 mainUICoin_StartEnd = new Vector2(-50f, 120f);
    Vector2 mainUIIcon_StartEnd = new Vector2(155f, -30f);
    Vector2 phoneUIRoot_StartEnd = new Vector2(0f, -920f);
    Vector2 phoneUIUndo_StartEnd = new Vector2(0f, -170f);

    public float animTime = 0.5f;

    private void Awake()
    {
        mainUI_Coin = mainUI.Find("CoinIcon");
        mainUI_Icon = mainUI.Find("PhoneIcon");

        phoneUI_Root = phoneUI.Find("Root");
        phoneUI_Undo = phoneUI.Find("UndoIcon");

        if (animCon == null) animCon = FindObjectOfType<AnimConChanger>();
        animCon.PhoneAnim += TogglePhoneUI;

        float moveV = mainUICoin_StartEnd.y - mainUICoin_StartEnd.x;
        mainUICoin_StartEnd = new Vector2(mainUI_Coin.localPosition.y, 
                                          mainUI_Coin.localPosition.y + moveV);

        moveV = mainUIIcon_StartEnd.y - mainUIIcon_StartEnd.x;
        mainUIIcon_StartEnd = new Vector2(mainUI_Icon.localPosition.y, 
                                          mainUI_Icon.localPosition.y + moveV);

        moveV = phoneUIRoot_StartEnd.y - phoneUIRoot_StartEnd.x;
        phoneUIRoot_StartEnd = new Vector2(phoneUI_Root.localPosition.y, 
                                           phoneUI_Root.localPosition.y + moveV);

        moveV = phoneUIUndo_StartEnd.y - phoneUIUndo_StartEnd.x;
        phoneUIUndo_StartEnd = new Vector2(phoneUI_Undo.localPosition.y, 
                                           phoneUI_Undo.localPosition.y + moveV);

        phoneUI_Root.localPosition = new Vector2(phoneUI_Root.localPosition.x, phoneUIRoot_StartEnd.y);
        phoneUI_Undo.localPosition = new Vector2(phoneUI_Undo.localPosition.x, phoneUIUndo_StartEnd.y);
    }

    void TogglePhoneUI(bool isPhoneModeOn)
    {
        if (isPhoneModeOn) StartCoroutine(CoStartPhoneMode());
        else StartCoroutine(CoEndPhoneMode());
    }

    IEnumerator CoStartPhoneMode()
    {
        phoneUI.gameObject.SetActive(true);
        mainUI_Coin.DOLocalMoveY(mainUICoin_StartEnd.y, animTime).SetEase(Ease.InOutBack);
        mainUI_Icon.DOLocalMoveY(mainUIIcon_StartEnd.y, animTime).SetEase(Ease.InOutBack);
        phoneUI_Root.DOLocalMoveY(phoneUIRoot_StartEnd.x, animTime).SetEase(Ease.InOutBack);
        phoneUI_Undo.DOLocalMoveY(phoneUIUndo_StartEnd.x, animTime).SetEase(Ease.InOutBack);

        yield return new WaitForSeconds(animTime);
        mainUI.gameObject.SetActive(false);
    }

    IEnumerator CoEndPhoneMode()
    {
        mainUI.gameObject.SetActive(true);
        mainUI_Coin.DOLocalMoveY(mainUICoin_StartEnd.x, animTime).SetEase(Ease.InOutBack);
        mainUI_Icon.DOLocalMoveY(mainUIIcon_StartEnd.x, animTime).SetEase(Ease.InOutBack);
        phoneUI_Root.DOLocalMoveY(phoneUIRoot_StartEnd.y, animTime).SetEase(Ease.InOutBack);
        phoneUI_Undo.DOLocalMoveY(phoneUIUndo_StartEnd.y, animTime).SetEase(Ease.InOutBack);

        yield return new WaitForSeconds(animTime);
        phoneUI.gameObject.SetActive(false);
    }
}
