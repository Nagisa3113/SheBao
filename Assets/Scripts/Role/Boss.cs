﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Boss : Role
{
    bool isEnter;

    Vector3 temp;
    float tempx, tempy;


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
        int r = Random.Range(0, 2);
        switch (r)
        {
            case 0:
                iEDelegate = BulletController.Instance.FireRound;
                break;
            case 1:
                iEDelegate = BulletController.Instance.FireArc;
                break;
            default:
                iEDelegate = null;
                break;
        }


    }

    // Start is called before the first frame update
    void Start()
    {
        isEnter = false;
        moveSpeed = 7f;
        moveDir = (new Vector3(Random.Range(-1, 1), -1, 0)).normalized; //开始时刻移动方向
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        text.transform.position = screenPos;
    }

    private void OnEnable()
    {
        isShaking = false;
        isHit = false;
        isAudio = false;
        hp = 20;
        StartCoroutine(iEDelegate(this));
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void Update()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        text.transform.position = screenPos;
    }


    private void FixedUpdate()
    {
        if (!isEnter)
        {
            CheckInMoveField();
        }
        CheckInField();
        CheckArrive();
        transform.position += moveDir * moveSpeed * Time.fixedDeltaTime;
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
            EnemyController.Instance.bosslist.Remove(this);

            GameObject.Find("AudioController").GetComponent<AudioController>().PlayEnemyDie(this.transform);
            ParticleController.Instance.CreateEnemyExplosion(transform.position);

            Pool.Instance.ReturnCacheGameObejct(this.gameObject);
            Pool.Instance.ReturnCacheGameObejct(text.gameObject);

        }
    }


    IEnumerator AudioPlay()
    {
        isAudio = true;
        GetComponent<AudioSource>().Play();
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


    void CheckInMoveField()
    {
        if (transform.position.x > -35 && transform.position.x < 35 && transform.position.y > 0 && transform.position.y < 8)
        {
            isEnter = true;
        }
    }

    void CheckInField()
    {
        if (isEnter == false && (transform.position.x <= -35 || transform.position.x >= 35 || transform.position.y <= -5 || transform.position.y >= 20))
        {
            tempx = Random.Range(-35, 35);
            tempy = Random.Range(-5, 20);
            temp = new Vector3(tempx, tempy, 0);
            moveDir = (temp - transform.position).normalized;
        }
        if (isEnter == true && (transform.position.x <= -35 || transform.position.x >= 35 || transform.position.y <= 0 || transform.position.y >= 8))
        {
            tempx = Random.Range(-30, 30);
            tempy = Random.Range(0, 8);
            temp = new Vector3(tempx, tempy, 0);
            moveDir = (temp - transform.position).normalized;
        }
    }

    void CheckArrive()
    {
        if (Mathf.Abs(transform.position.x - temp.x) <= 1 && Mathf.Abs(transform.position.y - temp.y) <= 1)
        {
            tempx = Random.Range(-35, 35);
            tempy = Random.Range(0, 8);
            temp = new Vector3(tempx, tempy, 0);
            moveDir = (temp - transform.position).normalized;
        }
    }


}
