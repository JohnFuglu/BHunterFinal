using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionTrigger : MonoBehaviour
{
    [SerializeField] AudioClip glass, metal;
    [SerializeField] List <Rigidbody2D>_contactRigidBodies;
    private GameObject _gOToSpawnOn;

    private void Start()
    {
        _contactRigidBodies = new List<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.attachedRigidbody != null) 
        {
            if(!_contactRigidBodies.Contains(collision.attachedRigidbody))
                _contactRigidBodies.Add(collision.attachedRigidbody);

            if (collision.CompareTag("Verre"))
            {
                GetComponent<AudioSource>().PlayOneShot(glass);
            }

            gameObject.SetActive(false);
            AddExplosionForce();
         
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _contactRigidBodies.Clear();
    }


    void AddExplosionForce()
    {
        foreach (Rigidbody2D rb in _contactRigidBodies)
        {
            rb.isKinematic = false;
                if (rb.gameObject.transform.parent.GetComponent<HintAllocator>() != null) 
                {
                     _gOToSpawnOn = rb.gameObject;
                    HintAllocator hintAllo = _gOToSpawnOn.transform.parent.GetComponent<HintAllocator>();
                    hintAllo.SpawnHint(hintAllo.hint.hintPrefabToSpawn);
                    Destroy(rb.GetComponentInParent<Transform>().parent.gameObject, 1f);
                }
        }
    }
}
