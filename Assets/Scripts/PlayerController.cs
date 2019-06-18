using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Role
{

    [SerializeField]
    [Header("Weapon")]
    List<Weapon> weaponList;
    Weapon currentWeapon;

    [Header("Controller")]
    //bool IsControlled = false;

    InputHandle inputHandle;


    [SerializeField]
    Vector3 t;


    public bool shoot;

    //public AnimationCurve moveCurve;


    private void Awake()
    {
        moveSpeed = 8f;

        inputHandle = GameObject.Find("Input").GetComponent<KeyboardInput>();

        weaponList = new List<Weapon> { new Petrol() };
        currentWeapon = weaponList[0];
    }

    private void Start()
    {
    }


    void FixedUpdate()
    {
        InputUpdate();
        PhysicsUpdate();
    }


    public void Update()
    {
        t = transform.up;
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
            Transform shootPos = GameObject.Find("DirArrow").GetComponent<Transform>();
            currentWeapon.Shoot(shootPos);
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
