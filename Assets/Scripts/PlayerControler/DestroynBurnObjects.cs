using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroynBurnObjects : MonoBehaviour
{
    /*S'attache sur les objets pour les détruires par le feu
     marche ac -> explosionDestruction*/

    [SerializeField] GameObject burnedAsset;
    [SerializeField] float rnd;


   public float lifeObject;
    public bool onFire;
    public float dotDamage;
    public bool burned=false;
    DestroynBurnObjects[] _toDestroy;
    private void Start()
    {
        rnd = Random.Range(0.01f, 0.05f);
    }

    private void Update()
    {
        _toDestroy = GetComponents<DestroynBurnObjects>();
        foreach (DestroynBurnObjects dst in _toDestroy)
        {
            if (dst.lifeObject == 0)
            {
                Destroy(dst);
            }
        }
    }


    private void FixedUpdate()
    {
        if (onFire)
        {
            Dot2(dotDamage+rnd);
        }    
    }

    public void Dot2(float dotDamage)
    {
        lifeObject -= dotDamage;
        if (lifeObject <= 0)
        {
            if (GetComponent<EnnemiScript>() != null || GetComponent<DroneControler>() != null)
            {
                Debug.Log("Ennemi brulé!");
                Destroy(gameObject);
            }
          else 
            {
                GameObject bA = Instantiate(burnedAsset);
                bA.SetActive(false);
                bA.transform.position = transform.position;
                Destroy(gameObject);
                bA.SetActive(true);
            }
           
        }
    }

}
