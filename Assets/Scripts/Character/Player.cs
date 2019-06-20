using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Role
{


    [SerializeField]
    float shootCD=0.5f;

    [SerializeField]
    [Header("Weapon")]
    List<Weapon> weaponList;
    Weapon currentWeapon;

    [SerializeField]
    InputHandle inputHandle;

    public bool shoot;

    //public AnimationCurve moveCurve;

    private void Awake()
    {
        moveSpeed = 4f;

        weaponList = new List<Weapon> { new Petrol() };
        currentWeapon = weaponList[0];
    }

    private void Start()
    {

        InvokeRepeating("Shoot",1,shootCD);
        //CancelInvoke();
    }


    void FixedUpdate()
    {
        currentWeapon.Update();

        InputUpdate();
        PhysicsUpdate();
    }


    public void Update()
    {
        SpriteUpdate();
    }


    public void Shoot()
    {

        Vector3 dir = transform.up;
        Vector3 pos = GameObject.Find("DirArrow").transform.position;
        currentWeapon.Shoot(dir, pos);
    }

    void PhysicsUpdate()
    {

        GetComponent<Rigidbody2D>().velocity = moveDir * moveSpeed;


        if (shootDir != Vector2.zero)
        {
            gameObject.transform.up = shootDir;
        }

        if (shoot)
        {
            Shoot();
        }

    }


    void InputUpdate()
    {
        if (inputHandle == null)
        {
            Debug.Log("No Input Device!");
            return;
        }

        moveDir = inputHandle.GetMoveDir();

        shootDir = inputHandle.GetShootDir();

        shoot = inputHandle.GetShoot();

    }


    void SpriteUpdate()
    {

    }


}
