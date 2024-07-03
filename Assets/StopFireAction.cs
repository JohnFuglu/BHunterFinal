using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopFireAction : ActionOnDestroy
{
    [SerializeField]ParticleSystem[] fires;
    [SerializeField]BoxCollider2D colliderFire;

    public override void Action()
    {
       foreach(ParticleSystem fire in fires){
            Destroy(fire);
       }
       Destroy(colliderFire);
    }
}
