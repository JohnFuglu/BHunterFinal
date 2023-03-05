using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Citerne : StandardObject
{

    [SerializeField]  ActionOnDestroy action;
    void Update()
    {
        if (Health <= 0 && !Destroyed) 
        {
            action.Action();
            GetDestroyed();
        }
    }
}
