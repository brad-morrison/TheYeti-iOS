using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : TheYeti
{
    public static AudioManager audio;
    //
    public AudioSource source;
    public AudioClip success;

    private void Awake()
    {
        audio = this;
    }

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip _sound)
    {
        source.PlayOneShot(_sound);
        Debug.Log("playing sound");
    }

    public void Blah()
    {
        Debug.Log("I'm sending a message blah blah blah");
    }
}
