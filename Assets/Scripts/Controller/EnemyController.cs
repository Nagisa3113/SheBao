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

    public int bossIndex = 0;

    public static List<Enemy> enemylist = new List<Enemy>();
    public static List<Boss> bosslist = new List<Boss>();


    void Awake()
    {

        enemy = Resources.Load<GameObject>("Prefabs/Role/Enemy");
        boss = Resources.Load<GameObject>("Prefabs/Role/Boss");

        canvas = GameObject.Find("Canvas");
        text = Resources.Load<GameObject>("Prefabs/Role/Text");

    }
    private void Start()
    {
        StartCoroutine(SpawnEnemy(2));
        StartCoroutine(SpawnBoss(10, 60));
        StartCoroutine(SpawnBoss(50, 70));
        StartCoroutine(SpawnBoss(80, 80));
        StartCoroutine(SpawnBoss(120, 100));
        StartCoroutine(SpawnBoss(160, 110));
        StartCoroutine(SpawnBoss(220, 120));
        StartCoroutine(SpawnBoss(260, 130));
        StartCoroutine(SpawnBoss(300, 180));

    }


    IEnumerator SpawnEnemy(float startTime)
    {
        int maxEnemyNum = 10;


        for (float t = 0; t < startTime; t += Time.deltaTime)
        {
            yield return 0;
        }

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

                yield return new WaitForSeconds(0.8f);
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
        CreateBoss(enemyNames[bossIndex++], hp);

    }




    public void CreateEnemy()
    {
        string name = "Director";
        Vector3 pos = new Vector3(Random.Range(-20, 20), Random.Range(15, 20), 0);
        GameObject e;
        e = Pool.Instance.RequestCacheGameObejct(enemy);
        e.transform.position = pos;
        e.transform.SetParent(this.gameObject.transform);

        enemylist.Add(e.GetComponent<Enemy>());

        GameObject t;
        t = Pool.Instance.RequestCacheGameObejct(text);
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
        t.transform.SetParent(GameObject.Find("EnemyTexts").transform);

        Text txt = t.GetComponent<Text>();
        txt.text = name;
        txt.fontSize = 50;

        e.transform.localScale = new Vector3(6f * txt.text.Length, 10, 1);

        e.GetComponent<Boss>().text = t.GetComponent<Text>();

    }


}
