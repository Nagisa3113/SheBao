using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioClip playerDieClip;
    public AudioClip enemyDieClip;

    public void PlayPlayerDie(Transform transform)
    {
        AudioSource.PlayClipAtPoint(playerDieClip, transform.position);
    }

    public void PlayEnemyDie(Transform transform)
    {
        AudioSource.PlayClipAtPoint(enemyDieClip, transform.position);
    }

    void PlayAudioClip(AudioClip clip)
    {
        GetComponent<AudioSource>().clip = clip;
        GetComponent<AudioSource>().Play();
    }
}
