using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

// UI 애니메이션 컨트롤
public class UIAnimCon : MonoBehaviour
{
    public AnimConChanger animCon;

    public Transform mainUI;
    Transform mainUI_Coin;
    Transform mainUI_Icon;
    Text mainUI_Coin1;
    Text mainUI_Coin2;

    public Transform phoneUI;
    Transform phoneUI_Root;
    Transform phoneUI_Undo;

    // x값이 시작, y값이 도착
    Vector2 mainUICoin_StartEnd = new Vector2(-50f, 120f);
    Vector2 mainUIIcon_StartEnd = new Vector2(155f, -30f);
    Vector2 phoneUIRoot_StartEnd = new Vector2(0f, -920f);
    Vector2 phoneUIUndo_StartEnd = new Vector2(0f, -170f);

    public float animTime = 0.5f;

    private void Awake()
    {
        mainUI_Coin = mainUI.Find("CoinIcon");
        mainUI_Icon = mainUI.Find("IconBtn");

        mainUI_Coin1 = mainUI_Coin.Find("Coin1").GetComponent<Text>();
        mainUI_Coin2 = mainUI_Coin.Find("Coin2").GetComponent<Text>();

        phoneUI_Root = phoneUI.Find("Root");
        phoneUI_Undo = phoneUI.Find("IconBtn");

        if (animCon == null) animCon = FindObjectOfType<AnimConChanger>();
        animCon.PhoneAnim += TogglePhoneUI;

        mainUI_Icon.GetComponent<Button>().onClick.AddListener(animCon.TogglePhone);
        phoneUI_Undo.GetComponentInChildren<Button>().onClick.AddListener(animCon.TogglePhone);

        SetPos();
        SetCoin();
    }

    void SetPos()
    {
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

    void SetCoin()
    {
        mainUI_Coin1.text = UserDataManager.instance.coin1.ToString();
        mainUI_Coin2.text = UserDataManager.instance.coin2.ToString();
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
        SetCoin();
        mainUI.gameObject.SetActive(true);
        mainUI_Coin.DOLocalMoveY(mainUICoin_StartEnd.x, animTime).SetEase(Ease.InOutBack);
        mainUI_Icon.DOLocalMoveY(mainUIIcon_StartEnd.x, animTime).SetEase(Ease.InOutBack);
        phoneUI_Root.DOLocalMoveY(phoneUIRoot_StartEnd.y, animTime).SetEase(Ease.InOutBack);
        phoneUI_Undo.DOLocalMoveY(phoneUIUndo_StartEnd.y, animTime).SetEase(Ease.InOutBack);

        yield return new WaitForSeconds(animTime);
        phoneUI.gameObject.SetActive(false);
    }
}
