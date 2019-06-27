using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Role
{

    Vector3 shootPos;

    bool isShaking;
    bool isHit;

    public bool alive;

    public float shakeScale = 0.3f;
    public float freq = 1;

    public int shakeCD = 14;
    public int hitCD = 14;

    public delegate IEnumerator IEDelegate(Enemy enemy);

    IEDelegate iEDelegate = null;

    private void Awake()
    {
        alive = true;

        //iEDelegate = BulletController.Instance.FireRandom;
        //iEDelegate = BulletController.Instance.FireRound;
        iEDelegate = BulletController.Instance.FireArc;

    }

    // Start is called before the first frame update
    void Start()
    {
        shootPos = transform.position;
        StartCoroutine(iEDelegate(this));
    }

    // Update is called once per frame
    void Update()
    {
        shootDir = transform.up;
    }


    void Die()
    {
        alive = false;

        GetComponent<Collider2D>().enabled = false;
        GetComponentInChildren<Text>().enabled = false;
        GetComponent<AudioController>().PlayDie();
        ParticleController.Instance.CreateEnemyExplosion(transform.position);
        Destroy(gameObject, 2);
    }



    IEnumerator StartShake()
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


    IEnumerator StartHitPaticle(Vector3 colPos)
    {
        isHit = true;

        GetComponentInChildren<ParticleSystem>().transform.position = Vector3.Lerp(transform.position, colPos, 0.9f);
        GetComponentInChildren<ParticleSystem>().Stop();
        GetComponentInChildren<ParticleSystem>().Play();

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
            hp--;

            if (hp < 0)
            {
                alive = false;
                Die();
            }

            else
            {
                if (!isShaking)
                {
                    StartCoroutine(StartShake());
                }
                if (!isHit)
                {
                    StartCoroutine(StartHitPaticle(collision.contacts[0].point));
                }
            }

            GetComponent<AudioController>().PlayHit();
        }
    }



}
