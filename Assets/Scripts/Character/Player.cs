using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Role
{

    [SerializeField]
    [Header("Weapon")]
    List<Weapon> weaponList;
    Weapon currentWeapon;

    InputHandle inputHandle;

    public bool shoot;

    //public AnimationCurve moveCurve;

    private void Awake()
    {
        moveSpeed = 8f;

        hp = 20f;
        isAlive = true;

        inputHandle = GameObject.Find("Input").GetComponent<KeyboardInput>();

        weaponList = new List<Weapon> { new Petrol() };
        currentWeapon = weaponList[0];
    }

    private void Start()
    {
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


    void PhysicsUpdate()
    {

        GetComponent<Rigidbody2D>().velocity = moveDir * moveSpeed;


        if (shootDir != Vector2.zero)
        {
            gameObject.transform.up = shootDir;
        }

        if (shoot)
        {
            Transform shootTrans = GameObject.Find("DirArrow").transform;
            currentWeapon.Shoot(shootTrans);
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

    void AliveUpdate()
    {
        if(hp <= 0)
        {
            Destroy(gameObject);
        }
    }

}
