using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

[System.Serializable]
public class Sound
{
    [SerializeField] public string soundname;

    public AudioClip sound;

    [SerializeField]
    [Range(0f, 1f)]
    public float volume;

    [SerializeField]
    [Range(.1f, 1f)]
    public float pitch;

    public bool loop;

    [HideInInspector]
    public AudioSource source;
}