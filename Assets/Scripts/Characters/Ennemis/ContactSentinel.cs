using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactSentinel : SentinelControler
{
  
    protected override void Start()
    {
        base.Start();
    }


    protected override void AiSwitchStates()
    {
        if (!character.Destroyed && !_playerDead)
        {
            switch (sentinelStates)
            {
                case enemyState.Patrolling:
                    if (character.Health == character.TempHp && character.Health > 0)
                    {
                        if (!Physics2D.OverlapCircle(_closeAttack.hiterGo.transform.position, _closeAttack.attackRange, _closeAttack.hiterLayer))
                        {  
                            ComplexAi();
                            _closeAttack.hasAttacked = true; // reset if player cancel the attack
                            _closeAttack.hasAttacked = false;
                        }


                        else if (Physics2D.OverlapCircle(_closeAttack.hiterGo.transform.position, _closeAttack.attackRange, _closeAttack.hiterLayer))
                        {
                            sentinelStates = enemyState.Attacking;
                        }
                    }
                    //test de se retourner si attaqué
                    if (character.Health < character.TempHp && !Physics2D.OverlapCircle(_closeAttack.hiterGo.transform.position, _closeAttack.attackRange, _closeAttack.hiterLayer) && character.Health > 0)
                    {
                        if (!asTurnedHimself)
                        {
                            Debug.Log("Je me retounre 2");
                            Flip();
                            StartCoroutine(TurnCountDown(1f));
                        }
                    }
                    break;

                case enemyState.Attacking:
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
                    break;
            }


        }
       
    }
  
}
