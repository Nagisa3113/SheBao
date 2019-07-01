using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Role
{
    public Text text;

    delegate IEnumerator IEDelegate(Enemy enemy);
    IEDelegate iEDelegate = null;

    public int index; //记录随机点

    Vector3 temp;

    float tempx, tempy;

    private void Awake()
    {
        iEDelegate = BulletController.Instance.FireRandom;
    }

    // Start is called before the first frame update
    void Start()
    {
        moveDir = (new Vector3(Random.Range(-30, 30), Random.Range(-100, 0), 0)).normalized; //开始时刻移动方向
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        text.transform.position = screenPos;
    }

    private void OnEnable()
    {
        StartCoroutine(iEDelegate(this));
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void FixedUpdate()
    {
        CheckInField();
        CheckArrive();
        transform.position += moveDir * moveSpeed * Time.fixedDeltaTime;
    }


    private void Update()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        text.transform.position = screenPos;
    }


    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.layer == LayerMask.NameToLayer("PlayerBullet"))
        {
            EnemyController.Instance.enemylist.Remove(this);

            GameObject.Find("AudioController").GetComponent<AudioController>().PlayDie();
            ParticleController.Instance.CreateEnemyhit(transform.position);

            Pool.Instance.ReturnCacheGameObejct(this.gameObject);
            Pool.Instance.ReturnCacheGameObejct(text.gameObject);
            ParticleController.Instance.CreateEnemyExplosion(transform.position);

        }
    }


    void CheckInField()
    {
        if (transform.position.x <= -35 || transform.position.x >= 35 || transform.position.y <= -5 || transform.position.y >= 20)
        {
            //temp = Random.Range(0, EnemyController.point.Length);
            //index = temp;
            //moveDir = (EnemyController.point[index] - transform.position).normalized;
            tempx = Random.Range(-35, 35);
            tempy = Random.Range(-5, 20);
            temp = new Vector3(tempx, tempy, 0);
            moveDir = (temp - transform.position).normalized;
        }
    }

    void CheckArrive()
    {
        if (Mathf.Abs(transform.position.x - temp.x) <= 1 && Mathf.Abs(transform.position.y - temp.y) <= 1)
        {
            tempx = Random.Range(-35, 35);
            tempy = Random.Range(-5, 20);
            temp = new Vector3(tempx, tempy, 0);
            moveDir = (temp - transform.position).normalized;
        }
    }
}
