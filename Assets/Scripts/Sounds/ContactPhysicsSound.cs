using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactPhysicsSound : MonoBehaviour
{
    [SerializeField] AudioSource adS;
    [SerializeField] AudioClip sound;
    //particle ? Anim ?
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) 
        {
            adS.PlayOneShot(sound);
            if (!adS.isPlaying) 
            {
                Destroy(gameObject);
            }
        }
    }
}
