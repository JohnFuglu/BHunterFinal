using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grue;

public class ButtonCrane : InSceneButton
{
    [SerializeField] CraneScript _crane;
    [SerializeField] string _command;
    protected override void OnMouseDown()
    {
        base.OnMouseDown();
        _crane.CraneCommand(_command);
       
    }

}
