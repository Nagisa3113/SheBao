using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : Bullet
{

    public override void FixedUpdate()
    {
        base.FixedUpdate();

    }

    public override void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.layer == LayerMask.NameToLayer("PlayerBullet")
            && this.type == BulletType.EnemyYellow)
        {
            //ParticleController.Instance.CreateEnemyhit(transform.position);
            ParticleController.Instance.CreateBulletExplosion(transform.position);
        }

        base.OnCollisionEnter2D(collision);



    }



}
