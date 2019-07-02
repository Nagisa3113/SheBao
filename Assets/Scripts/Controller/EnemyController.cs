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

    public string[] names;

    public int bossIndex = 0;

    public List<Enemy> enemylist;
    public List<Boss> bosslist;


    void Awake()
    {

        enemy = Resources.Load<GameObject>("Prefabs/Role/Enemy");
        boss = Resources.Load<GameObject>("Prefabs/Role/Boss");

        canvas = GameObject.Find("Canvas");
        text = Resources.Load<GameObject>("Prefabs/Role/Text");
    }
    private void Start()
    {
        enemylist = new List<Enemy>();
        bosslist = new List<Boss>();
    }



    private void OnEnable()
    {
        StartCoroutine(SpawnEnemy(2));
        StartCoroutine(SpawnBoss(10, 60));
        StartCoroutine(SpawnBoss(37, 80));
        StartCoroutine(SpawnBoss(70, 100));
        StartCoroutine(SpawnBoss(100, 120));
        StartCoroutine(SpawnBoss(140, 120));
        StartCoroutine(SpawnBoss(180, 120));
        StartCoroutine(SpawnBoss(230, 150));
        StartCoroutine(SpawnBoss(280, 200));

    }


    IEnumerator SpawnEnemy(float startTime)
    {
        int maxEnemyNum = 10;


        for (float t = 0; t < startTime; t += Time.deltaTime)
        {
            yield return 0;
        }

        CreateEnemy();
        CreateEnemy();
        CreateEnemy();

        CreateEnemy();
        yield return new WaitForSeconds(0.5f);

        CreateEnemy();
        yield return new WaitForSeconds(0.5f);
        CreateEnemy();
        yield return new WaitForSeconds(0.5f);

        while (true)
        {

            if (bosslist.Count > 0)
            {
                for (int i = 0; i < bossIndex + 3; i++)
                {
                    CreateEnemy();
                    yield return new WaitForSeconds(1.5f);

                }

                while (bosslist.Count > 0)
                {
                    yield return 0;
                }
            }

            if (enemylist.Count < maxEnemyNum)
            {
                CreateEnemy();

                yield return new WaitForSeconds(0.6f);
            }

        }
    }

    IEnumerator SpawnBoss(float startTime, int hp)
    {

        while (bosslist.Count > 0)
        {
            yield return 0;
        }
        for (float t = 0; t < startTime; t += Time.deltaTime)
        {
            yield return 0;
        }
        CreateBoss(enemyNames[bossIndex], hp);
        if(bossIndex<enemyNames.Length-1)
        {
            bossIndex++;
        }
    }




    public void CreateEnemy()
    {
        int r = Random.Range(0, names.Length);
        string name = names[r];
        Vector3 pos = new Vector3(Random.Range(-20, 20), Random.Range(15, 20), 0);
        GameObject e;
        e = Pool.Instance.RequestCacheGameObejct(enemy);
        e.transform.position = pos;
        e.transform.SetParent(this.gameObject.transform);

        enemylist.Add(e.GetComponent<Enemy>());

        GameObject t;
        t = Pool.Instance.RequestCacheGameObejct(text);
        e.GetComponent<Enemy>().text = t.GetComponent<Text>();
        t.transform.SetParent(GameObject.Find("EnemyTexts").transform);

        Text txt = t.GetComponent<Text>();
        txt.text = name;
        txt.fontSize = 35;
        e.transform.localScale = new Vector3(3f * txt.text.Length, 7, 1);

        e.GetComponent<Enemy>().text = t.GetComponent<Text>();

    }


    public void CreateBoss(string name, int hp)
    {
        Vector3 pos = new Vector3(Random.Range(-30, 30), Random.Range(15, 20), 0);

        GameObject e;
        e = Pool.Instance.RequestCacheGameObejct(boss);
        e.transform.position = pos;
        e.transform.SetParent(this.gameObject.transform);

        e.GetComponent<Boss>().HP = hp;

        bosslist.Add(e.GetComponent<Boss>());


        GameObject t;
        t = Pool.Instance.RequestCacheGameObejct(text);
        e.GetComponent<Boss>().text = t.GetComponent<Text>();
        t.transform.SetParent(GameObject.Find("EnemyTexts").transform);

        Text txt = t.GetComponent<Text>();
        txt.text = name;
        txt.fontSize = 50;

        e.transform.localScale = new Vector3(6f * txt.text.Length, 10, 1);


    }


}
