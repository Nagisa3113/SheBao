using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Editer : EditorWindow
{
    [MenuItem("Manager/Manager")]
    static void CreateWindow()
    {
        //Rect rect = new Rect(400, 400, 300, 400);
        //AudioWindowEditor window = EditorWindow.GetWindowWithRect(typeof(AudioWindowEditor), rect) as AudioWindowEditor;
        EditorWindow window = EditorWindow.GetWindow(typeof(Editer));
        window.Show();
    }

    private string audioName;
    private string audioPath;
    private Dictionary<string, string> audioDict = new Dictionary<string, string>();

    void Awake()
    {
        //Debug.Log(EditorApplication.applicationPath);
        //Debug.Log(EditorApplication.applicationContentsPath);
        //Debug.Log(Application.dataPath);

    }
    void OnGUI()
    {
        //EditorGUILayout.TextField("输入文字1", text);
        //GUILayout.TextField("输入文字2");
        GUILayout.BeginHorizontal();
        GUILayout.Label("音效名称");
        GUILayout.Label("音效路径");
        GUILayout.Label("操作");
        GUILayout.EndHorizontal();
        foreach (string key in audioDict.Keys)
        {
            string value;
            audioDict.TryGetValue(key, out value);
            GUILayout.BeginHorizontal();
            GUILayout.Label(key);
            GUILayout.Label(value);
            if (GUILayout.Button("删除"))
            {
                audioDict.Remove(key);
                //SaveAudioList();
                return;
            }
            GUILayout.EndHorizontal();
        }

        audioName = EditorGUILayout.TextField("音效名字", audioName);
        audioPath = EditorGUILayout.TextField("音效路径", audioPath);
        if (GUILayout.Button("添加音效"))
        {
            object o = Resources.Load(audioPath);
            if (o == null)
            {
                Debug.LogWarning("音效不存在于" + audioPath + " 添加不成功");
                audioPath = "";
            }
            else
            {
                if (audioDict.ContainsKey(audioName))
                {
                    Debug.LogWarning("名字已经存在，请修改");
                }
                else
                {
                    audioDict.Add(audioName, audioPath);
                    //SaveAudioList();
                }
            }
        }


        if (GUILayout.Button("敌人释放圆形弹幕"))
        {
            Enemy enemy = GameObject.Find("Enemy").GetComponent<Enemy>();
            enemy.DoFirRoundGroup();
        }
        if (GUILayout.Button("敌人释放涡轮弹幕"))
        {
            Enemy enemy = GameObject.Find("Enemy").GetComponent<Enemy>();
            enemy.DoFireTurbine();
        }



    }

    //窗口面板被更新的时候调用
    void OnInspectorUpdate()
    {
        //Debug.Log("Update");
        //LoadAudioList();
    }



    //private void SaveAudioList()
    //{
    //    StringBuilder sb = new StringBuilder();

    //    foreach (string key in audioDict.Keys)
    //    {
    //        string value;
    //        audioDict.TryGetValue(key, out value);
    //        sb.Append(key + "," + value + "\n");
    //    }

    //    File.WriteAllText(AudioManager.AudioTextPath, sb.ToString());
    //    //File.AppendAllText(savePath, sb.ToString());
    //}
    //private void LoadAudioList()
    //{
    //    audioDict = new Dictionary<string, string>();
    //    if (File.Exists(AudioManager.AudioTextPath) == false) return;
    //    string[] lines = File.ReadAllLines(AudioManager.AudioTextPath);
    //    foreach (string line in lines)
    //    {
    //        if (string.IsNullOrEmpty(line)) continue;
    //        string[] keyvalue = line.Split(',');
    //        audioDict.Add(keyvalue[0], keyvalue[1]);
    //    }
    //}
}

public class AudioManager
{
    //static 静态  const是常量
    private static string audioTextPathPrefix = Application.dataPath + "\\Framework\\Resources\\";
    private const string audioTextPathMiddle = "audiolist";
    private const string audioTextPathPostfix = ".txt";

    public static string AudioTextPath
    {
        get
        {
            return audioTextPathPrefix + audioTextPathMiddle + audioTextPathPostfix;
        }
    }

    private Dictionary<string, AudioClip> audioClipDict = new Dictionary<string, AudioClip>();

    public bool isMute = false;

    //public AudioManager()
    //{
    //    LoadAudioClip();
    //}
    public void Init()
    {
        LoadAudioClip();
    }
    private void LoadAudioClip()
    {
        audioClipDict = new Dictionary<string, AudioClip>();
        TextAsset ta = Resources.Load<TextAsset>(audioTextPathMiddle);
        string[] lines = ta.text.Split('\n');
        foreach (string line in lines)
        {
            if (string.IsNullOrEmpty(line)) continue;
            string[] keyvalue = line.Split(',');
            string key = keyvalue[0];
            AudioClip value = Resources.Load<AudioClip>(keyvalue[1]);
            audioClipDict.Add(key, value);
        }
    }

    public void Play(string name)
    {
        if (isMute) return;
        AudioClip ac;
        audioClipDict.TryGetValue(name, out ac);
        if (ac != null)
        {
            AudioSource.PlayClipAtPoint(ac, Vector3.zero);
        }
    }
    public void Play(string name, Vector3 position)
    {
        if (isMute) return;
        AudioClip ac;
        audioClipDict.TryGetValue(name, out ac);
        if (ac != null)
        {
            AudioSource.PlayClipAtPoint(ac, position);
        }
    }
}
