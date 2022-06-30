using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObjectPooling;
public class ShootSystem : MonoBehaviour
{
    //type character qui va englober ces variables;
    [SerializeField] int _ammo,_clipCount, _ammoMaxInClip, _actualAmmoInClip, _distanceDetection,_burstNumber;//_damage ??
    [SerializeField] TurretControler _controller;

    //Clip System
    public int Ammo { get { return _ammo; } set { _ammo = value; } }
    public int ClipCount { get { return _clipCount; } set { _clipCount = value; } }
    public int AmmoMaxInClip { get { return _ammoMaxInClip; } set { _ammoMaxInClip = value; } }
    public int ActualAmmoInClip { get { return _actualAmmoInClip; } set { _actualAmmoInClip = value; } }

    public int DistanceDetection { get { return _distanceDetection; } set { _distanceDetection = value; } }

    public int BurstNumber { get { return _burstNumber; } set { _burstNumber = value; } }
    public Projectile Projectile { get; set; }
    public float RateOfFire { get; set; }
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
            ActualiseAmmoCount(1);
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
                ActualiseAmmoCount(1);
            }
            else
            {
                GameObject shotBullet = Objectpool.Instance.RequestObjectFromAPool(pool.PoolName, Vector3.left, _shootForce);
                shotBullet.transform.position = canon.transform.position;
                shotBullet.GetComponent<Rigidbody2D>().AddForce(Vector3.left * _shootForce);// a éviter
                ActualiseAmmoCount(1);
            }
            
        }
    }

    public void ShootBurst(Vector3 target,int burst) 
    {
        if (ActualAmmoInClip >0) //Ammo > 0 && ActualAmmoInClip >= 0+burst
        {
            Debug.DrawRay(canon.transform.position, target - canon.transform.position, Color.blue);
            Vector2 directionToShootAt = target - canon.transform.position;

            
            for (int i = 0; i<= burst; i++) 
            {
                if (ActualAmmoInClip > 0) 
                {
                    Vector2 randomizedDirectionToShootAt = new Vector2(directionToShootAt.x, directionToShootAt.y + Random.Range(0.1f, 0.5f));
                    GameObject shotBullet = Objectpool.Instance.RequestObjectFromAPool(pool.PoolName, randomizedDirectionToShootAt, _shootForce);
                    shotBullet.transform.position = canon.transform.position;
                    shotBullet.GetComponent<Rigidbody2D>().AddForce(randomizedDirectionToShootAt * _shootForce);// a éviter
                    Debug.Log("Tire une balle ");
                    ActualiseAmmoCount(burst); 
                }
            }
        }

    }

    public void ShootBurstDumb(int burst)
    {
        int i = 0;
       
        if (ActualAmmoInClip >= _burstNumber) //  _ammo > 0    Ammo > 0 && 
        {
            if (_controller.looksRight)
            {
               while(i<=_burstNumber)
                {
                    GameObject shotBullet = Objectpool.Instance.RequestObjectFromAPool(pool.PoolName, Vector3.right, _shootForce);
                    shotBullet.transform.position = canon.transform.position;
                    shotBullet.GetComponent<Rigidbody2D>().AddForce(Vector3.right * _shootForce);// a éviter
                    ActualiseAmmoCount(i);
                    i++;
                }
            }
            else
            {
                for (i = 0; i <= burst; i++)
                {
                    GameObject shotBullet = Objectpool.Instance.RequestObjectFromAPool(pool.PoolName, Vector3.left, _shootForce);
                    shotBullet.transform.position = canon.transform.position;
                    shotBullet.GetComponent<Rigidbody2D>().AddForce(Vector3.left * _shootForce);// a éviter
                    ActualiseAmmoCount(burst);
                }
            }

        }
    }

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
                ActualiseAmmoCount(1);
            }
            else
            {
                GameObject shotBullet = Objectpool.Instance.RequestObjectFromAPool(pool.PoolName, Vector3.left, _shootForce);
                shotBullet.transform.position = canon.transform.position;
                shotBullet.GetComponent<Rigidbody2D>().AddForce(Vector3.left * _shootForce);// a éviter
                ActualiseAmmoCount(1);
            }

        }
    }



    void ActualiseAmmoCount(int nbrBulletsShot) 
    {
        ActualAmmoInClip-= nbrBulletsShot;
    }

}
#region(Working)
//public void ShootBurst(Vector3 target, int burst)
//{
//    if (ActualAmmoInClip > 0) //Ammo > 0 && ActualAmmoInClip >= 0+burst
//    {
//        Debug.DrawRay(canon.transform.position, target - canon.transform.position, Color.blue);
//        Vector2 directionToShootAt = target - canon.transform.position;

//        List<GameObject> burstGameObjects = new List<GameObject>();
//        for (int i = 0; i <= burst; i++)
//        {
//            burstGameObjects.Add(_ammoPrefab);
//            Debug.Log("ajout d'une balle dans " + burstGameObjects);
//        }
//        foreach (GameObject bullet in burstGameObjects)
//        {

//            Vector2 randomizedDirectionToShootAt = new Vector2(directionToShootAt.x, directionToShootAt.y + Random.Range(0.1f, 0.5f));
//            GameObject shotBullet = Instantiate(_ammoPrefab);
//            shotBullet.transform.position = canon.transform.position;
//            shotBullet.GetComponent<Rigidbody2D>().AddForce(randomizedDirectionToShootAt * _shootForce);
//            Debug.Log("Tire une balle ");
//            burstGameObjects.Clear();
//            Debug.Log("Clear la liste");
//            _ammo--;
//            ActualiseAmmoCount(burst);
//        }

//    }

//}

//public void Shoot(Vector3 target)//GameObject target
//{
    //if (ActualAmmoInClip > 0) //  _ammo > 0    Ammo > 0 && 
    //{ 
    //    Vector2 directionToShootAt = target-canon.transform.position;

    //     GameObject t = Instantiate(_ammoPrefab);
    //     t.transform.position = canon.transform.position;
    //     Debug.DrawRay(canon.transform.position, target-canon.transform.position, Color.green);
    //     t.GetComponent<Rigidbody2D>().AddForce(directionToShootAt * _shootForce);//shootAt-transform.position * _shootForce


    //    ObjectPooler.Instance.RequestBullet();

    //    // _ammo--;
    //    ActualiseAmmoCount(1);
    //} 
//}

#endregion