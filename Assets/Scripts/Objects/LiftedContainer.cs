using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LiftedContainer : ActionOnDestroy
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
    public override void Action()
    {
       DropContainer();
    }
    void DropContainer(){
        Debug.LogWarning("Action !");
        GetComponent<Rigidbody2D>().isKinematic=false;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(gameObject.transform.parent != null)
            _player.transform.SetParent(null);
    }
}
