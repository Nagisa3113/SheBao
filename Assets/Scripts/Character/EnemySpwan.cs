using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyAsset/EnemySpwan")]
public class EnemySpwan : ScriptableObject
{


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