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
    public bool isArcMode;


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


    public IEnumerator BulletArc(float angle)
    {
        isArcMode = true;
        while (isArcMode)
        {
            Quaternion tempQuat = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.up = tempQuat * transform.up;
            transform.position += speed * transform.up * 0.1f/* * Time.deltaTime*/;
            yield return new WaitForSeconds(0.3f); //速度
        }
    }

}