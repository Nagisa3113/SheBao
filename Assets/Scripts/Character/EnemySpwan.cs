using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "MyAsset/EnemySpwan")]
public class EnemySpwan : ScriptableObject
{
    [MenuItem("ScriptableObject/CreateEnemySpawn")]
    static void CreateEnemySpawn()
    {
        EnemySpwan enemySpwan = ScriptableObject.CreateInstance<EnemySpwan>();

        AssetDatabase.CreateAsset(enemySpwan, "Assets/EnemySpawn.asset");
        AssetDatabase.SaveAssets();
    }

    public GameObject[] enemys;

    public List<Spawn> spawns = new List<Spawn>();

    public int index;

}

[System.Serializable]
public class Spawn
{
    public Vector2 pos;
    public Vector2 dir;
    public float speed;
    public int type;

}