using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{

    [SerializeField]
    private GameObject _prefab;

    [SerializeField]
    private Queue<GameObject> _pooledInstanceQueue = new Queue<GameObject>();


    public GameObject GetInstance()
    {
        if (_pooledInstanceQueue.Count > 0)
        {
            GameObject instanceToReuse = _pooledInstanceQueue.Dequeue();
            instanceToReuse.SetActive(true);
            return instanceToReuse;
        }

        return Instantiate(_prefab);
    }

    public void ReturnInstance(GameObject gameObjectToPool)
    {
        _pooledInstanceQueue.Enqueue(gameObjectToPool);
        gameObjectToPool.SetActive(false);
        gameObjectToPool.transform.SetParent(gameObject.transform);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
