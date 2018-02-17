using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Universe;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : Manager<AudioManager> {

    public Sound[] sounds;

    public static AudioManager instance;

    void Awake()
    {
        //if (instance == null)
        //    instance = this;
        //else
        //{
        //    Destroy(gameObject);
        //    return;
        //}

        //DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            //s.source = gameObject.GetComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.loop;
        }
    }

    // Acts as a buffer to enable the gameObject of the AudioManager prefab
    public void BufferPlay(string name)
    {
        FindObjectOfType<AudioManager>().Play(name);
    }

    // Acts as a buffer to update the gameObject of the AudioManager prefab
    public void BufferUpdate(string type)
    {
        if(type == "Music")
        {
            FindObjectOfType<AudioManager>().UpdateMusicVolume();
        }
        else
        {
            FindObjectOfType<AudioManager>().UpdateSFXVolume();
        }
    }

    // Actually plays the audio clip from the audio source
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if(s == null)
        {
            Debug.LogWarning("Audio: " + name + " was not found.");
            return;
        }

        if (s.source == null)
        {
            Debug.LogWarning("Audio source is missing");
            return;
        }

        //if (s.source.isActiveAndEnabled == false)
        //{
        //    s.source.enabled = true;
        //}


        // If the sound is of Music Type
        if (s.type == 0)
        {
            s.source.volume = PlayerPrefs.GetFloat("MusicVolume");
            s.source.Play();
        }
        // If the sound is of SFX Type
        else
        {
            s.source.volume = PlayerPrefs.GetFloat("SFXVolume");
            s.source.Play();
        }

        
    }

    // Stops the audio
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Audio: " + name + " was not found.");
            return;
        }

        if (s.source == null)
        {
            Debug.LogWarning("Audio source is missing");
            return;
        }

        if(s.source.isPlaying == false)
        {
            Debug.LogWarning("The audio is already stopped");
            return;
        }
        else
        {
            s.source.Stop();
        }
    }

    public void UpdateMusicVolume()
    {
        GameObject musicSlider = GameObject.Find("MusicSlider");

        PlayerPrefs.SetFloat("MusicVolume", musicSlider.GetComponent<Slider>().value);

        foreach (Sound s in sounds)
        {
            if(s.type == 0)
            {
                s.source.volume = PlayerPrefs.GetFloat("MusicVolume");
            }
        }
    }

    public void UpdateSFXVolume()
    {
        GameObject SFXSlider = GameObject.Find("SFXSlider");

        PlayerPrefs.SetFloat("SFXVolume", SFXSlider.GetComponent<Slider>().value);

        foreach (Sound s in sounds)
        {
            if (s.type != 0)
            {
                s.source.volume = PlayerPrefs.GetFloat("SFXVolume");
            }
        }
    }

}
