using System.Collections.Generic;
using UnityEngine;

namespace Levels
{
    [System.Serializable]
    public class ObjectPoolItem
    {
        public GameObject pooledObject;
        public int pooledAmount;
        public bool willGrow;
    }
    public class ObjectPooler : MonoBehaviour
    {
        public static ObjectPooler objectPooler;

        [SerializeField] private List<ObjectPoolItem> _itemsToPool;

        [SerializeField] private List<GameObject> _pooledObjects;

        private void Awake()
        {
            objectPooler = this;
        }
        void Start()
        {
            _pooledObjects = new List<GameObject>();
            foreach (ObjectPoolItem item in _itemsToPool)
            {
                for (int i = 0; i < item.pooledAmount; i++)
                {
                    GameObject obj = (GameObject)Instantiate(item.pooledObject);
                    obj.SetActive(false);
                    _pooledObjects.Add(obj);
                }
            }
        }

        public GameObject GetPooledObject(string tag)   // метод активации обьектов
        {
            for (int i = 0; i < _pooledObjects.Count; i++)
            {
                if (!_pooledObjects[i].activeInHierarchy && _pooledObjects[i].tag == tag) //pooledObjects[i].activeInHierarchy == false
                {
                    return _pooledObjects[i];
                }
            }
            foreach (ObjectPoolItem item in _itemsToPool)
            {
                if (item.pooledObject.tag == tag)
                {
                    if (item.willGrow) // willGrow == true   будет ли увеличиваться пул
                    {
                        GameObject obj = Instantiate(item.pooledObject);
                        obj.SetActive(false);
                        _pooledObjects.Add(obj);
                        obj.transform.SetParent(transform);
                        return obj;

                    }
                }
            }
            return null;
        }
    }
}