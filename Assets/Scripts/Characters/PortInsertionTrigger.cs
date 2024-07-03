using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortInsertionTrigger : MonoBehaviour
{
    [SerializeField] bool _escape = false;
    [SerializeField] bool levelHasaShip = true;
    [SerializeField] Animator _anima_ship;
    [SerializeField] AudioSource _audio;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && _escape)
        {
            if(levelHasaShip)
                _anima_ship.SetTrigger("TakeOff");
            _audio.PlayOneShot(_audio.clip);
            GameObject.Find("GameHandler").GetComponent<PlayerPersistentDataHandler>().EndLevel(); 
        }
    }  
}
