using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHintOnDestroy
{
    private GameObject _gOToSpawnOn;
    public GameObject GoToSpawnOn { get; set; }
    public SpawnHintOnDestroy(GameObject objectToSpawnHintOn) 
    {
        this._gOToSpawnOn = objectToSpawnHintOn;
        SpawnHintGameObject(this._gOToSpawnOn);
    }

    public void SpawnHintGameObject(GameObject thisGo)
    {
            HintAllocator hintAllo = thisGo.GetComponent<HintAllocator>();
            GameObject SpawnHint = hintAllo.SpawnHint(hintAllo.hint.hintPrefabToSpawn);
            SpawnHint.transform.position = thisGo.transform.position;
            SpawnHint.transform.parent = null;
            Debug.Log("Je suis à la fin de spawnHintGO");
    }

}
