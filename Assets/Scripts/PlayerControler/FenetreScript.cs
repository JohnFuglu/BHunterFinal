using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenetreScript : MonoBehaviour
{
    [SerializeField] Rigidbody2D[] shards = new Rigidbody2D[4];
    [SerializeField] AudioClip glass,metal;//prevoir switch ou if pour le type de trucs qui cassent
    [SerializeField] AudioSource aud;
    [SerializeField] Vector2 explosionDirection;
    [SerializeField] float randomMin,randomMax;
    private float randomFloat;
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {    
            foreach (Rigidbody2D rb in shards)
            {
                aud.PlayOneShot(glass);//ou metal !!!
                randomFloat = Random.Range(randomMin, randomMax);
                rb.isKinematic = false;
                rb.AddForce(explosionDirection * randomFloat);
            }
        }
        
    }
}
