using System.Collections;
using UnityEngine;

public class SentinelControler : TurretControler, IWalk, ICanBleedAndDie
{
    #region("AI states")
    [SerializeField]protected enum enemyState
    {
        Patrolling,
        Attacking,
        Shooting,
        IsAttacked,
        Following,
        Reloading,
        DetectedPlayer,
        PlayerGoesAway,
        Teleport
    }
    [SerializeField] protected enemyState sentinelStates;
    #endregion

    [Header("Deplacements")]
    public float speed;
    protected float _speedTemp;
    public float runSpeed;
    protected Vector2 _startPos;
    protected Vector2 _startDirection;
    protected Vector3 flipVector;
    [SerializeField]protected float _detectRadius;
    [SerializeField]protected Transform eyeSight;
   
    [SerializeField]protected LayerMask playerLayerMask;
    
    [Header("Ai")]
    [SerializeField] protected Transform _fallChecker;
    [SerializeField] protected Transform _wallChecker;
    [SerializeField] protected Transform _jumpChecker;
    [SerializeField] protected Transform _floorChecker;
    [SerializeField] protected Vector2 _fallBoxSize;
    [SerializeField] protected Vector2 _floorBoxSize;
    [SerializeField] protected Vector2 _jumpBoxSize;
    [SerializeField] protected Vector2 _wallBoxSize;
    [SerializeField] protected int _jumpForce;
    [SerializeField] protected LayerMask jumpLayerMask;
    [SerializeField] protected LayerMask wallLayerMask;
    [SerializeField] bool onGround, _collidesUp,_collidesDown,_canAdvance;
   // Color defaultColor;
    protected  bool _chasing;
 //   Rigidbody2D _rb;




    [Header("Attack and Shoot")]
    [SerializeField] Vector2 _range;
    protected bool _hasToShoot, _walking, _reloading = false;



    [Header("Hiter")]
    protected HitAttack _closeAttack;

    //sound
    [SerializeField] protected AudioClip[] footSteps;
    [SerializeField] protected AudioClip fallSound;

    //Particles blood
    [SerializeField] protected GameObject bloodGo;
    [SerializeField] protected ParticleSystem bloodParticle;

    protected bool asTurnedHimself = false;

    AudioClip[] IWalk.footSteps { get { return footSteps; }set { footSteps = value ; } }
    AudioClip IWalk.fallSound { get { return fallSound; } set { fallSound = value; } }
    float IWalk.speed { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    float IWalk.runSpeed { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    ParticleSystem ICanBleedAndDie.bloodParticle { get { return bloodParticle; } set { bloodParticle = value; } }
    GameObject ICanBleedAndDie.bloodGo { get { return bloodGo; } set { bloodGo = value; } }
    protected GameObject player;

    protected override void Start()
    {
        base.Start();

        _closeAttack = GetComponent<HitAttack>();
        shootDirection = _range;
        _startPos = transform.position;
        sentinelStates = enemyState.Patrolling;
        _speedTemp = speed;
        character.TempHp = character.Health;
        
        if (looksRight)
        _startDirection = Vector2.right;
        else
        _startDirection = Vector2.left;

        flipVector = _startDirection;
     //   bloodParticle = bloodGo.GetComponent<ParticleSystem>();
       // defaultColor = GetComponent<SpriteRenderer>().color;
        _rb = GetComponent<Rigidbody2D>();
    }

    public virtual void Update()
    {
        if (!character.Destroyed && !_playerDead)
        {
            AiSwitchStates();
            WoundCheck(character);
            DeathCheck(character);
            //   if (character.Health <= 0)    /// Remplacer par munitions ?
            //   {
            //    character.GiveHint(gameObject);
            //   }
        }
        else
        {
            _rb.mass *= 100;
            _animator.SetBool("DeadBool", true);
        }
    }


    protected virtual void MoveCharacter()
    {
      
        transform.Translate(_startDirection * Time.deltaTime * speed);
        _animator.SetBool("Walking", true);
        _walking = true;
    }

    protected virtual void Flip()
    {
        flipVector = transform.localScale;
        flipVector.x *= -1;
        transform.localScale = flipVector;
     
        speed = -speed;
        runSpeed = -runSpeed;
        looksRight = !looksRight;
        shootDirection *= -1;
    }

    protected virtual void AiSwitchStates()
    {

            switch (sentinelStates)
            {
                case enemyState.Patrolling:
                if (character.Health == character.TempHp && character.Health > 0 && _shoot.ActualAmmoInClip > 0)
                {
                    
                    ComplexAi();
                }
                    if (base.Detection(shootDirection) && _shoot.ActualAmmoInClip > 0)
                    {
                        sentinelStates = enemyState.Attacking;
                    }
                    //test de se retourner si attaqué
                    if(character.Health < character.TempHp && !base.Detection(shootDirection) && _shoot.ActualAmmoInClip > 0 && !asTurnedHimself) 
                    {
                        StartCoroutine(TurnCountDown(1f));
                        Flip();
                    }
                    break;

                case enemyState.Attacking:
                   if(character.Health > 0) 
                    {
                    if (_shoot.ActualAmmoInClip > _shoot.burstNbr)
                          base.AskForShot();
                        if (_shoot.ActualAmmoInClip <= 0 && _shoot.Ammo>0)//0+base.burstNumber    && _shoot.weaponReloaded
                        {
                            sentinelStates = enemyState.Reloading;
                        }
                    }
                    break;
                    
                    

                case enemyState.Reloading:
                    if (character.Health > 0)
                    {
                        if (_shoot.Ammo > 0 && _shoot.ActualAmmoInClip < _shoot.AmmoMaxInClip)
                        {
                            if (_animator.GetBool("Reload") != true)
                            {
                                _animator.SetBool("Reload", true);
                                sentinelStates = enemyState.Patrolling;
                            }
                        }
                        else
                            sentinelStates = enemyState.Patrolling;
                    }
                    break;
            }
        
    }

    public void DisableHiter()
    {
        StartCoroutine(CCAttackCoolDown(()=>
        {
            _closeAttack.hiterGo.SetActive(false);
            _closeAttack.hasAttacked = false;
        }));
     
    }

    public void WoundedBlood()
    {
        bloodGo.SetActive(true);
        audioSource.PlayOneShot(woundedSound);
        if (!bloodParticle.IsAlive())
            bloodGo.SetActive(false);
    }


    #region("SoundsInAnimationEvents)
    public void PlayFootStep()
    {
        audioSource.PlayOneShot(footSteps[Random.Range(0, footSteps.Length)]);
    }

    public void PlayWoundedSound()
    { audioSource.PlayOneShot(woundedSound); }
    public void PlayDeathSound()
    { audioSource.PlayOneShot(deathSound); }
    #endregion


    public IEnumerator CCAttackCoolDown(System.Action OnComplete= null) 
    {
        yield return new WaitForSeconds(_closeAttack.ccCoolDown);
        if(OnComplete!=null)
             OnComplete();
    }

    public IEnumerator ReloadCoolDown(System.Action OnComplete = null) 
    {
        yield return new WaitForSeconds(2f);
        if (OnComplete != null)
            OnComplete();
    }

    protected IEnumerator TurnCountDown(float timer)
    {
        asTurnedHimself = true;
        yield return new WaitForSeconds(timer);
        asTurnedHimself = false;
    }

    protected virtual void ComplexAi() 
    {
        // si le player passe dans le champ de vision
        //l'ennemi va chercher à se rapprocher de sa position
        //si le player part à une distance D la chasse s'arrete
       player = GameObject.FindWithTag("Player");
       
        if (Physics2D.OverlapCircle(eyeSight.position, _detectRadius, playerLayerMask))
        {   
            _chasing = true; 
        }
        if (_rb.Distance(player.GetComponent<Collider2D>()).distance > 10)
        {
             _chasing = false;
        }
            

        if (_chasing) 
        {   
            Chasing(player);
            if (!Physics2D.OverlapCircle(_closeAttack.hiterGo.transform.position, _closeAttack.attackRange, _closeAttack.hiterLayer)) {
                Debug.Log("Je me retourne");
            FlipAndJump();
            }
                
        }

        else
        {
          // GetComponent<SpriteRenderer>().color = defaultColor;
            MoveCharacter(); 
            FlipAndJump();
        }
       
    }

    protected virtual void Chasing(GameObject player)
    {
        Vector3 v = player.transform.InverseTransformPoint(transform.position); //positif à gauche negatif droite
        if (v.x > 0 && looksRight) {
            //Debug.Log("Right " + "v =" +v.x);
            if(!Physics2D.OverlapCircle(eyeSight.position, _detectRadius, playerLayerMask))
                Flip(); 
        }
        if (v.x < 0 && !looksRight) 
        {
          //  Debug.Log("Gauche " + "v =" + v.x);
            if (!Physics2D.OverlapCircle(eyeSight.position, _detectRadius, playerLayerMask))
                Flip();  
        }

        if(player.GetComponent<Rigidbody2D>().velocity == Vector2.zero) 
        { 
            _animator.SetBool("Walking", false); //run...
            _walking = false;
        }

        MoveCharacter();
        _animator.SetBool("Walking", true); //run...
        _walking = true;
    }
   

    protected void FlipAndJump() 
    {
      
        onGround = Physics2D.OverlapBox(_floorChecker.position, _floorBoxSize, jumpLayerMask);
        _collidesUp = Physics2D.OverlapBox(_jumpChecker.position, _jumpBoxSize, wallLayerMask);
        _collidesDown = Physics2D.OverlapBox(_wallChecker.position, _jumpBoxSize, wallLayerMask);
        _canAdvance = Physics2D.OverlapBox(_fallChecker.position, _fallBoxSize, wallLayerMask);

        if (_collidesUp && onGround)
        {
         
            Flip();
        }
        if (!_collidesUp && _collidesDown && onGround && _canAdvance)
        {
       
            JumpAi();
        }

        if (!_canAdvance && onGround)
        {
           
            Flip();
        }
    }

    protected virtual void JumpAi() 
    {
        _rb.AddForce(Vector2.up * _jumpForce);
    }


    protected new virtual void OnDrawGizmosSelected()
    {
        if (_fallChecker == null && _floorChecker && _wallChecker && _jumpChecker == null)
            return;
        Gizmos.color = new Color (1,0,0,0.5f); 
        Gizmos.DrawCube(_fallChecker.position,_fallBoxSize);
        Gizmos.DrawCube(_floorChecker.position, _floorBoxSize);
        Gizmos.DrawCube(_wallChecker.position, _wallBoxSize);
        Gizmos.DrawCube(_jumpChecker.position, _jumpBoxSize);
        Gizmos.color = new Color(0, 0, 1, 0.5f);
        if(gameObject.name!="Cani")
            Gizmos.DrawWireSphere(eyeSight.position, _detectRadius);
    }
}
