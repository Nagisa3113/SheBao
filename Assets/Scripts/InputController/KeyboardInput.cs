using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : InputHandle
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


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

        hl = Input.GetAxis("ShootHorizontal");
        vt = Input.GetAxis("ShootVertical");
        Dir = new Vector2(hl, vt).normalized;

        ShootDir = Dir * Mathf.Sqrt(hl * hl + vt * vt);

        return ShootDir;
    }


    public override bool GetShoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            return true;
        }

        return false;
    }



}
