using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePool : MonoBehaviour
{

    [SerializeField] ParticleSystem fire;
    public void OnObjectSpawn(Vector2 dir, float shootForce)
    {
        gameObject.SetActive(true);
    }

    public void FixedUpdate()
    {
        if (!fire.isPlaying)
        {
            gameObject.SetActive(false);
        }
    }
}
