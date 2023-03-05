using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ElectricFence : ActionOnDestroy
{
  
    
    [SerializeField]float fenceDamage;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent(out Hero hero)) 
        {
            hero.TakeDamage(fenceDamage);
        }
    }


    public override void Action() 
    {
        GetComponent<PolygonCollider2D>().enabled = false;
        GetComponent<ParticleSystem>().Stop();
        GetComponent<UnityEngine.Rendering.Universal.Light2D>().intensity = 0;
        this.enabled = false;
    }
}
