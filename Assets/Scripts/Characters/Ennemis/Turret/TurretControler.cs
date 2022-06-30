using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretControler : Controller
{
    protected Vector2 shootDirection;
    protected bool _playerDead = false;
    public bool looksRight;
    [SerializeField] float _rangeToDetectPlayer;
    [SerializeField] LayerMask _layerMask;
    protected override void Start()
    {
        base.Start();
        PlayerController.killedDelegate += PlayerDead;
        character.TempHp = character.Health;
        if (looksRight)
            shootDirection= Vector2.right;
        else
            shootDirection = Vector2.left;
    }
    private void Update()
    {
        if (!character.Destroyed && !_playerDead)
        {
            DetectAndShoot();
        }
        character.GetDestroyed();  
    }


    void PlayerDead()
    {
        _playerDead = true;
    }


    protected virtual void DetectAndShoot() 
    {
        if (Physics2D.OverlapCircle(this.transform.position, _rangeToDetectPlayer,_layerMask)) 
        { 
            Detection(shootDirection* _shoot.DistanceDetection);
            if (hit.transform.CompareTag("Player"))
            {
                AskForShot();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if(this.transform==null)
            return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, _rangeToDetectPlayer);
    }
}
