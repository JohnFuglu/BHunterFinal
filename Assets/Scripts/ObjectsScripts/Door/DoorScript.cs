using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    [SerializeField]protected Animator anim;
    protected bool open=false;
    [SerializeField] string doorName;
    [SerializeField] protected AudioSource audio;
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
            open= collision.GetComponent<PlayerController>().CheckKey(doorName);
        if (open)
        {
            audio.PlayOneShot(audio.clip);
            anim.SetBool("Open", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            audio.PlayOneShot(audio.clip);
            anim.SetBool("Open", false);
            anim.SetBool("Close", true);
        }
    }

}
