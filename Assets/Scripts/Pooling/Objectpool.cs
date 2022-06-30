using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectPooling { 

public class Objectpool : MonoBehaviour 
{
    #region
    private static Objectpool _instance;
    public static Objectpool Instance 
    { get 
        {
            if (_instance == null)
                Debug.LogError("Missing pool"); 
            return _instance;
        }    
    }
    #endregion

  
    //[SerializeField] GameObject _bulletContainer;
    [SerializeField] List<Pool> _poolsList;
    
    private void Awake()
    {
        _instance = this;
    }
  
    void Start()
    {
        foreach(var pool in _poolsList) 
        { 
          GenerateGameObjects(pool);
        }
          
    }

    List<GameObject>GenerateGameObjects(Pool pool) 
    { 
        for(int i = 0; i < pool.PrefabNeeded; i++)
        {
            pool.Container = pool.gameObject;
            GameObject instantiated = Instantiate(pool.Prefab);

            //PooledObject poolObjectinstantiated= instantiated.GetComponent<PooledObject>();
                PooledProjectile poolObjectinstantiated = instantiated.GetComponent<PooledProjectile>();
            poolObjectinstantiated.NameInPool = pool.name;
            instantiated.SetActive(false);
            instantiated.transform.parent = pool.Container.transform;
            pool.poolOfObjects.Add(instantiated);
        }
        return pool.poolOfObjects;
    }

    public GameObject RequestObjectFromAPool(string nameOfPool, Vector2 dir , float  shootForce)
    {
        foreach(Pool pool in _poolsList) 
        {
            if (pool.name == nameOfPool)
            {
                Pool poolToRequestFrom = pool;
                foreach (var objectPooled in poolToRequestFrom.poolOfObjects)
                {
                    if (objectPooled.activeInHierarchy == false)
                    {
                        objectPooled.SetActive(true);
                        return objectPooled;
                    }
                }

                GameObject newObjectNeeded = Instantiate(poolToRequestFrom.Prefab);
                newObjectNeeded.transform.parent = poolToRequestFrom.Container.transform;
                newObjectNeeded.SetActive(true);
                poolToRequestFrom.poolOfObjects.Add(newObjectNeeded);
                return newObjectNeeded;
            }
            //else 
            //    {
            //        Debug.LogError("Mauvais nom de Pool");
            //        return null;
            //    }          
        }
            Debug.LogError("Pas de Pool");
            return null;

    }
}
}