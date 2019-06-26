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
            this.GetComponent<SpriteRenderer>().enabled = false;
            this.GetComponent<CircleCollider2D>().enabled = false;

            if (this.GetComponentInChildren<ParticleSystem>().isPlaying)
            {
                this.GetComponentInChildren<ParticleSystem>().Stop();
            }
            this.GetComponentInChildren<ParticleSystem>().Play();

            Pool.Instance.ReturnCacheGameObejct_Delay(this.gameObject, 0.5f);

        }
        else
        {
            base.OnCollisionEnter2D(collision);
        }

    }



}
