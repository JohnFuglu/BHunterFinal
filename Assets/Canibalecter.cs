using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Canibalecter : SentinelControler
{
    bool running=false;
    bool rangeDistance = true;
    bool projDistance = false;
    [SerializeField] Transform shootZone;
    [SerializeField] float cAcCoolDown = 2f;
    [SerializeField] float projCoolDown = 5f;
    protected override void ComplexAi()
    {
        base.ComplexAi();
        if(player==null)
            player = GameObject.FindWithTag("Player");
        rangeDistance = _rb.Distance(player.GetComponent<Collider2D>()).distance > 5;
        projDistance = _rb.Distance(player.GetComponent<Collider2D>()).distance > 10;
    }

    protected override void Chasing(GameObject player)
    {
        Vector2 v = player.transform.position;
        transform.position = Vector2.MoveTowards(transform.position, v, runSpeed * Time.deltaTime);
        _animator.SetBool("Running", true);
    }


    protected override void AiSwitchStates()
    {
        base.AiSwitchStates();

        if (Physics2D.OverlapCircle(_closeAttack.hiterGo.transform.position, _closeAttack.attackRange, _closeAttack.hiterLayer))
        {
            _animator.SetBool("Walking", false);
                    if (!_closeAttack.hasAttacked) 
                    {
                        _closeAttack.AttackCloseCombat(_animator);
                    }
                       
                    if (!Physics2D.OverlapCircle(_closeAttack.hiterGo.transform.position, _closeAttack.attackRange, _closeAttack.hiterLayer))
                    {
                        sentinelStates = enemyState.Patrolling;
                        _animator.SetBool("Walking", true);
                    }
        }

        /*
        switch (sentinelStates)
        {
                case enemyState.DetectedPlayer:
                if (base.Detection(shootDirection)){
                    if (Physics2D.OverlapCircle(shootZone.transform.position, _detectRadius, _closeAttack.hiterLayer))
                        sentinelStates = enemyState.Shooting;
                }
                    
                
                if (Physics2D.OverlapCircle(_closeAttack.transform.position, _closeAttack.attackRange, _closeAttack.hiterLayer))
                    sentinelStates = enemyState.Attacking;
                
               else if(_rb.Distance(player.GetComponent<Collider2D>()).distance > 12)
                     sentinelStates = enemyState.PlayerGoesAway;
                
                break;


              case enemyState.Teleport:
                if (_rb.Distance(player.GetComponent<Collider2D>()).distance > 15)
                        _animator.SetTrigger("Proj");
                else
                {
                    if (!Physics2D.OverlapCircle(_jumpChecker.position, 10, playerLayerMask))
                        Flip();
                        sentinelStates = enemyState.Following;
                    }
                break;




                case enemyState.PlayerGoesAway:
                if (_rb.Distance(player.GetComponent<Collider2D>()).distance > 15)
                    sentinelStates=  enemyState.Teleport;
                else
                    sentinelStates = enemyState.Following;
                break;



                case enemyState.IsAttacked:
                    StartCoroutine(TurnCountDown(1f));
                    Flip();
                break;



                case enemyState.Patrolling:
                 if (character.Health == character.TempHp && character.Health > 0)
                 {
                    _chasing = false;
                    MoveCharacter();
                    FlipAndJump();
                    if (character.Health < character.TempHp && !base.Detection(shootDirection) && !asTurnedHimself)
                        sentinelStates = enemyState.IsAttacked;
                    else if (Physics2D.OverlapCircle(shootZone.transform.position, _detectRadius, _closeAttack.hiterLayer))
                        sentinelStates = enemyState.Shooting;
                    else if (Physics2D.OverlapCircle(_closeAttack.transform.position, _closeAttack.attackRange, _closeAttack.hiterLayer))
                    sentinelStates = enemyState.Attacking;
                }
                break;

                case enemyState.Following:
                    FlipAndJump();
                    ComplexAi();
                    _chasing = true;
                    Chasing(player);
                if (Physics2D.OverlapCircle(shootZone.transform.position, _detectRadius, _closeAttack.hiterLayer))
                        sentinelStates = enemyState.DetectedPlayer;
                if (!Physics2D.OverlapCircle(shootZone.transform.position, _detectRadius, _closeAttack.hiterLayer))
                {   sentinelStates = enemyState.PlayerGoesAway;
                    if (Physics2D.OverlapCircle(_closeAttack.transform.position, _closeAttack.attackRange, _closeAttack.hiterLayer))
                    sentinelStates = enemyState.Attacking;
                
                }
                break;


                case enemyState.Shooting:
                if (_shoot.ActualAmmoInClip > 0 && _rb.Distance(player.GetComponent<Collider2D>()).distance <= _shoot.DistanceDetection )
                    AskForShot();
                else if(_rb.Distance(player.GetComponent<Collider2D>()).distance >_shoot.DistanceDetection)
                    sentinelStates = enemyState.Following;
                else if (!Physics2D.OverlapCircle(shootZone.transform.position, _detectRadius, _closeAttack.hiterLayer))
                    sentinelStates = enemyState.Following;
                break;


                case enemyState.Attacking:
                if (character.Health > 0)
                {
                        if (!_closeAttack.hasAttacked && 
                        Physics2D.OverlapCircle(_closeAttack.hiterGo.transform.position, _closeAttack.attackRange, _closeAttack.hiterLayer))
                            _closeAttack.AttackCloseCombat(_animator);

                        if (_shoot.ActualAmmoInClip > 0 && _rb.Distance(player.GetComponent<Collider2D>()).distance <= _shoot.DistanceDetection)
                            if (!Physics2D.OverlapCircle(_closeAttack.hiterGo.transform.position, _closeAttack.attackRange, _closeAttack.hiterLayer))
                                sentinelStates = enemyState.Shooting;


                    if (_rb.Distance(player.GetComponent<Collider2D>()).distance > 8)
                            sentinelStates = enemyState.PlayerGoesAway;
                }
                break;

        }
*/
    }

    protected override void JumpAi()
    {
        StartCoroutine(Jump());  
    }
    
    public void ProjectionAnim()
    {
        StartCoroutine(ProjectionCoolDown());
    }

    IEnumerator Jump() {
     
        yield return new WaitForSeconds(cAcCoolDown);  
        _animator.SetTrigger("Jump");
        _rb.AddForce(Vector2.up * _jumpForce);
    }

    IEnumerator ProjectionCoolDown() {
        
        this.transform.position = new Vector2(player.transform.position.x - 0.5f, player.transform.position.y);
        if (!Physics2D.OverlapCircle(_closeAttack.hiterGo.transform.position, _closeAttack.attackRange, _closeAttack.hiterLayer))
            Flip();

        _animator.SetTrigger("ProjAttack");
        yield return new WaitForSeconds(projCoolDown);  
    }

    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        Gizmos.DrawWireSphere(shootZone.transform.position, _detectRadius);
        //Gizmos.DrawWireSphere(_closeAttack.transform.position, _closeAttack.hiterLayer);
    }
}
