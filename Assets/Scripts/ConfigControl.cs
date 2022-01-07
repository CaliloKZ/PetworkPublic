using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class ConfigControl : MonoBehaviour
{
    [SerializeField]
    private Slider _musicSlider,
                   _sfxSlider;

    [SerializeField]
    private string _sfxParameter = "SFXVolume",
                   _musicParameter = "MusicVolume";

    [SerializeField]
    private float _multiplier = 30f;

    [SerializeField]
    private AudioMixerGroup[] _outputs;//0 = music, 1 = sfx

    public float GetMusicValue()
    {
        return _musicSlider.value;
    }

    public float GetSFXValue()
    {
        return _sfxSlider.value;
    }

    
    private void Awake()
    {
        _musicSlider.onValueChanged.AddListener(MusicSliderValueChanged);
        _sfxSlider.onValueChanged.AddListener(SFXSliderValueChanged);
    }

    private void Start()
    {
        _musicSlider.value = PlayerPrefs.GetFloat(_musicParameter, _musicSlider.value);
        _sfxSlider.value = PlayerPrefs.GetFloat(_sfxParameter, _sfxSlider.value);
    }

    public void LoadConfigs(float musicValue, float sfxValue) 
    {
        _musicSlider.value = musicValue;
        _sfxSlider.value = sfxValue;
    }


    private void OnDisable()
    {
        PlayerPrefs.SetFloat(_musicParameter, _musicSlider.value);
        PlayerPrefs.SetFloat(_sfxParameter, _sfxSlider.value);
    }

    private void SFXSliderValueChanged(float value)
    {
        _outputs[1].audioMixer.SetFloat(_sfxParameter, Mathf.Log10(value) * _multiplier);
    }

    private void MusicSliderValueChanged(float value)
    {
        _outputs[0].audioMixer.SetFloat(_musicParameter, Mathf.Log10(value) * _multiplier);
    }
}
