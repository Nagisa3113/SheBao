using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Role : MonoBehaviour
{

    [SerializeField]
    protected float moveSpeed;

    protected Vector2 moveDir;

    [SerializeField]
    protected Vector2 shootDir;

    [SerializeField]
    protected float hpMax;

    [SerializeField]
    protected float hpCurrent;
    public float HP
    {
        get
        {
            return hpCurrent;
        }
        set
        {
            hpCurrent = value;
        }
    }

}
