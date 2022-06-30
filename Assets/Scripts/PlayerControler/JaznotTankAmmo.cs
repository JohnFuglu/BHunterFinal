using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JaznotTankAmmo : Collectables
{

    [SerializeField]float _fuelAmmo=50f;
  

    protected override void SpecificCollectableBehavior(Collider2D collision)
    {
        if (collision.name == "Jaznot")
        {
            collision.GetComponent<Jaznot>().lanceFlamme.Fuel += _fuelAmmo;
            gameObject.SetActive(false);
        }
    }
}
