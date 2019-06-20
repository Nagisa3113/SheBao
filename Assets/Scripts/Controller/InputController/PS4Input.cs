using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PS4Input : InputHandle
{
    public override Vector2 GetMoveDir()
    {
        Vector2 MoveDir;
        Vector2 Dir;
        float hl;
        float vt;


        hl = Input.GetAxis("PS4_L_Horizontal");
        vt = Input.GetAxis("PS4_L_Vertical");
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

        hl = Input.GetAxis("PS4_R_Horizontal");
        vt = Input.GetAxis("PS4_R_Vertical");
        Dir = new Vector2(hl, vt).normalized;

        ShootDir = Dir * Mathf.Sqrt(hl * hl + vt * vt);

        return ShootDir;
    }

    public override bool GetShoot()
    {
        return false;
        //throw new System.NotImplementedException();
    }

   
}
