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
    protected int maxBossNum = 1;

    protected int enemyNum;
    protected int bossNum;


    public static List<Enemy> enemylist = new List<Enemy>();
    public static List<Boss> bosslist = new List<Boss>();

    private void Update()
    {
        bossNum = bosslist.Count;
        if (bossNum < maxBossNum)
        {
            CreateBoss();
        }
    }

    void Awake()
    {

        enemy = Resources.Load<GameObject>("Prefabs/Role/Enemy");
        boss = Resources.Load<GameObject>("Prefabs/Role/Boss");

        canvas = GameObject.Find("Canvas");
        text = Resources.Load<GameObject>("Prefabs/Role/Text");
    }

    private void Start()
    {
        //InvokeRepeating("CreateEnemy", 3, 1f);
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


    public void CreateBoss()
    {
        string name = "Boss";
        Vector3 pos = new Vector3(Random.Range(-30, 30), Random.Range(15, 20), 0);

        GameObject e;
        e = Pool.Instance.RequestCacheGameObejct(boss);
        e.transform.position = pos;

        bosslist.Add(e.GetComponent<Boss>());


        GameObject t;
        t = Pool.Instance.RequestCacheGameObejct(text);
        t.transform.SetParent(canvas.transform);

        Text txt = t.GetComponent<Text>();
        txt.text = name;
        txt.fontSize = 60;

        e.transform.localScale = new Vector3(6f * txt.text.Length, 10, 1);

        e.GetComponent<Boss>().text = t.GetComponent<Text>();

    }


}
