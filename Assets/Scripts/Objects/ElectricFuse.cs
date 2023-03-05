using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ElectricFuse : StandardObject
{
  /*
   qd cassé : action
  cassé = 0 pv
   */
    

    [SerializeField] ActionOnDestroy action;
  
    void Update()
    {
        if (Health <= 0 && !Destroyed ) {
            if (TryGetComponent<ElectricFence>(out ElectricFence fence))
            {
                fence.Action();
                GetDestroyed();
            }
            if (TryGetComponent<PetPuzzler>(out PetPuzzler trappe))
            {
                trappe.Action();
                GetDestroyed();
            }

            else
            {
                action.Action();
                GetDestroyed();
            }
        }
  
    }
}
