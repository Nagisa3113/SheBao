using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Boss : Role
{

    public Text text;

    bool isShaking;
    bool isHit;
    bool isAudio;

    public float shakeScale = 0.3f;
    public float freq = 1;

    public int shakeCD = 14;
    public int hitCD = 14;
    public int audioCD = 5;

    public delegate IEnumerator IEDelegate(Role enemy);

    IEDelegate iEDelegate = null;

    private void Awake()
    {
        //iEDelegate = BulletController.Instance.FireRandom;
        //iEDelegate = BulletController.Instance.FireRound;
        iEDelegate = BulletController.Instance.FireArc;

    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(iEDelegate(this));
    }

    private void Update()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        text.transform.position = screenPos;
    }



    void BeHit(Vector3 hitPos)
    {
        if (--hp > 0)
        {
            if (!isShaking)
                StartCoroutine(Shake());
            if (!isHit)
                StartCoroutine(HitPaticle(hitPos));
            if (!isAudio)
                StartCoroutine(AudioPlay());
        }
        else
        {
            Die();
        }
    }

    void Die()
    {
        StopAllCoroutines();
        Pool.Instance.ReturnCacheGameObejct(this.gameObject);
        ParticleController.Instance.CreateEnemyExplosion(transform.position);
    }

    IEnumerator AudioPlay()
    {
        isAudio = true;
        GetComponent<AudioController>().PlayHit();
        for (int i = 0; i < audioCD; i++)
        {
            yield return null;
        }
        isAudio = false;

    }

    IEnumerator Shake()
    {
        isShaking = true;
        Vector3 pos = transform.position;

        for (int i = 0; i < shakeCD; i++)
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


    IEnumerator HitPaticle(Vector3 colPos)
    {
        isHit = true;

        ParticleController.Instance.CreateEnemyhit(Vector3.Lerp(transform.position, colPos, 0.9f));

        for (int i = 0; i < hitCD; i++)
        {
            yield return 0;
        }
        isHit = false;
    }


    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.layer == LayerMask.NameToLayer("PlayerBullet"))
        {
            BeHit(collision.contacts[0].point);
        }
    }



}
