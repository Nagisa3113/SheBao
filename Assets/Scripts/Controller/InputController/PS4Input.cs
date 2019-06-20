using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PS4Input : InputHandle
{
    public override Vector2 GetMoveDir()
    {
        //throw new System.NotImplementedException();
        return new Vector2(0,0);
    }

    public override bool GetShoot()
    {
        throw new System.NotImplementedException();
    }

    public override Vector2 GetShootDir()
    {
        throw new System.NotImplementedException();
    }
}
