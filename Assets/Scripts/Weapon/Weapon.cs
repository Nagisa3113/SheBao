using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum WeaponType
{
    Petrol,
    UZI,
    Gun,
}

[System.Serializable]
public class Weapon
{
    [SerializeField]
    protected Pool pool;

    protected GameObject bullet;

    [SerializeField]
    protected float shootInterval;


    protected float reloadTime;

    protected int level;

    protected int maxCapacity;
    protected int currentNum;

    protected float coolDownTime;


    public Weapon()
    {
        //bullet = (GameObject)Resources.Load("Prefabs/", typeof(GameObject));
    }

    public virtual void Shoot(Transform transform)
    {
        //GameObject go = GameObject.Instantiate(bullet, shootPos);
        //go.transform.SetParent(GameObject.Find("Bullets").transform);
    }

    public virtual void Shoot()
    {
        //GameObject go = GameObject.Instantiate(bullet, shootPos);
        //go.transform.SetParent(GameObject.Find("Bullets").transform);
    }



    public virtual void Update()
    {

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
