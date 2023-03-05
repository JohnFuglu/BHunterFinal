using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevateur : ActionOnDestroy
{
 
    [SerializeField] float distance;
    bool down = false;
    public override void Action() {
        if (!down) { 
            transform.Translate(new Vector2(0, -distance));
        down = true;
        }
    }
}

