using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvocatorHKit : Collectables
{
    [SerializeField] float health = 2.5f;

    protected override void SpecificCollectableBehavior(Collider2D collision)
    {
        if (collision.name == "Invocator")
        {
            collision.GetComponent<Hero>().Health += health;
            gameObject.SetActive(false);
        }
    }
}
