using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grue { 
public class LiftedContainer : MonoBehaviour
{
    Transform _player;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))       
        {
            _player = collision.transform;
            collision.transform.SetParent(transform);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(gameObject.transform.parent != null)
            _player.transform.SetParent(null);
    }
}
}