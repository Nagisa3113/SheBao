using UnityEngine;
using System.Collections;

public enum BulletType
{
    Player,
    EnemyRed,
    EnemyYellow,
}


public class Bullet : MonoBehaviour
{
    [SerializeField]
    protected BulletType type;
    public BulletType Type
    {
        get
        {
            return type;
        }
    }

    [SerializeField]
    protected float speed;
    public float Speed
    {
        set
        {
            speed = value;
        }
    }

    [SerializeField]
    protected Vector3 dir;
    public Vector3 Dir
    {
        set
        {
            dir = value;
        }
    }


    public virtual void FixedUpdate()
    {
        dir = transform.up;
        transform.position += dir * speed * Time.fixedDeltaTime;
    }


    public virtual void OnCollisionEnter2D(Collision2D collision)
    {

        Pool.Instance.ReturnCacheGameObejct(this.gameObject);
        //Debug.Log(collision.gameObject.name);
    }

}