using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalEffect : MonoBehaviour
{
    [SerializeField] protected ParticleSystem particleEffect;
    [SerializeField] protected float elemDamage, dotTimer;

    protected void Awake()
    {
        particleEffect = GetComponent<ParticleSystem>();
        dotTimer = particleEffect.main.duration;
    }

    protected virtual void OnParticleCollision(GameObject other)
    {
        Debug.Log("Collision in Base elemental with " + other);
        StartCoroutine(GiveElementalDamages(dotTimer, elemDamage, other));
    }
    protected void OnDestroy()
    {
        StopAllCoroutines();
    }

    protected virtual IEnumerator GiveElementalDamages(float sec,float elemDamage, GameObject contactToDamage)

    {
        yield return new WaitForSeconds(sec);
    }
}
