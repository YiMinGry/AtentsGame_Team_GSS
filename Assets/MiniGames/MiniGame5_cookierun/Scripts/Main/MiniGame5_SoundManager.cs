using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGame5_SoundManager : MonoBehaviour
{
    AudioSource bgmAudio;
    AudioSource effectAudio;

    public AudioClip startSceneBGM;
    public AudioClip mainSceneBGM;
    public AudioClip bonusTimeBGM;

    public AudioClip jumpCilp;
    public AudioClip slideClip;
    public AudioClip eatItemClip;
    //public AudioClip eatSpecialItemClip;
    public AudioClip btnClip;
    public AudioClip startBtnClip;
    public AudioClip bonusTimeClip;
    public AudioClip damageClip;
    public AudioClip dieClip;

    static MiniGame5_SoundManager instance;
    public static MiniGame5_SoundManager Inst { get => instance; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            Initialize();
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            if (instance != this)
            {
                Destroy(this.gameObject);
            }
        }
    }

    public void Initialize()
    {
        bgmAudio = transform.Find("BGM").GetComponent<AudioSource>();
        effectAudio = transform.Find("Effect").GetComponent<AudioSource>();

        bgmAudio.loop = true;
        effectAudio.loop = false;
    }

    public void ClearBGM()
    {
        bgmAudio.clip = null;
        bgmAudio.Stop();
    }

    public void StartBGM()
    {
        bgmAudio.clip = startSceneBGM;
        bgmAudio.Play();
    }

    public void MainBGM()
    {
        bgmAudio.clip = mainSceneBGM;
        bgmAudio.Play();
    }

    public void BonusTimeBGM()
    {
        if (!bgmAudio.isPlaying) bgmAudio.Play();
        bgmAudio.clip = bonusTimeBGM;
    }

    public void PlayJumpCilp()
    {
        effectAudio.PlayOneShot(jumpCilp);
    }
    public void PlaySlideClip()
    {
        effectAudio.PlayOneShot(slideClip);
    }
    public void PlayEatItemClip()
    {
        effectAudio.PlayOneShot(eatItemClip);
    }
    public void PlayEatSpecialItemClip()
    {
        //effectAudio.PlayOneShot(eatSpecialItemClip);
    }
    public void PlayButtonClip()
    {
        effectAudio.PlayOneShot(btnClip);
    }
    public void PlayStartButtonClip()
    {
        effectAudio.PlayOneShot(startBtnClip);
    }
    public void PlayBonusTimeClip()
    {
        effectAudio.PlayOneShot(bonusTimeClip);
    }
    public void PlayDamageClip()
    {
        effectAudio.PlayOneShot(damageClip);
    }
    public void PlayDieClip()
    {
        effectAudio.PlayOneShot(dieClip);
    }
}
