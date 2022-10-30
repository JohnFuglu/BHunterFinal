using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ShootSystem))]
public class MachinGunTurret : TurretControler
{

    ShootSystem shoot;
    Animator animator;
    [SerializeField] GameObject circle;
    [SerializeField] float circleRadius;
    [SerializeField] LayerMask playerLayer;

    void Update()
    {
        var dir = target.transform.position - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        ShootDumbBurstAnimation();
       
    }
}
