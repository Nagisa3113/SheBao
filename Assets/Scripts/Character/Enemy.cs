using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Role
{

    GameObject bullet;

    Vector3 shootPos;

    bool isShaking;

    public AudioClip[] audioClips;

    bool alive;

    public float shakeScale = 0.3f;
    public float freq = 1;

    public GameObject particleHit;
    public GameObject particleDie;

    int shakeDelta = 14;

    public Text text;

    private void Awake()
    {
        hpCurrent = hpMax = 20;
        bullet = (GameObject)Resources.Load("Prefabs/EnemyBullet", typeof(GameObject));
        alive = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        shootPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        shootDir = transform.up;

        if (HP <= 0 && alive == true)
        {
            alive = false;
            GetComponent<Collider2D>().enabled = false;
            GetComponent<AudioSource>().clip = audioClips[1];
            GetComponent<AudioSource>().Play();

            particleDie.GetComponent<ParticleSystem>().Play();
            text.color = Color.clear;
            Destroy(gameObject, 2);
        }

    }



    IEnumerator StartShake()
    {
        isShaking = true;
        Vector3 pos = transform.position;

        for (int i = 0; i < shakeDelta; i++)
        {
            float x = shakeScale * Random.Range(-1f, 1f);
            float y = shakeScale * Random.Range(-1f, 1f);

            Vector3 randShake = new Vector3(Mathf.PerlinNoise(Time.time * freq, 0) * x, Mathf.PerlinNoise(Time.time * freq, 0) * y, 0f);

            transform.position += randShake;
            yield return 0;
        }


        transform.position = pos;
        isShaking = false;
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("PlayerBullet"))
        {
            HP -= 1;
            if (!isShaking)
            {
                StartCoroutine(StartShake());
                particleHit.GetComponent<ParticleSystem>().Stop();
                particleHit.GetComponent<ParticleSystem>().Play();

            }

            GetComponent<AudioSource>().clip = audioClips[0];
            GetComponent<AudioSource>().Play();
        }
    }



}
