using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Role
{
    InputHandle inputHandle;

    public float shootCD = 0.1f;
    public float shootPosOffset = 3f;

    private void Awake()
    {
        inputHandle = InputHandle.Instance;
        //bullet = (GameObject)Resources.Load("Prefabs/PlayerBullet", typeof(GameObject));
    }

    private void Start()
    {
        InvokeRepeating("Shoot", 1, shootCD);
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
        Vector3 pos = gameObject.transform.position + dir.normalized * shootPosOffset;

        BulletController.Instance.CreateBullet(BulletType.Player, pos, dir);

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


    void AudioUpdate()
    {

    }


    void InputUpdate()
    {
        if (inputHandle == null)
        {
            Debug.Log("No Input Device!");
            return;
        }


        if (Input.GetKeyDown(KeyCode.C))
        {
            GetDamage();
        }

        moveDir = inputHandle.GetMoveDir();
        shootDir = inputHandle.GetShootDir();
    }


    void SpriteUpdate()
    {

    }

    public void GetDamage()
    {
        hpCurrent--;
        switch (hpCurrent)
        {
            case 2:
                GameObject.Find("playerright").SetActive(false);
                break;
            case 1:
                GameObject.Find("playerleft").SetActive(false);
                break;
            case 0:
                break;

        }
    }



}
