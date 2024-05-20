using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortInsertionTrigger : MonoBehaviour
{
    [SerializeField] bool _escape = false;
    [SerializeField] Animator _anima_ship;
    [SerializeField] AudioSource _audio;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && _escape)
        {
            _audio.PlayOneShot(_audio.clip);
            GameObject.Find("GameHandler").GetComponent<PlayerPersistentDataHandler>().EndLevel();
        }
        if (collision.CompareTag("Player"))
        {
            _audio.PlayOneShot(_audio.clip);
            _anima_ship.SetTrigger("TakeOff");
        }
    }
}
