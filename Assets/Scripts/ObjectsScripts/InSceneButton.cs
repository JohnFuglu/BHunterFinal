﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using Grue;

[RequireComponent(typeof(Collider2D))]
public class InSceneButton : MonoBehaviour
{

   
    protected Light2D _light;
    [SerializeField] protected Color _pushedColor;
    protected Color _startColor;
    protected virtual void Start()
    {
        _light = GetComponent<Light2D>();
        _startColor = _light.color;
    }
    protected virtual void OnMouseDown()
    {
        _light.color =_pushedColor;
    }

    protected virtual void OnMouseUp()
    {
        _light.color = _startColor;
    }
}
