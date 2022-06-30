using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour, IPooledObject
{

    [SerializeField] Rigidbody2D rb;
    public int damages;
    public void OnObjectSpawn(Vector2 dir,float shootForce)
    {
        rb.AddForce(dir * shootForce);
         StartCoroutine(DisableLaser(2));
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            // collision.collider.GetComponent<CharacterControler>().Mort();
            //Verfi type ennemi pour enlever les dégats correspondant au player
            collision.collider.GetComponent<Character>().TakeDamage(damages); //cas multiple avc plus de heros
            gameObject.SetActive(false);
        }

        else if (collision.gameObject.TryGetComponent(out Character character)) 
        {
            character.TakeDamage(damages);
            gameObject.SetActive(false);
        }

        else if (collision.gameObject.TryGetComponent(out StandardObject objStand))
        {
            objStand.TakeDamage(damages);
            gameObject.SetActive(false);
        }
    }


    IEnumerator DisableLaser(float temps)
    {
            yield return new WaitForSeconds(temps);
        gameObject.SetActive(false);

    }
}
