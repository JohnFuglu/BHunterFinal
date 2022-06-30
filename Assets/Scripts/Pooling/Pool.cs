using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectPooling 
{ 
    [System.Serializable]
    public class Pool : MonoBehaviour
    {
        [SerializeField] int _prefabNeed;
        public int PrefabNeeded { get { return _prefabNeed; } }

        [SerializeField] string _poolName;
        public string PoolName { get { return _poolName; } }
        public GameObject Container { get; set; }
        public List<GameObject> poolOfObjects;
        [SerializeField] GameObject _prefab;
        public GameObject Prefab { get { return _prefab; } }
        }
}