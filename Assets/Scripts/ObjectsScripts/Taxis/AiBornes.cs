using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiBornes : MonoBehaviour
{
    public GameObject arrive;
    bool lookRight=false;
    [SerializeField] PolygonCollider2D coll;
    [SerializeField] AiAllerRetour aiVelolity;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")&& aiVelolity!=null)
        {
            aiVelolity.velocity = aiVelolity.velocity *-1;
            Flip(collision.transform);
        }
    }
   
    void Flip(Transform vehicle)
    {
        if (coll != null) 
        {
            if (vehicle.GetComponent<SpriteRenderer>().flipX == false)
            {
                vehicle.GetComponent<SpriteRenderer>().flipX = true;
                Vector2 flipVector = coll.transform.localScale;
                flipVector.x *= -1;
                transform.localScale = flipVector;
            }

            else
            {

                vehicle.GetComponent<SpriteRenderer>().flipX = false;
                Vector2 flipVector = coll.transform.localScale;
                flipVector.x *= -1;
                transform.localScale = flipVector;
            }


        }

    }
   
}
