using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicVolumeController : MonoBehaviour
{
    // Get the background audiosource 
    public AudioSource musicVolume;

    // Get the UI Slider component
    public Slider slider;

    void Update()
    {
        musicVolume.volume = slider.value;
    }

}

