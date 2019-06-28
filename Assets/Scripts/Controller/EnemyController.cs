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

    void Awake()
    {
        enemy = Resources.Load<GameObject>("Prefabs/Role/Enemy");
        boss = Resources.Load<GameObject>("Prefabs/Role/Boss");

        canvas = GameObject.Find("Canvas");
        text = Resources.Load<GameObject>("Prefabs/Role/Text");
    }

    private void Start()
    {
        InvokeRepeating("CreateEnemy", 1, 0.5f);
    }

    public void CreateEnemy()
    {
        string name = "Director";
        Vector3 pos = new Vector3(Random.Range(-5, 5), Random.Range(0, 5), 0);
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
