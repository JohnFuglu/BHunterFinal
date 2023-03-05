using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidWater : MonoBehaviour
{
   [SerializeField] private float acidDamage;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")||collision.CompareTag("Ennemis"))
        collision.gameObject.GetComponent<Character>().TakeDamage(acidDamage);
    }

}
