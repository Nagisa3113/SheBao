using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : SingletonMonoBehavior<BulletController>
{

    public int opt = 0;
    public bulletInfo[] bulletInfos;

    GameObject playerBullet;
    GameObject enemyBulletYellow;
    GameObject enemyBulletRed;



    void Awake()
    {
        playerBullet = Resources.Load<GameObject>("Prefabs/Bullet/PlayerBullet");
        enemyBulletRed = Resources.Load<GameObject>("Prefabs/Bullet/EnemyBullet_Red");
        enemyBulletYellow = Resources.Load<GameObject>("Prefabs/Bullet/EnemyBullet_Yellow");
    }


    //随机弹幕
    public IEnumerator FireRandom(Enemy enemy)
    {
        Vector3 dir;
        Vector3 pos = enemy.transform.position;

        GameObject player = GameObject.Find("Player");
        float x1, y1;

        while (enemy.alive)
        {
            dir = player.transform.position - enemy.transform.position;
            x1 = Random.Range(-0.6f, 0.6f);
            y1 = Random.Range(-0.6f, 0.6f);
            dir.Normalize();
            dir.x += x1;
            dir.y += y1;
            dir.Normalize();

            int r = Random.Range(1, 3);
            CreateBullet((BulletType)r, pos, dir);

            yield return new WaitForSeconds(0.5f);
        }
    }

    //圆形弹幕
    public IEnumerator FireRound(Enemy enemy)
    {

        Vector3 offset = new Vector3(3, 0, 0);


        BulletType bulletType = BulletType.EnemyRed;
        Vector3 dir = enemy.transform.up;
        Vector3 pos;
        Quaternion rotateQuate = Quaternion.AngleAxis(45, Vector3.forward);//使用四元数制造绕Z轴旋转10度的旋转
        while (enemy.alive)
        {
            pos = enemy.transform.position;
            for (int j = 0; j < 8; j++)
            {

                CreateBullet(bulletType, pos + offset, dir);
                CreateBullet(bulletType, pos - offset, dir);

                ChangeEnemyBulletType(ref bulletType);
                dir = rotateQuate * dir;
            }
            yield return new WaitForSeconds(0.5f);
        }

        yield return null;
    }


    //圆弧形弹幕
    public IEnumerator FireArc(Enemy enemy)
    {

        Vector3 offset = new Vector3(3, 0, 0);


        BulletType bulletType = BulletType.EnemyRed;

        Vector3 dir = enemy.transform.up;
        Vector3 pos;
        Quaternion r1 = Quaternion.AngleAxis(15, Vector3.forward);
        Quaternion rotateQuate = Quaternion.AngleAxis(90, Vector3.forward);
        while (enemy.alive)
        {
            pos = enemy.transform.position;
            for (int j = 0; j < 4; j++)
            {
                CreateBullet(bulletType, pos + offset, dir);
                CreateBullet(bulletType, pos - offset, dir);

                ChangeEnemyBulletType(ref bulletType);
                dir = rotateQuate * dir;
            }
            yield return new WaitForSeconds(0.2f); //间隔

            dir = r1 * dir;
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


[System.Serializable]
public struct bulletInfo
{
    public Vector3 vector3;
    public int num;
    public int rotation;
}
