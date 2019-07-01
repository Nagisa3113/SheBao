using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Background : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AlphaGradient(0, 1f));
    }



    public void NextScene(int num)
    {
        StartCoroutine(AlphaGradient(1, 1f));
        StartCoroutine(Next(num));
    }


    IEnumerator Next(int num)
    {
        yield return new WaitForSeconds(2f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(num);
    }


    IEnumerator AlphaGradient(int n, float time)
    {
        if (n == 0)
        {
            for (float i = 0; i < time; i += Time.deltaTime)
            {
                GetComponent<Image>().color = Color.Lerp(Color.black, Color.clear, i / time);
                yield return 0;
            }
        }
        else
        {
            for (float i = 0; i < time; i += Time.deltaTime)
            {
                GetComponent<Image>().color = Color.Lerp(Color.clear, Color.black, i / time);
                yield return 0;
            }
        }



    }


}
