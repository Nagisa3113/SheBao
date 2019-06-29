using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : SingletonMonoBehavior<EnemyController>
{
    GameObject enemy;
    GameObject boss;

    GameObject canvas;
    GameObject text;

    public string[] enemyNames;


    protected int maxEnemyNum = 8;
    protected int maxBossNum = 3;

    protected int enemyNum;
    protected int bossNum;

    public static Vector3[] point = new Vector3[10];
    public static Vector3[] bossPoint = new Vector3[3];

    public static List<Enemy> enemylist = new List<Enemy>();
    public static List<Boss> bosslist = new List<Boss>();


    void SetPoints()
    {
        point[0] = new Vector3(-20, 16, 0);
        point[1] = new Vector3(20, 16, 0);
        point[2] = new Vector3(-30, 12, 0);
        point[3] = new Vector3(-10, 14, 0);
        point[4] = new Vector3(7, 9, 0);
        point[5] = new Vector3(26, 10, 0);
        point[6] = new Vector3(-27, 4, 0);
        point[7] = new Vector3(-14, -2, 0);
        point[8] = new Vector3(11, 0, 0);
        point[9] = new Vector3(31, 2, 0);

        bossPoint[0] = new Vector3(-25, 7, 0);
        bossPoint[1] = new Vector3(2, 3, 0);
        bossPoint[2] = new Vector3(20, 6, 0);
    }


    void Awake()
    {
        SetPoints();

        enemy = Resources.Load<GameObject>("Prefabs/Role/Enemy");
        boss = Resources.Load<GameObject>("Prefabs/Role/Boss");

        canvas = GameObject.Find("Canvas");
        text = Resources.Load<GameObject>("Prefabs/Role/Text");
    }

    private void Start()
    {
        InvokeRepeating("CreateEnemy", 3, 1f);
    }

    public void CreateEnemy()
    {
        string name = "Director";
        Vector3 pos = new Vector3(Random.Range(-5, 5), Random.Range(15, 20), 0);
        GameObject e;
        e = Pool.Instance.RequestCacheGameObejct(enemy);
        e.transform.position = pos;

        GameObject t;
        t = Pool.Instance.RequestCacheGameObejct(text);
        t.transform.SetParent(canvas.transform);

        Text txt = t.GetComponent<Text>();
        txt.text = name;
        txt.fontSize = 35;
        e.transform.localScale = new Vector3(3f * txt.text.Length, 7, 1);

        e.GetComponent<Enemy>().text = t.GetComponent<Text>();

    }


    public void CreateBoss(string name, Vector3 pos)
    {
        GameObject e;
        e = Pool.Instance.RequestCacheGameObejct(boss);
        e.transform.position = pos;

        GameObject t;
        t = Pool.Instance.RequestCacheGameObejct(text);
        t.transform.SetParent(canvas.transform);

        Text txt = t.GetComponent<Text>();
        txt.text = name;
        txt.fontSize = 60;

        e.transform.localScale = new Vector3(6f * txt.text.Length, 10, 1);

        e.GetComponent<Enemy>().text = t.GetComponent<Text>();

    }


}
