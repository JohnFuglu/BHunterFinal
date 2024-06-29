using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider2D))]
public class InSceneButton : MonoBehaviour
{

   
    protected UnityEngine.Rendering.Universal.Light2D _light;
    [SerializeField] protected Color _pushedColor;
    protected Color _startColor;
    protected virtual void Start()
    {
        _light = GetComponent<UnityEngine.Rendering.Universal.Light2D>();
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
