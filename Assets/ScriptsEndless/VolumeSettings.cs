using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeSettings : MonoBehaviour
{
    public AudioMixer Mixer;

    public void setVolumeMainMenu(float sliderValue)
    {
        Mixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("MusicVolume", sliderValue);
    }


    public void setVolumeFX(float sliderValue)
    {
        Mixer.SetFloat("FxVol", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("FxVolume", sliderValue);
    }
}