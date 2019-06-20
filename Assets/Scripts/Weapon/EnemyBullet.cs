using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : Bullet
{

    // Start is called before the first frame update
    void Start()
    {
        speed = 23f;
        dir = transform.up;

        //GetComponent<Rigidbody2D>().AddForce(transform.up * speed, ForceMode2D.Force);
        //Destroy(gameObject, 4);
    }



    // Update is called once per frame
    void Update()
    {
        transform.position += dir * 0.05f;
        //GetComponent<Rigidbody2D>().velocity = dir * speed * Time.fixedDeltaTime;
    }


    public override void OnCollisionEnter2D(Collision2D collision)
    {
        //base.OnCollisionEnter2D(collision);


        //use pool
        GameObject.Find("EnemyPool").GetComponent<Pool>().ReturnInstance(this.gameObject);


    }
}
