using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteExplosion : MonoBehaviour
{

    [Header("Spawn objet detruit")]
    public GameObject destroyedObject;
    public float dispersionForce = 25;
    public GameObject spawnDetruit;
    [SerializeField] GameObject aDetruire;

    public void ExploEnd()
    {
       SpawnDestroyObject(aDetruire);
    }

    public void SpawnDestroyObject(GameObject aDetruire)
    {
            GameObject destroyed = Instantiate(destroyedObject);
            destroyed.transform.position = spawnDetruit.transform.position;
            Destroy(aDetruire);
    }


}
