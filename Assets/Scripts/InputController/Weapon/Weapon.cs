using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum WeaponType
{
    Petrol,
    UZI,
    Gun,
}


public class Weapon
{

    protected GameObject bullet;

    protected float shootInterval;
    protected float reloadTime;

    protected int level;

    protected int maxCapacity;
    protected int currentNum;


    public Weapon()
    {
        //bullet = (GameObject)Resources.Load("Prefabs/", typeof(GameObject));
    }

    public virtual void Shoot(Transform shootPos)
    {
        //GameObject go = GameObject.Instantiate(bullet, shootPos);
        //go.transform.SetParent(GameObject.Find("Bullets").transform);
    }


    public virtual void Upgrade()
    {
        //switch (level)
        //{
        //    case 1:

        //        break;

        //    default:
        //        break;
        //}
    }


}
