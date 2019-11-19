using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public Slider volumeSlider;
    public GameObject mute, unmute;

    void Start()
    {
        volumeSlider.value = volumeSlider.maxValue;
        mute.SetActive(false);
        unmute.SetActive(true);
    }

    public void VolumeChanged(float value)
    {
        if (volumeSlider.value == 0)
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
        volumeSlider.value = 0;
    }

    public void Unmute()
    {
        mute.SetActive(false);
        unmute.SetActive(true);
        volumeSlider.value = 1;
    }
}
