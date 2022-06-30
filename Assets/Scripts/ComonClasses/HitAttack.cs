using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitAttack : MonoBehaviour // classe generic de type character
{

    [SerializeField] float _hitForce;
   // AudioSource _audioSource;

    public LayerMask hiterLayer;
    public float attackRange;
    public GameObject hiterGo;
    public bool hasAttacked = false;
    public float ccCoolDown = 1.2f;
    public Rigidbody2D toPush;


    //windows
    List<Rigidbody2D> _contactRigidBodies = new List<Rigidbody2D>();
    GameObject _gOToSpawnOn;
    [SerializeField] AudioClip _glassSound1, _glassSound2;
    
    private int _damage;
   
        private void Start()
        {
            _damage = GetComponentInParent<Character>().Damage;
          //  _audioSource = GetComponentInParent<AudioSource>();

}

    public void Push<T>(Rigidbody2D rb) where T: TurretControler
    {
        Debug.Log("Pusher  = "+this.gameObject.name);
        if (TryGetComponent(out T senti))
        {
            if (senti.looksRight)
            {
                Debug.Log("Senti LookRight");
                float randomInt = Random.Range(0.01f, 0.05f);
                Vector2 rightAndRandomY = new Vector2(1f, randomInt);


                if (rb.TryGetComponent(out Character character))
                {
                    Debug.Log("character");
                    character.TakeDamage(_damage);
                    character.gameObject.GetComponent<PlayerController>().WoundedBlood();//temporaire
                }
                rb.AddForce(rightAndRandomY * _hitForce);
                if (rb.TryGetComponent(out StandardObject destroyableObject))
                {
                    Debug.Log("standart");
                    destroyableObject.TakeDamage(_damage);
                }
                else
                {
                    Debug.Log("autre cas");
                    rb.AddForce(Vector2.up, ForceMode2D.Impulse);
                }
            }
           
            else
            {
                Debug.Log("LookLeft");
                float randomInt = Random.Range(0.1f, 0.5f);
                Vector2 rightAndRandomY = new Vector2(-1f, randomInt);
                
                rb.AddForce(rightAndRandomY * _hitForce);
                if (rb.isKinematic)
                    rb.isKinematic = false;  // coroutine?


                if (rb.TryGetComponent(out StandardObject destroyableObject))
                {
                    destroyableObject.TakeDamage(_damage);
                }
            }
        }
        else
        {
            Debug.Log("pas turret controller");
            rb.AddForce(Vector2.up*10, ForceMode2D.Impulse); 
            if (rb.TryGetComponent(out StandardObject std ))
            {
                std.TakeDamage(_damage);
            }

        }

       

    }

    public void AttackCloseCombat(Animator animator)
    {
        Debug.Log("Attack");
        hiterGo.gameObject.SetActive(true);
        animator.SetTrigger("AttackKnife");
        if (Physics2D.OverlapCircle(hiterGo.transform.position, attackRange, hiterLayer))
        {
            if (!hasAttacked)
            {
               // animator.SetTrigger("AttackKnife");
                toPush = Physics2D.OverlapCircle(hiterGo.transform.position, attackRange, hiterLayer).GetComponent<Rigidbody2D>();
                hasAttacked = true;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (hiterGo == null)
            return;
        Gizmos.DrawWireSphere(hiterGo.transform.position, attackRange);

    }
}
/*
    private void OnDrawGizmosSelected()
    {
        if (attackPointR == null)
            return;
        Gizmos.DrawWireSphere(attackPointR.position, attackRange);
        //Gizmos.DrawWireSphere(attackPointL.position, attackRange);
    }

    public void MakeDamage() //gere l'anim
    {
        if (Physics2D.OverlapCircle(attackPointR.position, attackRange, playerLayer) && !player.mort)
        {
            switch (namePlayer)
            {
                case "Jaznot":
                    Debug.Log("Jaznot est atttaqué");
                    player.jaznot.vie -= thisEnnemi.Damage;
                    break;
                    //à enrichir avec autres noms;
            }

        }

    }
    */

/*     [Header("DetectAndHit")]
    [SerializeField] Transform _attackPointR;
    [SerializeField] float _attackRange;
    [SerializeField] int _hitForce;
    public LayerMask playerLayer;
    private Character _thisHiter;
    private Controller _hiterControler;

 *    
 *    
 *    
 *    
 *    private void Start()
{
    _thisHiter= GetComponentInParent<Character>();//generic ?

    if (TryGetComponent(out Controller controller))
        _hiterControler = GetComponentInParent<Controller>();
    if (TryGetComponent(out SentinelControler sentinelController))
        _hiterControler = GetComponentInParent<SentinelControler>();
}

private void Update()
{
    if (Physics2D.OverlapCircle(_attackPointR.position, _attackRange, playerLayer)) 
    {
        AttackCloseCombat(Physics2D.OverlapCircle(_attackPointR.position, _attackRange, playerLayer).gameObject);


    }
}



void AttackCloseCombat (GameObject collided) 
{
    if (!_thisHiter.Destroyed)//check generic
    {
        if (collided.GetComponent<StandardObject>()) 
        {
            StandardObject _genericObject = collided.GetComponent<StandardObject>();
            _genericObject.TakeDamage(_thisHiter.Damage);
            Push(collided.GetComponent<Rigidbody2D>());
        }
        else if (collided.GetComponent<Character>())
        {
                Character _genericCharacter = GetComponent<Character>();
                _genericCharacter.TakeDamage(_thisHiter.Damage);
                Push(collided.GetComponent<Rigidbody2D>());
        }
    }

}
void Push(Rigidbody2D rb) 
{ 
    if(TryGetComponent(out Controller controller))
    { 
       if(controller.lookRight) 
        {
            float randomInt =Random.Range(5f, 10f);
            Vector2 rightAndRandomY = new Vector2(1f, randomInt);
            rb.AddForce(Vector2.right * _hitForce);
        }
        else
        {
            float randomInt = Random.Range(5f, 10f);
            Vector2 rightAndRandomY = new Vector2(1f, randomInt);
            rb.AddForce(Vector2.left * _hitForce);
        }

    }
}

private void OnDrawGizmosSelected()
{
    if (_attackPointR == null)
        return;
    Gizmos.DrawWireSphere(_attackPointR.position, _attackRange);
}
*/
