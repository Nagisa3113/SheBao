using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class UIController : MonoBehaviour
{

    public GameObject playerSlider;
    public GameObject player;


    public GameObject enemySlider;
    public GameObject enemy;


    [SerializeField]
    Vector3 offset = new Vector3(0, 10, 0);

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        Vector3 ScreenPos;

        ScreenPos = Camera.main.WorldToScreenPoint(player.transform.position);
        playerSlider.transform.position = ScreenPos + offset;

        ScreenPos = Camera.main.WorldToScreenPoint(enemy.transform.position);
        enemySlider.transform.position = ScreenPos + offset;
    }
}
