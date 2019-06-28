using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandle : Singleton<InputHandle>
{
    bool keyboard;

    float hl, vl;
    float hr, vr;


    public Vector2 GetMoveDir()
    {

        hl = Input.GetAxis("Horizontal") + Input.GetAxis("PS4_L_Horizontal");
        vl = Input.GetAxis("Vertical") + Input.GetAxis("PS4_L_Vertical");

        return new Vector2(hl, vl);
    }

    public Vector2 GetShootDir()
    {
        hr = Input.GetAxis("Horizontal") + Input.GetAxis("PS4_R_Horizontal");
        vr = Input.GetAxis("Vertical") + Input.GetAxis("PS4_R_Vertical");

        return new Vector2(hr, vr);
    }

}
