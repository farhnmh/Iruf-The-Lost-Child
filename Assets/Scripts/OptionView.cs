using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionView : MonoBehaviour
{
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;

    private void OnEnable()
    {
        bgmSlider.onValueChanged.AddListener(OnBGMVolumeChanged);
        sfxSlider.onValueChanged.AddListener(OnSFXVolumeChanged);
    }

    private void OnDisable()
    {
        bgmSlider.onValueChanged.RemoveListener(OnBGMVolumeChanged);
        sfxSlider.onValueChanged.RemoveListener(OnSFXVolumeChanged);
        
        PlayerPrefs.Save();
    }

    private void OnSFXVolumeChanged(float v)
    {
        PlayerPrefs.SetFloat("setting-volume-sfx", v);
    }

    private void OnBGMVolumeChanged(float v)
    {
        PlayerPrefs.SetFloat("setting-volume-bgm", v);
    }
}
