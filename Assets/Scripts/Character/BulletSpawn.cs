using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "MyAsset/BulletSpwan")]
public class BulletSpawn : ScriptableObject
{
    [MenuItem("ScriptableObject/CreateBulletSpawn")]
    static void CreateEnemySpawn()
    {
        BulletSpawn bulletSpawn = ScriptableObject.CreateInstance<BulletSpawn>();

        AssetDatabase.CreateAsset(bulletSpawn, "Assets/BulletSpawn.asset");
        AssetDatabase.SaveAssets();
    }

    public Bulletstruct[] bulletstructs;

    public int index;

}


[System.Serializable]
public struct Bulletstruct
{
    public int num;
    public int rotation;
}