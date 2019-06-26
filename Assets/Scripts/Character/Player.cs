using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Role
{
    InputHandle inputHandle;

    public float shootCD = 0.1f;
    public float shootPosOffset = 3f;

    public GameObject partMove;
    public GameObject partDie;

    private void Awake()
    {
        inputHandle = InputHandle.Instance;
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
            partDie.GetComponent<ParticleSystem>().Play();
            //Destroy(gameObject, 0.8f);
        }

        moveDir = inputHandle.GetMoveDir();
        shootDir = inputHandle.GetShootDir();
    }


    void SpriteUpdate()
    {
        if (GetComponent<Rigidbody2D>().velocity.sqrMagnitude > 50)
        {
            if(!partMove.gameObject.GetComponent<ParticleSystem>().isPlaying)
                partMove.gameObject.GetComponent<ParticleSystem>().Play();
        }
        else
        {
            if (partMove.gameObject.GetComponent<ParticleSystem>().isPlaying)
                partMove.gameObject.GetComponent<ParticleSystem>().Stop();
        }

    }


}
