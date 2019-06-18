using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetrolBullet : Bullet
{
    private Vector3 shootDir;

    // Start is called before the first frame update
    void Start()
    {
        shootDir = GameObject.Find("Player").transform.up;
        transform.up = GameObject.Find("Player").transform.up;
        Destroy(gameObject, 4);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += shootDir * 0.1f;
    }



    public override void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        Destroy(gameObject);
    }



}
