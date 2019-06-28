using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{

    public AudioClip hitClip;
    public AudioClip dieClip;


    public void PlayHit()
    {
        PlayAudioClip(hitClip);
    }

    public void PlayDie()
    {
        PlayAudioClip(dieClip);
    }

    void PlayAudioClip(AudioClip clip)
    {
        GetComponent<AudioSource>().clip = clip;
        GetComponent<AudioSource>().Play();
    }
}
