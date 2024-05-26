using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObjectPooling;
public class ShootSystem : MonoBehaviour
{
    //type character qui va englober ces variables;
    [SerializeField] int _ammo,_clipCount, _ammoMaxInClip, _actualAmmoInClip, _distanceDetection;//_damage ??
    [SerializeField] TurretControler _controller;
    public short burstNbr;
    //Clip System
    public int Ammo { get { return _ammo; } set { _ammo = value; } }
    public int ClipCount { get { return _clipCount; } set { _clipCount = value; } }
    public int AmmoMaxInClip { get { return _ammoMaxInClip; } set { _ammoMaxInClip = value; } }
    public int ActualAmmoInClip { get { return _actualAmmoInClip; } set { _actualAmmoInClip = value; } }

    public int DistanceDetection { get { return _distanceDetection; } set { _distanceDetection = value; } }

    public Projectile Projectile { get; set; }
    public float rateOfFire;
    private float nextShot;
    bool canShoot=true;
    //public Attack AttackAction { get; set; }


    [Header("DEBUG")]
    // [SerializeField] Projectile _projectile;
   

    [Header("Canon")]
    public Transform canon;
  

    [Header("PoolSystemAndShootParameters")]
   //public string poolName;
    [SerializeField] Pool pool;
    [SerializeField] int _shootForce;
   // [SerializeField] GameObject _ammoPrefab; // à enlever
    public AudioClip shootSound,reloadSound;
   // public bool weaponReloaded;

     bool _looksRight;
    void Start()
    {
     //   weaponReloaded = true;
  
        Ammo = AmmoMaxInClip * ClipCount;
        ActualAmmoInClip = AmmoMaxInClip;
        if(TryGetComponent<TurretControler>(out TurretControler turret)) 
        {
            _looksRight = turret.looksRight;
        }
        if (this.gameObject.name=="Invocator")
            this.pool = GameObject.Find("InvocatorPool").GetComponent<Pool>();
    }

    public void Reload() 
    {
    
        Debug.Log("Ammo"+ Ammo);
        if(Ammo>0) 
        {
            if (Ammo >= AmmoMaxInClip) 
            { 
              ActualAmmoInClip = AmmoMaxInClip;
              Ammo -= AmmoMaxInClip;
                ClipCount--;
            }
            else if (Ammo>0 && Ammo<AmmoMaxInClip) 
            { 
              ActualAmmoInClip = Ammo;
                ClipCount--;
            }
        }
  
    }


    public void Shoot(Vector3 target)//GameObject target
    {
        if (ActualAmmoInClip > 0) //  _ammo > 0    Ammo > 0 && 
        {
            Vector2 directionToShootAt = target - canon.transform.position;
            //Vector2 randomizedDirectionToShootAt = new Vector2(directionToShootAt.x, directionToShootAt.y + Random.Range(0.1f, 0.3f));
            GameObject shotBullet = Objectpool.Instance.RequestObjectFromAPool(pool.PoolName, directionToShootAt, _shootForce);
            shotBullet.transform.position = canon.transform.position;
            shotBullet.GetComponent<Rigidbody2D>().AddForce(directionToShootAt * _shootForce);// a éviter
            ActualiseAmmoCount();
        }
    }

    public void ShootDumbBurst(Vector3 target) 
    {
        canShoot = true;
        if (canShoot)
        {
            if (ActualAmmoInClip > 0) //Ammo > 0 && ActualAmmoInClip >= 0+burst
            { 
                Debug.DrawRay(canon.transform.position, target - canon.transform.position, Color.blue);
                Vector2 directionToShootAt = target - canon.transform.position;
                StartCoroutine(RateOfFire(rateOfFire,burstNbr,directionToShootAt)); 
                
                ActualiseAmmoCount(burstNbr);

                //StopCoroutine(RateOfFire(rateOfFire,burstNbr,directionToShootAt));
            }
        }
        
    }
    IEnumerator RateOfFire(float rateOfFire,short burstNbr, Vector2 directionToShootAt){
        int i =0;
        while (i<burstNbr){
            i++;
            Vector2 randomizedDirectionToShootAt = new Vector2(directionToShootAt.x, directionToShootAt.y + Random.Range(0.1f, 0.5f));
            GameObject shotBullet = Objectpool.Instance.RequestObjectFromAPool(pool.PoolName, randomizedDirectionToShootAt, _shootForce);
            shotBullet.transform.position = canon.transform.position;
            shotBullet.GetComponent<Rigidbody2D>().AddForce(randomizedDirectionToShootAt * _shootForce);// a éviter
        yield return new WaitForSeconds(rateOfFire);
        }
        
        
    }
    public void ShootDumb() 
    {
        if (ActualAmmoInClip > 0) //  _ammo > 0    Ammo > 0 && 
        {
            if (_controller.looksRight) 
            {    GameObject shotBullet = Objectpool.Instance.RequestObjectFromAPool(pool.PoolName, Vector3.right, _shootForce);
                shotBullet.transform.position = canon.transform.position;
                shotBullet.GetComponent<Rigidbody2D>().AddForce(Vector3.right * _shootForce);// a éviter
                ActualiseAmmoCount();
            }
            else
            {
                GameObject shotBullet = Objectpool.Instance.RequestObjectFromAPool(pool.PoolName, Vector3.left, _shootForce);
                shotBullet.transform.position = canon.transform.position;
                shotBullet.GetComponent<Rigidbody2D>().AddForce(Vector3.left * _shootForce);// a éviter
                ActualiseAmmoCount();
            }
            
        }
    }
    public void ShootAuto(Vector3 target) {
            if(Time.time > nextShot) {
                Vector2 directionToShootAt = target - canon.transform.position;
                Vector2 randomizedDirectionToShootAt = new Vector2(directionToShootAt.x, directionToShootAt.y + Random.Range(0.1f, 0.5f));
                GameObject shotBullet = Objectpool.Instance.RequestObjectFromAPool(pool.PoolName, randomizedDirectionToShootAt, _shootForce);
                shotBullet.transform.position = canon.transform.position;
                shotBullet.GetComponent<Rigidbody2D>().AddForce(randomizedDirectionToShootAt * _shootForce);// a éviter
                ActualiseAmmoCount();
            nextShot = Time.time + rateOfFire;
        }
    }
    public void ShootBurst(Vector3 target) 
    {
        canShoot = true;
        if (canShoot)
        {
            if (ActualAmmoInClip > 0) //Ammo > 0 && ActualAmmoInClip >= 0+burst
            {
                Debug.DrawRay(canon.transform.position, target - canon.transform.position, Color.blue);
                Vector2 directionToShootAt = target - canon.transform.position;

                for (short i = 0; i<burstNbr; i++)
                {
                        Vector2 randomizedDirectionToShootAt = new Vector2(directionToShootAt.x, directionToShootAt.y + Random.Range(0.1f, 0.5f));
                        GameObject shotBullet = Objectpool.Instance.RequestObjectFromAPool(pool.PoolName, randomizedDirectionToShootAt, _shootForce);
                        shotBullet.transform.position = canon.transform.position;
                        shotBullet.GetComponent<Rigidbody2D>().AddForce(randomizedDirectionToShootAt * _shootForce);// a éviter
                        Debug.Log("Tire une balle ");
                        ActualiseAmmoCount();
                }
                
            }
        }
    }
    public void StopAutoFire() { canShoot = false; }
    public void ShootHero(bool playerDirectionIsRight) 
    {
        Debug.Log("Hero looks right = " + playerDirectionIsRight);
        if (ActualAmmoInClip > 0) //  _ammo > 0    Ammo > 0 && 
        {
            if (!playerDirectionIsRight) // inversé dans le jeu...
            {
                GameObject shotBullet = Objectpool.Instance.RequestObjectFromAPool(pool.PoolName, Vector3.right, _shootForce);
                shotBullet.transform.position = canon.transform.position;
                shotBullet.GetComponent<Rigidbody2D>().AddForce(Vector3.right * _shootForce);// a éviter
                ActualiseAmmoCount();
            }
            else
            {
                GameObject shotBullet = Objectpool.Instance.RequestObjectFromAPool(pool.PoolName, Vector3.left, _shootForce);
                shotBullet.transform.position = canon.transform.position;
                shotBullet.GetComponent<Rigidbody2D>().AddForce(Vector3.left * _shootForce);// a éviter
                ActualiseAmmoCount();
            }

        }
    }

    void ActualiseAmmoCount(int burst){
        ActualAmmoInClip-=burst;
    }


    void ActualiseAmmoCount() 
    {
        ActualAmmoInClip--;
    }

}
