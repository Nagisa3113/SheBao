﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Role : MonoBehaviour
{

    [SerializeField]
    protected float moveSpeed;

    protected float drag;

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


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}