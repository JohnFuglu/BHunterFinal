using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    StandardObject thisObject;
    void Start()
    {
        thisObject = GetComponent<StandardObject>();
    }

    private void Update()
    {
        thisObject.GetDestroyed();
    }

}
