using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wingman : MonoBehaviour
{

    public GameObject player;


    public float shootCD = 0.1f;
    public float shootPosOffset = 3f;

    Vector3 dir;
    Vector3 pos;

    Quaternion rotate = Quaternion.AngleAxis(3, Vector3.forward);

    public Vector3 rotationDir;

    private void Start()
    {
        player = GameObject.Find("Player");
        InvokeRepeating("Shoot", 1, shootCD);
        //CancelInvoke();
    }

    private void Update()
    {
        //rotationDir = transform.position - player.transform.position;
        //rotationDir = rotate * rotationDir;
        //transform.position = player.transform.position + rotationDir;
        //transform.up = player.transform.up;
    }


    public void Shoot()
    {
        dir = transform.up;
        pos = gameObject.transform.position + dir.normalized * shootPosOffset;
        BulletController.Instance.CreateBullet(BulletType.Player, pos, dir);
    }
}
