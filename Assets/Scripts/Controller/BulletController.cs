using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : Singleton<BulletController>
{

    public GameObject enemy;
    GameObject bullet_yellow;
    GameObject bullet_red;
    Vector3 shootDir;


    public BulletController()
    {
        enemy = GameObject.Find("Enemy");
        bullet_red = Resources.Load<GameObject>("Prefabs/EnemyBullet_Red");
        bullet_yellow = Resources.Load<GameObject>("Prefabs/EnemyBullet_Yellow");
        shootDir = enemy.transform.up;
    }


    public void StartShoot(IEnumerator enumerator)
    {
        enemy.GetComponent<Enemy>().StartCoroutine(enumerator);
    }


    EnemyBullet CreateBullet(EnemyBulletType bulletType, Vector3 pos, Vector3 dir)
    {
        switch (bulletType)
        {
            case EnemyBulletType.Red:
                return Bullet.InitBullet(bullet_red, pos, dir).GetComponent<EnemyBullet>();

            case EnemyBulletType.Yellow:
                return Bullet.InitBullet(bullet_yellow, pos, dir).GetComponent<EnemyBullet>();

            default:
                return null;
        }

    }

    /// <summary>
    /// 向四周发射
    /// </summary>
    /// <param name="pos">生成位置</param>
    /// <param name="rotation">旋转角度</param>
    IEnumerator FirRound(Vector3 pos, int number, int rotation)
    {
        Vector3 dir = shootDir;
        Quaternion rotateQuate = Quaternion.AngleAxis(rotation, Vector3.forward);//使用四元数制造绕Z轴旋转10度的旋转
        for (int i = 0; i < number; i++)    //发射波数
        {
            for (int j = 0; j < 360; j += rotation)
            {
                CreateBullet(EnemyBulletType.Red, pos, dir);   //生成子弹
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
        List<EnemyBullet> bullets = new List<EnemyBullet>();       //装入开始生成的8个弹幕
        for (int i = 0; i < 360; i += rotation)
        {
            var tempBullet = CreateBullet(EnemyBulletType.Red, pos, dir);
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










}
