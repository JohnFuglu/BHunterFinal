using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ElectricFuse : StandardObject
{
  
    public static Action onFuseDestroyed;
    public static Action onFuseDestroyedPet;

    [SerializeField] ElectricFence _fence;
  
    void Update()
    {
        if (onFuseDestroyed != null) 
        {
            if (Health <= 0 && _fence != null)
            {
                onFuseDestroyed();
            }
            else if (Health <= 0 && _fence == null)
                onFuseDestroyedPet();
        }
        
    }
}
