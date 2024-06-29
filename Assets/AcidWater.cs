using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidWater : Water
{
   [SerializeField] private float acidDamage;
    protected override void OnTriggerStay2D(Collider2D collision)
    {
        base.OnTriggerStay2D(collision);
        if(collision.CompareTag("Player")||collision.CompareTag("Ennemis")){ 
            if(collision.CompareTag("Player")){
                collision.GetComponent<PlayerController>().inAcide = true;
                collision.GetComponent<Animator>().SetBool("InAcid",true);
            }
            collision.gameObject.GetComponent<Character>().TakeDamage(acidDamage);
        }
           
    }
    protected override void OnTriggerExit2D(Collider2D collision){
        base.OnTriggerExit2D(collision);
        if(collision.CompareTag("Player")){
            collision.GetComponent<PlayerController>().inAcide = false;
            collision.GetComponent<Animator>().SetBool("InAcid",false);
        }
    }
}
