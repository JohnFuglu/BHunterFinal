using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionDoor : ActionOnDestroy
{
    [SerializeField]Transform pont;
    public override void Action() 
    {
        pont.Rotate(new Vector3(0,0,80));
    }
}
