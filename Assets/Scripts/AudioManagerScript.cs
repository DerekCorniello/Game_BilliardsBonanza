using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManagerScript : MonoBehaviour
{
    public SoundClass[] sounds;
    public static float globalVolume = 20f;

    void Awake()
    {
       foreach(SoundClass s in sounds)
       {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = globalVolume;
            s.source.pitch = s.pitch;

            s.source.loop = s.loop;
        } 
    }

    public void OnAdjustVolume(float volume)
    {
        foreach (SoundClass s in sounds)
        {
            s.source.volume = volume;
        }
        globalVolume = volume;
    }

    public void Play(string name)
    {
        SoundClass match = null;
        foreach (SoundClass s in sounds)
        {
            bool isMatch = false;
            if (s.name == name)
            {
                match = s;
                isMatch = true;
            }
            if (isMatch) break;
        }

        if (match != null)
        {
            match.source.Play();
        }
    }
}