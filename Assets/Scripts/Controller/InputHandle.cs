using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandle : Singleton<InputHandle>
{

    float hl, vl;
    float hr, vr;


    public Vector2 GetMoveDir()
    {
        hl = Input.GetAxis("PS4_L_Horizontal");
        vl = Input.GetAxis("PS4_L_Vertical");

        return new Vector2(hl, vl);
    }

    public Vector2 GetShootDir()
    {
        hr = Input.GetAxis("PS4_R_Horizontal");
        vr = Input.GetAxis("PS4_R_Vertical");

        //return new Vector2(hr, vr).normalized * Mathf.Sqrt(hr * hr + vr * vr);
        return new Vector2(hr, vr);
    }

}
