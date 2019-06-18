using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Petrol : Weapon
{
    public Petrol()
    {
        bullet = (GameObject)Resources.Load("Prefabs/PetrolBullet", typeof(GameObject));

        shootInterval = 0.2f;
        reloadTime = 1f;
        level = 1;

        maxCapacity = 100;
        currentNum = 50;

    }

    public override void Shoot(Transform shootPos)
    {

        if (currentNum > 0)
        {
            GameObject go = GameObject.Instantiate(bullet, shootPos);
            go.transform.SetParent(GameObject.Find("Bullets").transform);

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
