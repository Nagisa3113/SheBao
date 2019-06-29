using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Role : MonoBehaviour
{

    [SerializeField]
    protected float moveSpeed;

    protected Vector3 moveDir;

    [SerializeField]
    protected int hp;
    public int HP
    {
        get
        {
            return hp;
        }
        set
        {
            hp = value;
        }
    }

}
