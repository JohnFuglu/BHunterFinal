using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ShootSystem))]
public class MachinGunTurret : TurretControler
{

    ShootSystem shoot;
    Vector3 dir;
    void Update()
    {
        dir = target.transform.position - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        DetectionDrone();
    }


    void DetectionDrone()
    {
        hit = Physics2D.Raycast(_shoot.canon.position, dir, _shoot.DistanceDetection);
        
        detectSomething = hit;
        Debug.DrawRay(_shoot.canon.position, dir, Color.red);
        if (detectSomething)
        {
                target = hit.collider.gameObject;                
                if (Physics2D.OverlapCircle(transform.position, _rangeToDetectPlayer, _layerMask))
                    _animator.SetBool("ShootBool",true);
                else _animator.SetBool("ShootBool", false);
        }
    }
}
