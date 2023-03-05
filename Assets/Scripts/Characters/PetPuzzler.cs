using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetPuzzler : ActionOnDestroy
{

    Transform _origin;


   public override void Action()
    {
        GameObject toOpen = gameObject;
        _origin = GetComponent<Transform>();
        toOpen.transform.localScale = new Vector3(0.1f,1,1);
        toOpen.transform.localPosition = new Vector3(-87, 1, 1);

    }

    void CloseGate()
    {
        GameObject toOpen = gameObject;
        toOpen.transform.position = _origin.position;
        toOpen.transform.localScale = _origin.localScale;
    }
}
