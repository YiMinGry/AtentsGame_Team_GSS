
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG3_SoundManager : MonoBehaviour
{
    public static MG3_SoundManager instance;
    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
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
    public void BgmStop()
    {
        GetComponent<AudioSource>().volume = 0;
    }
}
