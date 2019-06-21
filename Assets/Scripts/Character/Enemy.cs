using System.Collections;
using System.Collections.Generic;
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


    private void Awake()
    {
        bullet = (GameObject)Resources.Load("Prefabs/EnemyBullet", typeof(GameObject));
        pool = GameObject.Find("EnemyPool").GetComponent<Pool>();

        slider.value = 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        hpMax = hpCurrent = 100;


        shootPos = transform.position;

        //StartCoroutine(FirRoundGroup());

        //StartCoroutine(FireTurbine());
    }


    public void DoFirRoundGroup()
    {
        StartCoroutine(FirRoundGroup());
    }
    public void DoFireTurbine()
    {
        StartCoroutine(FireTurbine());
    }

    // Update is called once per frame
    void Update()
    {
        shootDir = transform.up;

        slider.value = hpCurrent / hpMax;

        if (Input.GetKeyDown(KeyCode.Z))
        {
            StartCoroutine(FireTurbine());

        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            StartCoroutine(FirRoundGroup());
        }
    }


    EnemyBullet CreateBullet(Vector3 bulletDir, Vector3 createPoint)
    {
        //GameObject go = Instantiate<GameObject>(bullet);

        GameObject go = pool.GetInstance();
        Bullet t = go.GetComponent<Bullet>();

        t.Dir = bulletDir;
        t.transform.position = createPoint;

        go.transform.SetParent(GameObject.Find("EnemyBullets").transform);

        return go.GetComponent<EnemyBullet>();
    }

    IEnumerator FirRound(int number, Vector3 createPoint)
    {
        Vector3 bulletDir;
        bulletDir = shootDir;
        Quaternion rotateQuate = Quaternion.AngleAxis(30, Vector3.forward);//使用四元数制造绕Z轴旋转10度的旋转
        for (int i = 0; i < number; i++)    //发射波数
        {
            for (int j = 0; j < 36; j++)
            {
                CreateBullet(bulletDir, createPoint);   //生成子弹
                bulletDir = rotateQuate * bulletDir; //让发射方向旋转10度，到达下一个发射方向
            }
            yield return new WaitForSeconds(0.5f); //协程延时，0.5秒进行下一波发射
        }

        yield return null;
    }


    IEnumerator FirRoundGroup()
    {
        Vector3 bulletDir = shootDir;
        Quaternion rotateQuate = Quaternion.AngleAxis(45, Vector3.forward);//使用四元数制造绕Z轴旋转45度的旋转
        List<EnemyBullet> bullets = new List<EnemyBullet>();       //装入开始生成的8个弹幕
        for (int i = 0; i < 8; i++)
        {
            var tempBullet = CreateBullet(bulletDir, shootPos);
            bulletDir = rotateQuate * bulletDir; //生成新的子弹后，让发射方向旋转45度，到达下一个发射方向
            bullets.Add(tempBullet);
        }
        yield return new WaitForSeconds(1.0f);   //1秒后在生成多波弹幕
        for (int i = 0; i < bullets.Count; i++)
        {
            bullets[i].GetComponent<Rigidbody2D>().velocity = Vector2.zero; //弹幕停止移动
            StartCoroutine(FirRound(2, bullets[i].transform.position));//通过之前弹幕的位置，生成多波多方向的圆形弹幕。这里调用了上面写过的圆形弹幕函数

        }


    }


    IEnumerator FireTurbine()
    {
        Vector3 bulletDir = shootDir;      //发射方向
        Quaternion rotateQuate = Quaternion.AngleAxis(30, Vector3.forward);//使用四元数制造绕Z轴旋转20度的旋转
        float radius = 0.6f;        //生成半径
        float distance = 0.2f;      //每生成一次增加的距离
        for (int i = 0; i < 18; i++)
        {
            Vector3 firePoint = shootPos + bulletDir * radius;   //使用向量计算生成位置
            StartCoroutine(FirRound(3, firePoint));     //在算好的位置生成一波圆形弹幕
            yield return new WaitForSeconds(0.05f);     //延时较小的时间（为了表现效果），计算下一步
            bulletDir = rotateQuate * bulletDir;        //发射方向改变
            radius += distance;     //生成半径增加
        }
    }





}
