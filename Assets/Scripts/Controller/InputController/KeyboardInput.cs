﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : InputHandle
{

    public override Vector2 GetMoveDir()
    {
        Vector2 MoveDir;
        Vector2 Dir;
        float hl;
        float vt;


        hl = Input.GetAxis("Horizontal");
        vt = Input.GetAxis("Vertical");
        Dir = new Vector2(hl, vt).normalized;


        MoveDir = Dir * Mathf.Sqrt(hl * hl + vt * vt);
        return MoveDir;
    }

    public override Vector2 GetShootDir()
    {
        Vector2 ShootDir;
        Vector2 Dir;

        float hl;
        float vt;

        hl = Input.GetAxis("Horizontal");
        vt = Input.GetAxis("Vertical");
        Dir = new Vector2(hl, vt).normalized;

        ShootDir = Dir * Mathf.Sqrt(hl * hl + vt * vt);

        return ShootDir;
    }


    public override bool GetShoot()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            return true;
        }

        return false;
    }



}