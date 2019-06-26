using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : Bullet
{

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }


    public override void OnCollisionEnter2D(Collision2D collision)
    {

        base.OnCollisionEnter2D(collision);
    }


}
