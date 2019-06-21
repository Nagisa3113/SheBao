using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Role : MonoBehaviour
{

    [SerializeField]
    protected float moveSpeed;

    protected float drag;

    protected Vector2 moveDir;

    protected float hp;
    protected bool isAlive;
    public float re;

    [SerializeField]
    protected Vector2 shootDir;

    public void ApplyDamage(float damage)
    {
        if (damage < hp)
        {
            hp -= damage;
        }
        else
        {
            isAlive = false;
            hp = 0;
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
