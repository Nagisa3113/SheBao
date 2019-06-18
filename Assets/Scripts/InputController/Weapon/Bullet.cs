using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{


    float speed;
    float damage;
    Vector3 dir;

    // Use this for initialization
    void Start()
    {
        //transform.Rotate();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += dir * 0.1f;
    }
    

    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        Destroy(gameObject);
    }


}
