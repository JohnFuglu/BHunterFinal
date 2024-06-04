using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clouder : MonoBehaviour
{
    [SerializeField]GameObject[] clouds;
    [SerializeField]float wind= 0.5f;

    void Update()
    {
        foreach(GameObject go in clouds){
            go.transform.Translate(Vector2.left*wind);
       } 
    }
}
