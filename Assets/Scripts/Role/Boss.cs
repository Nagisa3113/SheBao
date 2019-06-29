using System.Collections;
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
        moveSpeed = 7;
        //iEDelegate = BulletController.Instance.FireRandom;
        //iEDelegate = BulletController.Instance.FireRound;
        iEDelegate = BulletController.Instance.FireArc;

    }

    // Start is called before the first frame update
    void Start()
    {
        isEnter = false;

        moveSpeed = 7f;
        moveDir = (new Vector3(Random.Range(-1, 1), -1, 0)).normalized; //开始时刻移动方向

    }

    private void OnEnable()
    {
        StartCoroutine(iEDelegate(this));
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
            EnemyController.bosslist.Remove(this);

            StopAllCoroutines();
            GameObject.Find("AudioController").GetComponent<AudioController>().PlayDie();

            Pool.Instance.ReturnCacheGameObejct(this.gameObject);
            ParticleController.Instance.CreateEnemyExplosion(transform.position);
            Pool.Instance.ReturnCacheGameObejct(text.gameObject);
            ParticleController.Instance.CreateEnemyExplosion(transform.position);

        }
    }


    IEnumerator AudioPlay()
    {
        isAudio = true;
        GameObject.Find("AudioController").GetComponent<AudioController>().PlayHit();
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
        if (transform.position.x > -35 && transform.position.x < 35 && transform.position.y > -3 && transform.position.y < 8)
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
        if (isEnter == true && (transform.position.x <= -35 || transform.position.x >= 35 || transform.position.y <= -3 || transform.position.y >= 3))
        {
            tempx = Random.Range(-30, 30);
            tempy = Random.Range(-3, 8);
            temp = new Vector3(tempx, tempy, 0);
            moveDir = (temp - transform.position).normalized;
        }
    }

    void CheckArrive()
    {
        if (Mathf.Abs(transform.position.x - temp.x) <= 1 && Mathf.Abs(transform.position.y - temp.y) <= 1)
        {
            tempx = Random.Range(-35, 35);
            tempy = Random.Range(-3, 8);
            temp = new Vector3(tempx, tempy, 0);
            moveDir = (temp - transform.position).normalized;
        }
    }


}
