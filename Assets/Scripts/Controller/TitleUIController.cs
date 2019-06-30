using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleUIController : MonoBehaviour
{
    public Color c1;
    public Color c2;
    public GameObject t1;
    public GameObject t2;

    // Start is called before the first frame update
    void Awake()
    {
        t1.GetComponent<Text>().color = Color.clear;
        t2.GetComponent<Text>().color = Color.clear;
    }

    // Update is called once per frame
    void Start()
    {
        StartCoroutine(ColorGradient(t1, 1, 8, 1));
        StartCoroutine(ColorGradient(t2, 3, 8, 1));
        StartCoroutine(NextScence(8));
    }


    /// <summary>
    /// Colors the gradient.
    /// </summary>
    /// <returns>The gradient.</returns>
    /// <param name="go">Go.</param>
    /// <param name="startTime">Start time.</param>
    /// <param name="endTime">End time.</param>
    /// <param name="gradientTime">Gradient time.</param>
    IEnumerator ColorGradient(GameObject go, float startTime, float endTime, float gradientTime)
    {
        Text text = go.GetComponent<Text>();
        for (float t = 0; t < endTime; t += Time.deltaTime)
        {
            if (t > startTime && t < startTime + gradientTime)
            {
                text.color = Color.Lerp(c1, c2, Mathf.Clamp01((t - startTime) / gradientTime));
            }

            if (t > endTime - gradientTime)
            {
                text.color = Color.Lerp(c1, c2, Mathf.Clamp01((endTime - t) / gradientTime));
            }

            yield return 0;
        }

        Destroy(go);
    }

    IEnumerator NextScence(float endTime)
    {
        for (float t = 0; t < endTime; t += Time.deltaTime)
        {
            yield return 0;
        }
        SceneManager.LoadScene(1);
    }



}
