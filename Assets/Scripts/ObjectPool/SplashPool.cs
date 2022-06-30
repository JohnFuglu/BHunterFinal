using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashPool : MonoBehaviour, IPooledObject
{
   [SerializeField] ParticleSystem splash;
   public void OnObjectSpawn(Vector2 dir, float shootForce)
    {
      //  gameObject.SetActive(true);
    }

    public void FixedUpdate()
    {
        if (!splash.isPlaying)
        {
            gameObject.SetActive(false);
        }
    }
}
