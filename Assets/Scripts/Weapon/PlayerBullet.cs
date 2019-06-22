using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : Bullet
{

    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<Rigidbody2D>().AddForce(dir * speed, ForceMode2D.Impulse);
        //Destroy(gameObject, 4);

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * speed * Time.fixedDeltaTime;
        //GetComponent<Rigidbody2D>().velocity = dir * speed * Time.fixedDeltaTime;
    }


    public override void OnCollisionEnter2D(Collision2D collision)
    {

        base.OnCollisionEnter2D(collision);
    }


}
