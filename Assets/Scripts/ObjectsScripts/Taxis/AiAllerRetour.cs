using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiAllerRetour : MonoBehaviour
{
   [SerializeField] Transform arrive;
    [SerializeField] float vitesse;
     public Vector3 velocity;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(TryGetComponent(out AiBornes airborne)) 
        {
            arrive = collision.gameObject.GetComponent<AiBornes>().arrive.transform;
        }
       
    }

    private void FixedUpdate()
    {
        transform.position += (velocity * Time.deltaTime);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
         if (collision.gameObject.CompareTag("Player"))
        {
           collision.collider.transform.SetParent(transform);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.collider.transform.SetParent(null);
        }
    }

}
