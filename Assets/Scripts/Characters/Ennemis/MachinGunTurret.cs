using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(ShootSystem))]
public class MachinGunTurret : TurretControler
{
    ShootSystem shoot;
    Vector3 dir;
    [SerializeField]Transform devant;
    protected override void Update()
    {

        hit = Physics2D.Raycast(_shoot.canon.position, devant.transform.position - _shoot.canon.position, _shoot.DistanceDetection);
        Debug.DrawRay(_shoot.canon.position, devant.transform.position - _shoot.canon.position, Color.yellow);
        if (Physics2D.OverlapCircle(transform.position, _rangeToDetectPlayer, playerLayerMask)&&hit)
            _animator.SetBool("ShootBool",true);
        else {
            _animator.SetBool("ShootBool",false);
            transform.Rotate(new Vector3(0,0,1)*200*Time.deltaTime);
        } 
    }

    }

