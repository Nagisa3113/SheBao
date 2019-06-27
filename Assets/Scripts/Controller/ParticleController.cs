using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : SingletonSerializedMonoBehavior<ParticleController>
{

    public GameObject bulletExplosion;
    public GameObject enemyExplosion;

    public List<GameObject> particleObjects;

    public void CreateBulletExplosion(Vector3 pos)
    {
        CreateParticle(pos, bulletExplosion);
    }

    public void CreateEnemyExplosion(Vector3 pos)
    {
        CreateParticle(pos, enemyExplosion);
    }


    private void CreateParticle(Vector3 pos, GameObject gameObject)
    {
        GameObject go = Pool.Instance.RequestCacheGameObejct(gameObject);
        particleObjects.Add(go);
        go.transform.SetParent(this.transform);
        go.transform.position = pos;
        go.GetComponent<ParticleSystem>().Play();
    }

    private void Update()
    {
        for (int i = particleObjects.Count - 1; i >= 0; i--)
        {
            if (particleObjects[i].GetComponent<ParticleSystem>().isStopped)
            {
                Pool.Instance.ReturnCacheGameObejct(particleObjects[i]);
                particleObjects.RemoveAt(i);
            }
        }
    }





}
