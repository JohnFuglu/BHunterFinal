using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Grenef : MonoBehaviour
{
    [SerializeField] ParticleSystem explosion;
    [SerializeField] SpriteRenderer grenef;
    [SerializeField] UnityEngine.Rendering.Universal.Light2D lum;
    [SerializeField] float temps;
    [SerializeField] AudioClip grenefS;
     [SerializeField] AudioSource audSource;

    private void Awake()
    {
        explosion.Pause();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        audSource.PlayOneShot(grenefS);
        StartCoroutine(Explosion(temps));
        explosion.Play();
    }

    private IEnumerator Explosion(float temps)
    {  
        lum.pointLightOuterRadius = 4.37f;
        yield return new WaitForSeconds(temps);
        grenef.enabled = false;
        lum.enabled = false;
        Destroy(gameObject);
    }

}


/* [SerializeField] GameObject particules, lum, grenefEntier;
    [SerializeField] float tempsExplo;
    [SerializeField] float radiusExplo, puissance,puissanceSoulev;
    private ParticleSystem parti;
    Vector3 impactGrenade;
    private void Awake()
    {
        parti = particules.GetComponent<ParticleSystem>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        //impactGrenade.x = collision.GetContact(0).point.x;
        // impactGrenade.y = collision.GetContact(0).point.y;
        //impactGrenade.z = collision.GetContact(0).point.z;
        impactGrenade = collision.transform.position;
        if (collision.gameObject.tag == "Decor"|| collision.gameObject.tag == "Ennemi") ;//ou ennemi
        {
            parti.Play();
            
            StartCoroutine(LumParticules(1f));
        }
    }

    private IEnumerator LumParticules(float temps)
    {
        lum.SetActive(true);
        Explosion();
        yield return new WaitForSeconds(tempsExplo);
        lum.SetActive(false);
        parti.Stop();
        SupprimeGrenade();
    }

    void SupprimeGrenade()
    {
        grenefEntier.SetActive(false);
    }

    void Explosion()
    {
        Vector3 explosionPos = impactGrenade ;//transform.position
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radiusExplo);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
                rb.AddExplosionForce(puissance, explosionPos, radiusExplo, puissanceSoulev,ForceMode.Impulse);
        }
    }*/
