using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGame5_SoundManager : MonoBehaviour
{
    AudioSource bgmAudio;
    AudioSource effectAudio;

    public AudioClip loadingSceneBGM;
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

    float bgmVolume = 0.2f;
    public float BgmVolume
    {
        get => bgmVolume;
        set
        {
            bgmAudio.volume = Mathf.Clamp(value, 0f, 0.2f);
        }
    }

    float effectVolume = 0.3f;
    public float EffectVolume
    {
        get => effectVolume;
        set
        {
            effectAudio.volume = Mathf.Clamp(value, 0f, 0.8f);
        }
    }

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

        bgmAudio.volume = bgmVolume;
        effectAudio.volume = effectVolume;
    }

    public void ClearBGM()
    {
        bgmAudio.clip = null;
        bgmAudio.Stop();
    }

    public void loadingBGM()
    {
        bgmAudio.clip = loadingSceneBGM;
        bgmAudio.Play();
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
        bgmAudio.clip = bonusTimeBGM;
        bgmAudio.Play();
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
