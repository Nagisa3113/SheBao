using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Role
{
    [Header("UI")]
    public Slider slider;

    GameObject bullet;

    Vector3 shootPos;

    [SerializeField]
    Pool pool;

    [SerializeField]
    bool isShaking;

    public float shakeScale = 0.3f;
    public float freq = 1;

    private void Awake()
    {
        bullet = (GameObject)Resources.Load("Prefabs/EnemyBullet", typeof(GameObject));
        pool = GameObject.Find("Pool").GetComponent<Pool>();
    }

    // Start is called before the first frame update
    void Start()
    {
        hpMax = hpCurrent = 100;

        shootPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        shootDir = transform.up;
        //slider.value = hpCurrent / hpMax;
    }



    IEnumerator StartShake()
    {
        isShaking = true;
        Vector3 pos = transform.position;

        for (int i = 0; i < 10; i++)
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
            HP -= 5;
            if (!isShaking)
            {
                StartCoroutine(StartShake());
            }

            //GetComponent<AudioSource>().Play();
        }
    }



}
