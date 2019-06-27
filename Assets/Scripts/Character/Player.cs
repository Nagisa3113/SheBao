using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Role
{
    InputHandle inputHandle;

    public Sprite[] sprites;
    public SpriteRenderer subSprite;

    public float shootCD = 0.1f;
    public float shootPosOffset = 3f;
    public int audioCD = 14;
    bool isAudio;

    public int movespeedlow;

    public GameObject partMove;
    public GameObject partDie;

    private void Awake()
    {
        GetComponent<SpriteRenderer>().sprite = sprites[2];
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

        if (!isAudio)
            StartCoroutine(AudioPlay());

    }

    void PhysicsUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = moveDir * moveSpeed;

        if (shootDir.sqrMagnitude > 0.5)
        {
            gameObject.transform.up = shootDir;
        }

    }

    IEnumerator AudioPlay()
    {
        isAudio = true;
        GetComponent<AudioSource>().Play();
        for (int i = 0; i < audioCD; i++)
        {
            yield return null;
        }
        isAudio = false;

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
        }

        moveDir = inputHandle.GetMoveDir();
        shootDir = inputHandle.GetShootDir();
    }


    void SpriteUpdate()
    {
        if (GetComponent<Rigidbody2D>().velocity.sqrMagnitude > movespeedlow)
        {
            if (!partMove.gameObject.GetComponent<ParticleSystem>().isPlaying)
                partMove.gameObject.GetComponent<ParticleSystem>().Play();
        }
        else
        {
            if (partMove.gameObject.GetComponent<ParticleSystem>().isPlaying)
                partMove.gameObject.GetComponent<ParticleSystem>().Stop();
        }

    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("EnemyBulletRed")
            || collision.gameObject.layer == LayerMask.NameToLayer("EnemyBulletYellow"))
        {
            hp--;
            partDie.gameObject.GetComponent<ParticleSystem>().Play();
            if (hp < 0)
            {
                Destroy(gameObject, 0.5f);

            }
            else if (hp < 3)
            {
                GetComponent<SpriteRenderer>().sprite = sprites[hp];
                subSprite.sprite = sprites[hp];
            }
        }
    }








}
