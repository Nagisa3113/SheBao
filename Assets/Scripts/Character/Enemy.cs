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

    bool alive;

    public float shakeScale = 0.3f;
    public float freq = 1;

    public int shakeCD = 14;
    public int hitCD = 14;


    private void Awake()
    {
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
            Die();
        }

    }


    void Die()
    {
        alive = false;

        GetComponent<Collider2D>().enabled = false;
        GetComponentInChildren<Text>().enabled = false;
        GetComponent<AudioController>().PlayDie();
        GetComponent<ParticleController>().PlayDie(transform.position);
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

        GetComponent<ParticleController>().PlayHit(Vector3.Lerp(transform.position, colPos, 0.9f));

        for (int i = 0; i < hitCD; i++)
        {
            yield return 0;
        }
        isHit = false;
    }



    void OnCollisionEnter2D(Collision2D collision)
    {

        ContactPoint2D c = collision.contacts[0];

        if (collision.gameObject.layer == LayerMask.NameToLayer("PlayerBullet"))
        {
            HP -= 1;
            if (!isShaking)
            {
                StartCoroutine(StartShake());
            }
            if (!isHit)
            {
                StartCoroutine(StartHitPaticle(c.point));
            }

            GetComponent<AudioController>().PlayHit();
        }
    }



}
