using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject player;
    public GameObject wingman;
    public bool callWingman;

    List<GameObject> wings = new List<GameObject>();

    float wingmanRaduis = 2f;

    Quaternion r1 = Quaternion.AngleAxis(60, Vector3.forward);
    Quaternion rotate = Quaternion.AngleAxis(3, Vector3.forward);

    Vector3 dir = Vector3.up;

    // Start is called before the first frame update
    void Start()
    {
        if (callWingman)
        {
            for (int i = 0; i < 6; i++)
            {
                GameObject go = Instantiate(wingman);
                wings.Add(go);
                go.transform.SetParent(player.transform);
                go.transform.position = player.transform.position + dir * wingmanRaduis;
                dir = r1 * dir;
            }
        }

    }

    private void Update()
    {

        if (callWingman)
        {
            foreach (GameObject go in wings)
            {
                go.transform.position = player.transform.position + dir * wingmanRaduis;
                go.transform.up = player.transform.up;
                dir = r1 * dir;

            }
            dir = rotate * dir;
        }

    }

}
