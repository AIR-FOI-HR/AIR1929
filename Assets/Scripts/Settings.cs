using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.IO;

public class Settings : MonoBehaviour
{
    public Slider volumeSlider, controlSlider;
    public InputField nameField;
    public GameObject mute, unmute;
    public AudioMixer mixer;

    void Start()
    {
        if (PlayerPrefs.HasKey("Controls"))
        {
            controlSlider.value = PlayerPrefs.GetFloat("Controls");
        }
        if (PlayerPrefs.HasKey("PlayerName"))
        {
            nameField.text = PlayerPrefs.GetString("PlayerName");
        }
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

    public void SavePlayerName()
    {
        PlayerPrefs.SetString("PlayerName", nameField.text);
    }

    public void ControlChange(float value)
    {
        PlayerPrefs.SetFloat("Controls", controlSlider.value);
    }
}
