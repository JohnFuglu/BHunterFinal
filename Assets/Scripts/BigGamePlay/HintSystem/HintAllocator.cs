using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HintAllocator : MonoBehaviour
{
    public Hint hint;

    [SerializeField] GameObject _hintPrefab;
    bool _spawned = false;
    bool _destroyed = false;
    Transform whereToSpawn;
    //private void OnDisable()
    //{
    //    SpawnHint(hint.hintPrefabToSpawn);
    //}

    public void InitHints() {
        _hintPrefab = hint.hintPrefabToSpawn;
        GameObject instantiated = Instantiate(_hintPrefab) as GameObject;
        instantiated.GetComponent<HintAsset>().hintToFind = hint;
        instantiated.transform.position = transform.position;
    }
    public GameObject SpawnHint(GameObject prefabHint)
    {
        if (GetComponent<HintAllocator>() != null && !_spawned)
        {
           // _hintPrefab = hint.hintPrefabToSpawn;
            GameObject instantiated=Instantiate(prefabHint);
            instantiated.GetComponent<HintAsset>().hintToFind = hint;
            _spawned = true;
            return instantiated;
        }
        Debug.LogWarning("Je n'ai pas instancié de GO dans spawnHint");
        return null;
    }
}
