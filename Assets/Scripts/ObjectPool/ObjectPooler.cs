using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ObjectPooler : MonoSingleton <ObjectPooler>
{
   // public static Action OnRequest;


    [SerializeField] private GameObject _container;
    [SerializeField] private GameObject _bullet;
    public int numberOfBullets = 10;
    [SerializeField]private List<GameObject> _bulletList;

    List<GameObject>GenerateBullets() 
    {
    
        for (int i = 0; i < numberOfBullets;i++) 
        {
            GameObject instance = Instantiate(_bullet);
            instance.transform.parent = _container.transform;
            instance.SetActive(false);
            _bulletList.Add(_bullet);
        }
       
        return _bulletList;
    }

    private void Start()
    {
        _bulletList = GenerateBullets();
    }
   
    public void RequestBullet() 
    {
        foreach(GameObject bullet in _bulletList) 
        {
            if (!bullet.activeInHierarchy && bullet !=null) 
            {
                bullet.SetActive(true);
            }
            if (bullet.activeInHierarchy) 
            {
                GameObject instance = Instantiate(_bullet);
                instance.transform.parent = _container.transform;
                instance.SetActive(true);
                _bulletList.Add(_bullet);
            }
        }
           
    }
}
/*  [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }


    #region Singleton
    public static ObjectPooler Instance;

    private void Awake()
    {
        Instance= this;
    }



    #endregion


    public List<Pool>pools;
    public Dictionary<string,Queue<GameObject>> poolDictionary;



    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach(Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for(int i = 0; i< pool.size; i++)
            {
               GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag,Vector3 position,Quaternion rotation, Vector2 dir, float shootForce,float damages)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Le tag" + tag+"n'apparait pas dans le dico");
            return null;
        }


        GameObject objToSpawn = poolDictionary[tag].Dequeue();
        objToSpawn.SetActive(true);
        objToSpawn.transform.position = position;
        objToSpawn.transform.rotation = rotation;

        IPooledObject pooled = objToSpawn.GetComponent<IPooledObject>();

        if (pooled != null)
        {
            pooled.OnObjectSpawn(dir, shootForce);
        }

        poolDictionary[tag].Enqueue(objToSpawn);

        return objToSpawn;
    }
*/
