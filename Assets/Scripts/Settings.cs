using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public Slider volumeSlider;
    public GameObject mute, unmute, mainMenu;

    void Start()
    {
        volumeSlider.value = volumeSlider.maxValue;
        mute.SetActive(false);  
        
    } 

    public void VolumeChanged(float value)
    {
        if(volumeSlider.value > 0)
        {
            mute.SetActive(false);
            unmute.SetActive(true);
        } else
        {
            mute.SetActive(true);
            unmute.SetActive(false);
        }
    }
    public void Mute(bool value)
    {
        if(value)
        {
            volumeSlider.value = 0;
        } else
        {
            volumeSlider.value = 1;
        }
    }
}
