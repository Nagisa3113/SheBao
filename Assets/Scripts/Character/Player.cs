using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Role
{
    public GameObject bullet;
    public Pool pool;

    public Slider slider;

    [SerializeField]
    float shootCD = 0.5f;

    InputHandle inputHandle;

    //public AnimationCurve moveCurve;

    private void Awake()
    {
        inputHandle = InputHandle.Instance;
    
        bullet = (GameObject)Resources.Load("Prefabs/PlayerBullet", typeof(GameObject));

        pool = GameObject.Find("Pool").GetComponent<Pool>();

        slider = GetComponentInChildren<Slider>();

        hpCurrent = hpMax = 50;
    }

    private void Start()
    {

        InvokeRepeating("Shoot", 0, shootCD);
        //CancelInvoke();
    }


    void FixedUpdate()
    {
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

        Bullet.InitBullet(bullet, pos, dir);

        GetComponent<AudioSource>().Play();
    }

    void PhysicsUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = moveDir * moveSpeed;

        if (shootDir != Vector2.zero)
        {
            gameObject.transform.up = shootDir;
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
    }


    void SpriteUpdate()
    {
        //slider.value = hpCurrent / hpMax;
    }


}
