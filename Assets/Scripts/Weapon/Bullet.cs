using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    protected float speed;

    [SerializeField]
    protected float damage;

    protected Vector3 dir;
    public Vector3 Dir
    {
        set
        {
            dir = value;
            transform.up = dir;
        }
    }



    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
    }


}
