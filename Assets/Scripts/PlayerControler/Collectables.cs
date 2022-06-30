using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    [SerializeField] string thisHeroName;
    protected bool contactPlayer;
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == thisHeroName)
        {
            contactPlayer = true;
        }
        if (contactPlayer) 
        {
            SpecificCollectableBehavior(collision);
        }
    }

    protected virtual void SpecificCollectableBehavior(Collider2D collision) { }
}
