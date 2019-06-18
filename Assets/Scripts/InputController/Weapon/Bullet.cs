using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{


    protected float speed;
    protected float damage;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    

    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        Destroy(gameObject);
    }


}
