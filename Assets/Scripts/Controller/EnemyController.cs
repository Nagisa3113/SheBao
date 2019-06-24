using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyController : Singleton<EnemyController>
{

    public List<GameObject> enemys = new List<GameObject>();

    int index;

    public EnemyController()
    {
        EnemySpwan enemySpwan = AssetDatabase.LoadAssetAtPath<EnemySpwan>("Assets/EnemySpawn.asset");

    }



    public void InitEnemy()
    {

        float x = Random.Range(-10, 10);
        float y = Random.Range(-10, 10);

        GameObject.Instantiate(enemys[index]);

        index++;
        if (index >= enemys.Count)
        {
            index = 0;
        }

    }
}
