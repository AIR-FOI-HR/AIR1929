using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Settings : MonoBehaviour
{
    public Slider volumeSlider;
    public GameObject mute, unmute;
    public AudioMixer mixer;

    void Start()
    {
        volumeSlider.value = volumeSlider.maxValue;
        mute.SetActive(false);
        unmute.SetActive(true);
    }

    public void VolumeChanged(float value)
    {
        mixer.SetFloat("MusicVol", Mathf.Log10(value) * 30);

        if (volumeSlider.value <= 0.001)
        {
            mute.SetActive(true);
            unmute.SetActive(false);
        }
        else
        {
            mute.SetActive(false);
            unmute.SetActive(true);
        }
    }

    public void Mute()
    {
        mute.SetActive(true);
        unmute.SetActive(false);
        volumeSlider.value = 0.0001f;
    }

    public void Unmute()
    {
        mute.SetActive(false);
        unmute.SetActive(true);
        volumeSlider.value = 1;
    }
}
