using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    protected float speed;

    [SerializeField]
    protected float damage;


    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        Pool pool = GameObject.Find("Pool").GetComponent<Pool>();
        pool.ReturnCacheGameObejct(this.gameObject);
        //Debug.Log(collision.gameObject.name);
    }

    public static GameObject InitBullet(GameObject bullet, Vector3 pos,Vector3 dir)
    {
        Pool pool = GameObject.Find("Pool").GetComponent<Pool>();
        GameObject b = pool.RequestCacheGameObejct(bullet);
        b.transform.position = pos;
        b.transform.up = dir;
        b.transform.SetParent(GameObject.Find("Bullets").transform);

        return b;
    }
}
