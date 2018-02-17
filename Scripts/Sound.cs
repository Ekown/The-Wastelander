using UnityEngine.Audio;
using UnityEngine;
using System;
using System.Collections.Generic;

[System.Serializable]
public class Sound {

    public string name;

    public AudioClip clip;

    public SoundTypes type;

    [Range(0f, 1f)]
    [HideInInspector]
    public float volume;

    [HideInInspector]
    public bool loop;

    //[HideInInspector]
    public AudioSource source;

    public enum SoundTypes
    {
        Music, SFX
    }
}
