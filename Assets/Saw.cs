using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player") {
            collision.gameObject.GetComponent<Hero>().Health -= 1;
        }
    }
}
