using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDamage : ElementalEffect
{
    protected override void OnParticleCollision(GameObject other)
    {
        if (other.TryGetComponent(out StandardObject objectS) && other.name != "Jaznot") { 
            StartCoroutine(GiveElementalDamages(dotTimer, elemDamage, other));
            // InstantiateFlammesOnExplosion(other);
            FireSpawner fire = new FireSpawner(other, particleEffect);
        }
    }

    private void OnDisable()
    {
        Destroy(gameObject);
    }

    protected override IEnumerator GiveElementalDamages(float sec, float elemDamage, GameObject contactToDamage)

    {
                if (contactToDamage.TryGetComponent(out StandardObject objectS))
                {
                    if(!objectS.Destroyed)
                        objectS.TakeDamage(elemDamage);
                 }
                else if (contactToDamage.TryGetComponent(out Character character))
                {
                    if (!character.Destroyed)
                         character.TakeDamage(elemDamage);
                }
                yield return new WaitForSeconds(sec);

                gameObject.SetActive(false); 
    }

}

public class FireSpawner{
    int max = 5;
    public FireSpawner(GameObject other, ParticleSystem part)
    {
        if (!other.GetComponent<StandardObject>().Destroyed) {
            
            if (other.CompareTag("Destructibles") || other.CompareTag("Ennemis"))
            {
                if (!other.GetComponentInChildren<FireDamage>())
                {  
                        other.AddComponent<FireDamage>();
                        ParticleSystem flames = MonoBehaviour.Instantiate(part) as ParticleSystem;
                        flames.Pause();
                        flames.transform.position = other.transform.position;
                        flames.gameObject.AddComponent<DistanceJoint2D>();
                        flames.GetComponent<DistanceJoint2D>().connectedBody = other.GetComponent<Rigidbody2D>();
                        flames.Play();
                        if (!flames.IsAlive())
                            GameObject.Destroy(flames.gameObject);
                    
                }
            }
        }
    }

}
