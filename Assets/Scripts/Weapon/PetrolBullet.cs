using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetrolBullet : Bullet
{

    // Start is called before the first frame update
    void Start()
    {
        speed = 10f;
        dir = transform.up;

        //GetComponent<Rigidbody2D>().AddForce(dir * speed, ForceMode2D.Impulse);
        //Destroy(gameObject, 4);

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += dir * speed * Time.fixedDeltaTime;
        //GetComponent<Rigidbody2D>().velocity = dir * speed * Time.fixedDeltaTime;
    }


    public override void OnCollisionEnter2D(Collision2D collision)
    {

        //base.OnCollisionEnter2D(collision);

        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Role>().HP-=5;
        }


        //Destroy(gameObject);
        GameObject.Find("PlayerPool").GetComponent<Pool>().ReturnInstance(this.gameObject);

    }


}
