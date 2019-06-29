using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : SingletonMonoBehavior<BulletController>
{
    GameObject playerBullet;
    GameObject enemyBulletYellow;
    GameObject enemyBulletRed;

    public float randomCD = 0.4f;
    public float roundCD = 0.4f;
    public float arcCD = 0.4f;

    void Awake()
    {
        playerBullet = Resources.Load<GameObject>("Prefabs/Bullet/PlayerBullet");
        enemyBulletRed = Resources.Load<GameObject>("Prefabs/Bullet/EnemyBullet_Red");
        enemyBulletYellow = Resources.Load<GameObject>("Prefabs/Bullet/EnemyBullet_Yellow");
    }


    //Shoot at random direction
    public IEnumerator FireRandom(Role enemy)
    {
        Vector3 dir;
        float r;
        int t;
        GameObject player = GameObject.Find("Player");

        while (true)
        {
            r = Random.Range(-60, 60);
            Quaternion rotateQuate = Quaternion.AngleAxis(r, Vector3.forward);
            dir = player.transform.position - enemy.transform.position;
            dir = rotateQuate * dir;

            t = Random.Range(1, 3);
            CreateBullet((BulletType)t, enemy.transform.position, dir);

            yield return new WaitForSeconds(randomCD);
        }
    }

    //shoot round
    public IEnumerator FireRound(Role enemy)
    {
        Vector3 offset = new Vector3(4, 0, 0);
        BulletType bulletType = BulletType.EnemyRed;
        Vector3 dir = Vector3.up;
        Vector3 pos;
        Quaternion rotateQuate = Quaternion.AngleAxis(60, Vector3.forward);
        while (true)
        {
            pos = enemy.transform.position;
            Vector3 p = pos - offset;
            for (int i = 0; i < 3; i++)
            {

                for (int j = 0; j < 6; j++)
                {
                    CreateBullet(bulletType, p, dir);
                    ChangeEnemyBulletType(ref bulletType);
                    dir = rotateQuate * dir;
                }
                p += offset;

                Quaternion r1 = Quaternion.AngleAxis(Random.Range(-30, 30), Vector3.forward);
                dir = r1 * dir;
            }

            yield return new WaitForSeconds(roundCD);

        }
    }

    //shoot round
    public IEnumerator FireArc(Role enemy)
    {
        Vector3 offset = new Vector3(4, 0, 0);
        BulletType bulletType = BulletType.EnemyRed;
        Vector3 dir = Vector3.up;
        Vector3 pos;
        Quaternion r1 = Quaternion.AngleAxis(15, Vector3.forward);
        Quaternion rotateQuate = Quaternion.AngleAxis(60, Vector3.forward);

        while (true)
        {
            pos = enemy.transform.position;
            Vector3 p = pos - offset;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    CreateBullet(bulletType, p, dir);
                    ChangeEnemyBulletType(ref bulletType);
                    dir = rotateQuate * dir;
                }

                ChangeEnemyBulletType(ref bulletType);

                p += offset;
            }

            dir = r1 * dir;

            yield return new WaitForSeconds(arcCD);

        }
    }


    void ChangeEnemyBulletType(ref BulletType bulletType)
    {
        if (bulletType == BulletType.EnemyRed)
        {
            bulletType = BulletType.EnemyYellow;
        }
        else
        {
            bulletType = BulletType.EnemyRed;
        }
    }


    public Bullet CreateBullet(BulletType type, Vector3 pos, Vector3 dir)
    {
        GameObject b;
        switch (type)
        {
            case BulletType.Player:
                b = Pool.Instance.RequestCacheGameObejct(playerBullet);
                break;
            case BulletType.EnemyRed:
                b = Pool.Instance.RequestCacheGameObejct(enemyBulletRed);
                break;
            case BulletType.EnemyYellow:
                b = Pool.Instance.RequestCacheGameObejct(enemyBulletYellow);
                break;

            default:
                Debug.Log("Bullet Type Error!");
                return null;
        }

        b.transform.SetParent(this.transform);
        b.transform.position = pos;
        b.transform.up = dir;

        return b.GetComponent<Bullet>();
    }
}
