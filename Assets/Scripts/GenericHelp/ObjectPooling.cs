using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    [System.Serializable]
    struct Pool
    {
        public string tag;
        public GameObject pooledObject;
        public int amount;
    }

    [SerializeField] private Pool[] _pools;

    private Dictionary<string, Queue<GameObject>> _poolsDictionary;

    public void Start()
    {
        _poolsDictionary = new Dictionary<string, Queue<GameObject>>(_pools.Length);
        foreach(Pool pool in _pools)
        {
            _poolsDictionary.Add(pool.tag, new Queue<GameObject>(pool.amount));

            for (int i = 0; i < pool.amount; i++)
            {
                GameObject instance = Instantiate(pool.pooledObject);
                _poolsDictionary[pool.tag].Enqueue(instance);
            }
        }
    }

    public GameObject GetFromPool(string name)
    {
        if (_poolsDictionary.ContainsKey(name))
        {

            GameObject obj = _poolsDictionary[name].Dequeue();
            _poolsDictionary[name].Enqueue(obj);

            return obj;
        }
        else
        {
            Debug.LogError("Pool doesn't contain " + name + " key");
            return null;
        }
    }
}
