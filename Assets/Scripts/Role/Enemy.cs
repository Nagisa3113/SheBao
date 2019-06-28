using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Role
{
    public Text text;

    delegate IEnumerator IEDelegate(Enemy enemy);
    IEDelegate iEDelegate = null;

    private void Awake()
    {
        iEDelegate = BulletController.Instance.FireRandom;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(iEDelegate(this));
    }

    private void Update()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        text.transform.position = screenPos;
    }


    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.layer == LayerMask.NameToLayer("PlayerBullet"))
        {
            GameObject.Find("AudioController").GetComponent<AudioController>().PlayDie();
            ParticleController.Instance.CreateEnemyhit(transform.position);
            StopAllCoroutines();
            Pool.Instance.ReturnCacheGameObejct(this.gameObject);
            Pool.Instance.ReturnCacheGameObejct(text.gameObject);
            ParticleController.Instance.CreateEnemyExplosion(transform.position);

        }
    }



}
