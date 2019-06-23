using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyController : Singleton<EnemyController>
{

    public List<GameObject> enemys = new List<GameObject>();
    public List<Spawn> spawns = new List<Spawn>();

    int index;

    public EnemyController()
    {
        EnemySpwan enemySpwan = AssetDatabase.LoadAssetAtPath<EnemySpwan>("Assets/EnemySpawn.asset");
        for (int i = 0; i < enemySpwan.enemys.Length; i++)
        {
            enemys.Add(enemySpwan.enemys[i]);
            spawns.Add(enemySpwan.spawns[i]);
        }
    }



    public void InitEnemy()
    {

        float x = Random.Range(-10, 10);
        float y = Random.Range(-10, 10);

        Vector3 assetPos = new Vector3(spawns[index].pos.x + x, spawns[index].pos.y + y, 0);

        GameObject.Instantiate(enemys[index]);

        enemys[index].transform.position = assetPos;

        index++;
        if (index >= enemys.Count)
        {
            index = 0;
        }

    }
}
