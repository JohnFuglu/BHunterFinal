using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShardsDisposer : MonoBehaviour
{
    [SerializeField] List<GameObject> shards;

    private void OnEnable()
    {
        foreach (GameObject g in shards)
            Destroy(g,3);
    }

}
