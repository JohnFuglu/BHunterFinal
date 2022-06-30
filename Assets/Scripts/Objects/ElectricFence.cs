using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class ElectricFence : MonoBehaviour
{
  
    protected PolygonCollider2D _collider;
    protected ParticleSystem _particle;
    protected Light2D _light;
    [SerializeField]float fenceDamage;

    void Start()
    {
        _collider = GetComponent<PolygonCollider2D>();
        _light= GetComponent<Light2D>();
        _particle = GetComponent<ParticleSystem>();
    }

    protected void OnEnable()
    {
        
        ElectricFuse.onFuseDestroyed += OpenGate;

    }
    protected void OnDisable()
    {

        ElectricFuse.onFuseDestroyed -= OpenGate;

    }
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent(out Hero hero)) 
        {
            hero.TakeDamage(fenceDamage);
        }
    }


    protected virtual void OpenGate() 
    {
        _collider.enabled = false;
        _particle.Stop();
        _light.intensity = 0;
    }
}
