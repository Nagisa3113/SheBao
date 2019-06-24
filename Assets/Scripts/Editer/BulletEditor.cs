using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

public class BulletEditor : EditorWindow
{

    Bulletstruct[] bs = new Bulletstruct[10];

    [MenuItem("Editor/BulletEditor")]
    public static void CreateWindow()
    {
        EditorWindow window = EditorWindow.GetWindow(typeof(BulletEditor));
        window.Show();

    }

    void Awake()
    {

    }

    void OnGUI()
    {


        GUILayout.Space(10);
        GUI.skin.label.fontSize = 24;
        GUI.skin.label.alignment = TextAnchor.MiddleCenter;
        GUILayout.Label("弹幕编辑");
        GUI.skin.label.fontSize = 10;

        if (GUILayout.Button("保存"))
        {
            BulletSpawn bulletSpawn = AssetDatabase.LoadAssetAtPath<BulletSpawn>("Assets/BUlletSpawn.asset");
            bulletSpawn.bulletstructs = bs;
        }

        GUILayout.BeginHorizontal();
        GUILayout.Label("数量");
        GUILayout.Label("角度");
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        bs[0].num = EditorGUILayout.IntField("", bs[0].num);
        bs[0].rotation = EditorGUILayout.IntField("", bs[0].rotation);
        GUILayout.EndHorizontal();
        if (GUILayout.Button("敌人释放圆形弹幕"))
        {
            if (Application.isPlaying)
            {
                BulletController bc = BulletController.Instance;
                bc.StartShoot(bc.FirRoundGroup(bc.enemy.transform.position, bs[0].num, bs[0].rotation));
            }
        }


        GUILayout.BeginHorizontal();
        bs[1].num = EditorGUILayout.IntField("", bs[1].num);
        bs[1].rotation = EditorGUILayout.IntField("", bs[1].rotation);
        GUILayout.EndHorizontal();
        if (GUILayout.Button("敌人释放涡轮弹幕"))
        {
            if (Application.isPlaying)
            {
                BulletController bc = BulletController.Instance;
                bc.StartShoot(bc.FireTurbine(bc.enemy.transform.position, bs[1].num, bs[1].rotation));
            }
        }






    }


    void SaveAsset()
    {

    }


    void OnInspectorUpdate()
    {
        //Debug.Log("Update");
        //LoadAudioList();
    }

}
