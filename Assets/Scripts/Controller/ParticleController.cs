using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ParticleController : MonoBehaviour
{

    public GameObject particleHit;
    public GameObject particleDie;


    public void PlayDie(Vector3 pos)
    {
        PlayParticleSystem(particleDie,pos);
    }

    public void PlayHit(Vector3 pos)
    {
        PlayParticleSystem(particleHit,pos);
    }


    void PlayParticleSystem(GameObject go, Vector3 pos)
    {
        go.GetComponent<ParticleSystem>().Stop();
        go.transform.position = pos;
        go.GetComponent<ParticleSystem>().Play();

    }





}
