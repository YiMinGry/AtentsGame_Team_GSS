using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameOpeingUI : MonoBehaviour
{
    public Text teamLogo;
    public GameObject gameOpeningUI;
    public RectTransform gameLogo;
    bool sceneDone = false;

    public bl_SceneLoader sceneLoader;
    public bl_LoadingScreenUI sceneLoaderUI;

    private void Awake()
    {
        teamLogo.color = new(1, 1, 1, 0);
        gameLogo.GetComponent<Image>().color = new(1, 1, 1, 0);
        gameLogo.localScale = new(1.75f, 1.75f, 1.75f);
    }

    private void Start()
    {
        teamLogo.DOFade(1, 1.5f).SetLoops(2, LoopType.Yoyo).OnComplete(() =>
        {
            gameOpeningUI.SetActive(true);
            //gameLogo.DOScale(1.1f, 0.5f).SetLoops(-1, LoopType.Yoyo);
            //gameLogo.DOLocalRotate(new(0f, 0f, -5f), 0.5f).SetEase(Ease.InExpo).SetLoops(-1, LoopType.Yoyo);
            gameLogo.GetComponent<Image>().DOFade(1, 0.5f).SetEase(Ease.OutBounce);
            gameLogo.DOScale(1, 0.5f).SetEase(Ease.OutBounce);
            sceneDone = true;
        });
    }

    private void Update()
    {
        if (sceneDone)
        {
            if (Input.anyKeyDown)
            {
                //로딩시작
                sceneLoaderUI.ActiveBG();
                sceneLoader.canStartLoading = true;
            }
        }
    }
}
