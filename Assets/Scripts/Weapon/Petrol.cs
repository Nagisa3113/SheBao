using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Petrol : Weapon
{





    public Petrol()
    {
        bullet = (GameObject)Resources.Load("Prefabs/PetrolBullet", typeof(GameObject));


        shootInterval = 0.5f;
        reloadTime = 1f;
        level = 1;

        maxCapacity = 100;
        currentNum = 50;

        coolDownTime = 0;

        pool = GameObject.Find("PlayerPool").GetComponent<Pool>();
    }


    public override void Update()
    {
        if (coolDownTime > 0)
        {
            coolDownTime -= Time.fixedDeltaTime;
        }
    }


    public override void Shoot(Vector3 shootDir,Vector3 shootPos)
    {

        if (currentNum > 0 & coolDownTime <= 0)
        {

            //GameObject go = GameObject.Instantiate(bullet);

            GameObject go = pool.GetInstance();

            Bullet t = go.GetComponent<Bullet>();

            t.transform.position = shootPos;
            t.Dir = shootDir;

            go.transform.SetParent(GameObject.Find("Bullets").transform);

            coolDownTime = shootInterval;

            currentNum--;
        }


    }


    public override void Upgrade()
    {
        switch (level)
        {
            case 1:

                break;

            default:
                break;
        }
    }


}
