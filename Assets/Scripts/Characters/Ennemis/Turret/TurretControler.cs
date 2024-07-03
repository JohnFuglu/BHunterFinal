﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretControler : Controller
{
    protected Vector2 shootDirection;
    
    public bool looksRight;
    [SerializeField]protected float _rangeToDetectPlayer;
    [SerializeField]protected LayerMask playerLayerMask;
     [SerializeField]protected Slider healthBar;

     [SerializeField]Vector3 offset;
     protected float lastShot;
    protected override void Start()
    {
        base.Start();
        healthBar.maxValue = character.Health;
        character.TempHp = character.Health;
        if (looksRight)
            shootDirection= Vector2.right;
        else
            shootDirection = Vector2.left;
    }
    protected virtual void Update()
    {
        if (!character.Destroyed && !_playerDead)
        {
            DetectAndShoot();
        }
        character.GetDestroyed();  
        ShowHealth();
    }

    protected virtual void ShowHealth(){
        healthBar.transform.position = transform.position + offset;
        healthBar.value = character.Health;
    }
    protected virtual void DetectAndShoot() 
    {
        if (Physics2D.OverlapCircle(this.transform.position, _rangeToDetectPlayer,playerLayerMask)) 
        { 
            Detection(shootDirection* _shoot.DistanceDetection);
            if (hit.transform.CompareTag("Player"))
            {
                lastShot-=_shoot.rateOfFire;
                Debug.Log("Last shot ="+lastShot);
                if(lastShot <=0){
                    lastShot = Time.deltaTime;
                    AskForShot();
                }
            }
        }
    }

    protected void OnDrawGizmosSelected()
    {
        if(this.transform==null)
            return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, _rangeToDetectPlayer);
    }
}
