using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    [SerializeField] private AudioSource bgm, sfx;
    
    private float bgmVolume = 1f;
    private float sfxVolume = 1f;

    private void Awake()
    {
        Instance = this;

        if(PlayerPrefs.HasKey("setting-volume-bgm")) bgmVolume = PlayerPrefs.GetFloat("setting-volume-bgm");
        if(PlayerPrefs.HasKey("setting-volume-sfx")) sfxVolume = PlayerPrefs.GetFloat("setting-volume-sfx");
    }

    public void PlaySFX(AudioClip clip, float volume = 1f, float pitch = 1f)
    {
        sfx.volume = volume * sfxVolume;
        sfx.pitch = pitch;
        sfx.PlayOneShot(clip);
    }

    public void PlayBGM(AudioClip clip, float volume = 1f)
    {
        bgm.Stop();
        bgm.clip = clip;
        bgm.volume = bgmVolume * volume;
        bgm.Play();
    }

    public void StopBGM() => bgm.Stop();

}
