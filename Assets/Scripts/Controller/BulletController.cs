using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : SingletonSerializedMonoBehavior<BulletController>
{

    public int opt = 0;
    public bulletInfo[] bulletInfos;

    public GameObject enemy;

    GameObject playerBullet;
    GameObject enemyBulletYellow;
    GameObject enemyBulletRed;

    Vector3 shootDir;

    Pool pool;


    public delegate IEnumerator IEDelegate(Vector3 vector3, int num, int rotation);

    void Awake()
    {
        pool = Pool.Instance;
        playerBullet = Resources.Load<GameObject>("Prefabs/Bullet/PlayerBullet");
        enemyBulletRed = Resources.Load<GameObject>("Prefabs/Bullet/EnemyBullet_Red");
        enemyBulletYellow = Resources.Load<GameObject>("Prefabs/Bullet/EnemyBullet_Yellow");

        shootDir = enemy.transform.up;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartShoot();
        }
    }


    public void StartShoot()
    {
        IEDelegate iEDelegate = null;
        switch (opt)
        {
            case 0:
                iEDelegate = new IEDelegate(FirRound);
                break;
            case 1:
                iEDelegate = new IEDelegate(FirRoundGroup);
                break;
            case 2:
                iEDelegate = new IEDelegate(FireTurbine);
                break;
            case 3:
                iEDelegate = new IEDelegate(FireShot);
                break;
            case 4:
                iEDelegate = new IEDelegate(FireTrack);
                break;

            case 5:
                iEDelegate = new IEDelegate(FireBall);
                break;
            case 6:
                iEDelegate = new IEDelegate(FireArc);
                break;

            default:
                break;
        }
        enemy.GetComponent<Enemy>().StartCoroutine(
            iEDelegate(bulletInfos[opt].vector3, bulletInfos[opt].num, bulletInfos[opt].rotation)
            );
    }

    public void StartShoot(IEnumerator enumerator)
    {
        enemy.GetComponent<Enemy>().StartCoroutine(enumerator);
    }


    /// <summary>
    /// 向四周发射
    /// </summary>
    /// <param name="pos">生成位置</param>
    /// <param name="rotation">旋转角度</param>
    public IEnumerator FirRound(Vector3 pos, int number, int rotation)
    {
        Vector3 dir = shootDir;
        Quaternion rotateQuate = Quaternion.AngleAxis(rotation, Vector3.forward);//使用四元数制造绕Z轴旋转10度的旋转
        for (int i = 0; i < number; i++)    //发射波数
        {
            for (int j = 0; j < 360; j += rotation)
            {
                CreateBullet(BulletType.EnemyYellow, pos, dir);   //生成子弹
                dir = rotateQuate * dir; //让发射方向旋转10度，到达下一个发射方向
            }
            yield return new WaitForSeconds(0.5f); //协程延时，0.5秒进行下一波发射
        }

        yield return null;
    }

    public IEnumerator FirRoundGroup(Vector3 pos, int num, int rotation)
    {
        Vector3 dir = shootDir;
        Quaternion rotateQuate = Quaternion.AngleAxis(rotation, Vector3.forward);//使用四元数制造绕Z轴旋转45度的旋转
        List<Bullet> bullets = new List<Bullet>();       //装入开始生成的8个弹幕
        for (int i = 0; i < 360; i += rotation)
        {
            var tempBullet = CreateBullet(BulletType.EnemyRed, pos, dir);
            dir = rotateQuate * dir; //生成新的子弹后，让发射方向旋转45度，到达下一个发射方向
            bullets.Add(tempBullet);
        }
        yield return new WaitForSeconds(1.0f);   //1秒后在生成多波弹幕
        for (int i = 0; i < bullets.Count; i++)
        {
            bullets[i].GetComponent<Rigidbody2D>().velocity = Vector2.zero; //弹幕停止移动
            StartShoot(FirRound(bullets[i].transform.position, num, 45));//通过之前弹幕的位置，生成多波多方向的圆形弹幕。这里调用了上面写过的圆形弹幕函数
        }
    }

    public IEnumerator FireTurbine(Vector3 pos, int num, int rotation)
    {
        Vector3 bulletDir = shootDir;      //发射方向
        Quaternion rotateQuate = Quaternion.AngleAxis(rotation, Vector3.forward);//使用四元数制造绕Z轴旋转20度的旋转
        float radius = 0.2f;        //生成半径
        float distance = 0.6f;      //每生成一次增加的距离
        for (int i = 0; i < 360; i += rotation)
        {
            Vector3 firePoint = pos + bulletDir * radius;   //使用向量计算生成位置
            StartShoot(FirRound(firePoint, num, 45));     //在算好的位置生成一波圆形弹幕
            yield return new WaitForSeconds(0.15f);     //延时较小的时间（为了表现效果），计算下一步
            bulletDir = rotateQuate * bulletDir;        //发射方向改变
            radius += distance;     //生成半径增加
        }
    }

    //三向散弹
    public IEnumerator FireShot(Vector3 pos, int num, int rotation)
    {
        Vector3 dir = -shootDir;
        Quaternion rightRotateQuate = Quaternion.AngleAxis(rotation, Vector3.forward);
        Quaternion leftRotateQuate = Quaternion.AngleAxis(-rotation, Vector3.forward);
        for (int i = 0; i < 5; i++)
        {
            CreateBullet(BulletType.EnemyRed, pos, dir);
            dir = rightRotateQuate * dir;
            CreateBullet(BulletType.EnemyRed, pos, dir);
            dir = leftRotateQuate * leftRotateQuate * dir;
            CreateBullet(BulletType.EnemyRed, pos, dir);
            dir = -shootDir;
            yield return new WaitForSeconds(0.5f);

            CreateBullet(BulletType.EnemyYellow, pos, dir);
            dir = rightRotateQuate * dir;
            CreateBullet(BulletType.EnemyYellow, pos, dir);
            dir = leftRotateQuate * leftRotateQuate * dir;
            CreateBullet(BulletType.EnemyYellow, pos, dir);
            dir = -shootDir;
            yield return new WaitForSeconds(0.5f);
        }

    }

    //跟踪弹幕
    //跟踪速度较慢
    public IEnumerator FireTrack(Vector3 pos, int num,int rotation)
    {
        Vector3 dir;
        GameObject player;
        for (int i = 0; i < 5; i++)
        {
            player = GameObject.Find("Player");
            dir = player.transform.position - enemy.transform.position;
            CreateBullet(BulletType.EnemyRed, pos, dir);
            yield return new WaitForSeconds(0.5f);
            player = GameObject.Find("Player");
            dir = player.transform.position - enemy.transform.position;
            CreateBullet(BulletType.EnemyYellow, pos, dir);
            yield return new WaitForSeconds(0.5f);
        }
    }

    //球形弹幕
    public IEnumerator FireBall(Vector3 pos, int num, int rotation)
    {
        Vector3 dir = shootDir;      //发射方向
        Quaternion rotateQuate = Quaternion.AngleAxis(rotation, Vector3.forward);
        //float distance = 1.0f;
        for (int j = 0; j < num; j++)
        {
            for (int i = 0; i < 360 / rotation; i++)
            {
                //Vector3 creatPoint = enemy.transform.position + dir * distance;
                Bullet tempBullet = CreateBullet(BulletType.EnemyRed, pos, dir);
                //tempBullet.isMove = false;
                //tempBullet.StartCoroutine(tempBullet.DirChangeMoveMode(10.0f, 0.4f, 15));
                //dir = rotateQuate * dir;
            }
            yield return new WaitForSeconds(0.2f);
        }
    }

    //圆弧形弹幕
    public IEnumerator FireArc(Vector3 pos, int num, int rotation)
    {
        Vector3 dir = shootDir;
        Quaternion rotateQuate = Quaternion.AngleAxis(rotation, Vector3.forward);
        for (int i = 0; i < num; i++)
        {
            for (int j = 0; j < 360 / rotation; j++)
            {
                Bullet tempBullet = CreateBullet(BulletType.EnemyRed, pos, dir);
                //tempBullet.isMove = false;
                //enemy = GameObject.Find("Enemy");
                //tempBullet.StartCoroutine(tempBullet.BulletArc(enemy.transform.position + 3 * new Vector3(dir.y, -dir.x, 0).normalized, 15));
                dir = rotateQuate * dir;
            }
            yield return new WaitForSeconds(0.02f);
        }
    }




    public Bullet CreateBullet(BulletType type, Vector3 pos, Vector3 dir)
    {
        GameObject b;
        switch (type)
        {
            case BulletType.Player:
                b = pool.RequestCacheGameObejct(playerBullet);
                break;
            case BulletType.EnemyRed:
                b = pool.RequestCacheGameObejct(enemyBulletRed);
                break;
            case BulletType.EnemyYellow:
                b = pool.RequestCacheGameObejct(enemyBulletYellow);
                break;

            default:
                Debug.Log("Bullet Type Error!");
                return null;
        }
        b.transform.position = pos;
        b.transform.up = dir;
        b.transform.SetParent(GameObject.Find("Bullets").transform);

        return b.GetComponent<Bullet>();
    }

}



public struct bulletInfo
{
    public Vector3 vector3;
    public int num;
    public int rotation;
}
