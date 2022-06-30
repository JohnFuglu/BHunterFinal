using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObjectPooling;
using System;
public class PooledProjectile : PooledObject
{

    [SerializeField] Rigidbody2D _rb;
    public int damages;

    private void OnCollisionEnter2D(Collision2D collision)
    {
      if (collision.gameObject.TryGetComponent(out Character character))
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                character.TakeDamage(damages);
                gameObject.SetActive(false);
            }

            else
            {
                character.TakeDamage(damages);
                gameObject.SetActive(false);
            }
        }

        else if (collision.gameObject.TryGetComponent(out StandardObject objStand))
        {
            objStand.TakeDamage(damages);
            gameObject.SetActive(false);
        }
       // else gameObject.SetActive(false);
    }
   
    IEnumerator DisableLaser(float temps)
    {
        yield return new WaitForSeconds(temps);
        gameObject.SetActive(false);

    }

    public void ShootBullet(Vector2 dir, float shootForce) 
    {
    }
}
