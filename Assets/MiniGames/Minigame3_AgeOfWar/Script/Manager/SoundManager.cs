
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void SFXPlay(string sfxName,AudioClip clip)
    {
        GameObject obj=new GameObject(sfxName+"Sound");
        AudioSource audiosource = obj.AddComponent<AudioSource>();
        audiosource.clip = clip;
        audiosource.Play();
        Destroy(obj,clip.length);
    }
}
