using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneControler : MonoBehaviour
{
    ////nouvelle class
    //[SerializeField] Attack _attack;


    //    //stats
    //[SerializeField] List <Transform> waypoints;
    //[SerializeField] EnnemiMobile thisEnnemi;
    //[SerializeField] float speed;
    //Transform currentWaypoint;
    //int waypoint;

    ////Arme
    //[SerializeField] Transform _canon;

    //Transform player;

    ////pool
    //ObjectPooler objPooler;
    //[SerializeField] string nomPool;
    //[SerializeField] GameObject _target;
    ////rate of fire
    //float _timer; 
    //void Start()
    //{
    //    currentWaypoint = waypoints[waypoint];
    //    player = GameObject.FindWithTag("Player").GetComponent<Transform>();
    //    //tir
    //    objPooler = ObjectPooler.Instance;
    //    thisEnnemi.projectile.damages = thisEnnemi.damages;
    //}

    //void Update()
    //{ 
    //    Quaternion rotation = Quaternion.LookRotation(waypoints[waypoint].position - transform.position, transform.TransformDirection(Vector3.forward));
    //    transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
    //    float dist = Vector3.Distance(waypoints[waypoint].position, transform.position);
    //    transform.position = Vector3.MoveTowards(transform.position, waypoints[waypoint].position, Time.deltaTime * speed);

    //    if (Vector3.Distance(transform.position, waypoints[waypoint].position) < 2.5)
    //    {
    //        if (waypoint + 1 < waypoints.Count)
    //            waypoint++;
    //        else
    //            waypoint = 0;
    //    }
    //    Vector3 forward = transform.TransformDirection(Vector3.down) * 10;
    //    _target = _attack.Detection(forward, _canon);
    //    if (_target != null) 
    //    {
    //        Debug.Log("Ammo = "+thisEnnemi.munitions);
    //        if (thisEnnemi.munitions > 0)
    //         {   
    //            _attack.Shoot(_target,thisEnnemi.damages,_canon,thisEnnemi.munitions);
    //         }
    //    }


       
    //}

    //void Tire(Vector3 direction) 
    //{
    //    Debug.DrawRay(transform.position, direction, Color.green);

    //    RaycastHit2D hit = Physics2D.Raycast(transform.position, direction,detectSensors);
    //    _timer += Time.deltaTime;
    //    if (hit.transform.CompareTag("Player") && _timer > thisEnnemi.rateOfFire)
    //    {
    //        //GameObject t = Instantiate(leTir);
    //        //t.transform.position = canon.position;

    //        //t.GetComponent<Rigidbody2D>().AddForce(direction*shootForce);


    //        objPooler.SpawnFromPool(nomPool, canon.transform.position, Quaternion.identity, direction, shootForce, thisEnnemi.damages);
    //        thisEnnemi.munitions--;
    //        _timer = 0;
    //    }
    //}

  
}
