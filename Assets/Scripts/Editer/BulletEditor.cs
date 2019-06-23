using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

public class BulletEditor : EditorWindow
{
    BulletController bc;
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
        if (GUILayout.Button("初始化"))
        {
            bc = BulletController.Instance;
            bc.Init();
        }

        if (GUILayout.Button("敌人释放圆形弹幕"))
        {
            bc.StartShoot(bc.FirRoundGroup(bc.enemy.transform.position, 2, 30));
        }
        if (GUILayout.Button("敌人释放涡轮弹幕"))
        {

            bc.StartShoot(bc.FireTurbine(bc.enemy.transform.position, 2, 30));
        }
    }



    void OnInspectorUpdate()
    {
        //Debug.Log("Update");
        //LoadAudioList();
    }

}
