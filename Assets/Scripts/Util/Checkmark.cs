using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkmark : MonoBehaviour
{

    public GameObject T1;
    public GameObject T2;

    public Transform t1;
    public Transform t2;
    int opt = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("PS4_L_Vertical") > 0.5)
        {
            if (opt == 0)
            {
                opt = 1;
                this.gameObject.transform.SetParent(t1);
                this.transform.position = t1.transform.position;
                StartCoroutine(OptionMoveLeft(T1));
            }
            else if (opt == 1)
            {
            }
            else if (opt == 2)
            {
                opt = 1;
                this.gameObject.transform.SetParent(t1);
                this.transform.position = t1.transform.position;
                StartCoroutine(OptionMoveLeft(T1));
                StartCoroutine(OptionMoveRight(T2));
            }

        }
        if (Input.GetAxis("PS4_L_Vertical") < -0.5)
        {
            if (opt == 0)
            {
                opt = 2;
                this.gameObject.transform.SetParent(t2);
                this.transform.position = t2.transform.position;
                StartCoroutine(OptionMoveLeft(T2));
            }
            else if (opt == 1)
            {
                opt = 2;
                this.gameObject.transform.SetParent(t2);
                this.transform.position = t2.transform.position;
                StartCoroutine(OptionMoveLeft(T2));
                StartCoroutine(OptionMoveRight(T1));
            }
            else if (opt == 2)
            {

            }
        }


        if (Input.GetKey(KeyCode.JoystickButton2))
        {
            switch (opt)
            {
                case 1:
                    WingmanController.callWingman = false;
                    SceneManager.LoadScene(1);
                    break;
                case 2:
                    WingmanController.callWingman = true;
                    SceneManager.LoadScene(1);
                    //Application.Quit();
                    break;
                default:
                    break;
            }
        }
    }


    IEnumerator OptionMoveLeft(GameObject go)
    {
        for(float i = 0; i < 0.3f; i+=Time.deltaTime)
        {
            go.transform.position += new Vector3(-3, 0, 0);
            yield return 0;
        }
    }
    IEnumerator OptionMoveRight(GameObject go)
    {
        for (float i = 0; i < 0.3f; i += Time.deltaTime)
        {
            go.transform.position += new Vector3(3f, 0, 0);
            yield return 0;
        }
    }


}
