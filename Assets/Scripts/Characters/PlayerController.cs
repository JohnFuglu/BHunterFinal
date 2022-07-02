using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
[RequireComponent(typeof(HitAttack)), RequireComponent(typeof(AudioSource)), RequireComponent(typeof(AudioListener)), RequireComponent(typeof(Rigidbody2D)), RequireComponent(typeof(CapsuleCollider2D))]
public class PlayerController : Controller, IWalk, ICanBleedAndDie
{
    public delegate void OnKilled();
    public static OnKilled killedDelegate;

    protected Hero _thisHero;

    [Header("FallDamages")]
    [SerializeField]protected float _killHeight = -10f;
     bool _falled;
    
    [Header("Ground detect")]
    [SerializeField] bool grounded;
    float groundRadius = 0.2f;
    Transform _groundCheck;
    [SerializeField] LayerMask theGround;

    [Header("Stats Divers")]
    float _tempHP;
    [SerializeField] float _jumpForce = 150f;
    [SerializeField] float _boostedJumpForce = 200f;
    [SerializeField] float _runSpeed;
    [SerializeField] float _speed;


    float _xAxis;

    
    //[Header("Water")]
    public bool inWater = false;
    ParticleSystem splash;
    [SerializeField] GameObject splashGo;
    float waterdebuf = 20;

    [Header("Echelles checker")]
    public bool handsOnLadder = false;
    [SerializeField] float handsRadius = 0.2f;
    Transform handsCheck;
    [SerializeField] LayerMask ladders;
    CinemachineVirtualCamera _cam;
    //[Header("FX")]
    ParticleSystem poussiere;// _bloodParticles;
   
    [Header("Keys")]
    public string[] keys = new string[4];
    public int numKeys = 0;

    private Jaznot hero;
    private Invocator invo;
    protected bool looksRight;

    [Header("Audio")]
    [SerializeField] protected AudioClip fallSound,_splashSound, jumpSound;
    [SerializeField] protected AudioClip[] footSteps;
    [SerializeField] private AudioClip ladderSound;

    protected HitAttack _closeAttack;
    AudioClip[] IWalk.footSteps { get { return footSteps; } set { footSteps = value; } }
    AudioClip IWalk.fallSound { get { return fallSound; } set { fallSound = value; } }
    

    public float speed { get { return _speed; } set { _speed = value; } }

    public float runSpeed { get { return _runSpeed; } set { _runSpeed = value; } }

    //[Header("BloodGo")]
    [SerializeField] protected GameObject _bloodGo;
    protected ParticleSystem _bloodParticle;

    public ParticleSystem bloodParticle { get { return _bloodParticle; } set { _bloodParticle = value; } }
    public GameObject bloodGo { get { return _bloodGo; } set { _bloodGo = value; } }
    
    protected override void Start()
    {
        _cam = GameObject.Find("CameraCine").GetComponent<CinemachineVirtualCamera>();
        _cam.m_Lens.OrthographicSize = PlayerPersistentDataHandler.Instance.defaultCamOrthoSize;
        _groundCheck = GameObject.Find("GroundCheck").transform;
        splash = splashGo.GetComponent<ParticleSystem>();
        _thisHero = GetComponent<Hero>();
        _tempHP = _thisHero.Health;
        handsCheck = GameObject.Find("HandChecker").transform;
        _closeAttack = GetComponent<HitAttack>();
        _bloodParticle = _bloodGo.GetComponent<ParticleSystem>();
        poussiere = GameObject.Find("PoussiereGo").GetComponent<ParticleSystem>(); 
        base.Start();
        AssignCamera(_thisHero.name);
    }

    protected void AssignCamera(string name)
    {
        Debug.LogWarning("Assign Camera");
        _cam.Follow = this.transform;
        _cam.LookAt = this.transform;
    }

    protected void Update()
    {  
       
        if (!_thisHero.Destroyed)
        {
            WoundCheck(character);
            if (_thisHero.Health <= 0)
            { 
                _thisHero.Destroyed = true;
                _animator.SetTrigger("Dead");
               
            }


            if (!inWater && _thisHero.Health > 0)
            {
                _falled = DamageFall();
                

                Jump(_jumpForce);
            }
            if(inWater && _thisHero.Health > 0)
                Jump(_jumpForce - waterdebuf);
        }
    }

    public void HeroDeath()//Dans l'animation
    {
            _animator.SetBool("DeadBool", true);
        Dead(_thisHero);
        HeroSDead();
            PlayerPersistentDataHandler.Instance.EndLevel();
    }

    protected virtual void FixedUpdate()
    {
        if (_thisHero.Health > 0 && !grounded)
        {
                _animator.SetFloat("VSpeed", _rb.velocity.y); 
        }
        if (_thisHero.Health > 0 && grounded)
        {
                _animator.SetFloat("VSpeed", 0);
        }
        if (!_thisHero.Destroyed && _thisHero.Health>0)
        {
            handsOnLadder = Physics2D.OverlapCircle(handsCheck.position, handsRadius, ladders);

            Deplacement();


            if (!inWater)
            {
                grounded = Physics2D.OverlapCircle(_groundCheck.position, groundRadius, theGround);
                _animator.SetBool("Grounded", grounded);

     

                if (Input.GetKey(KeyCode.G) && _thisHero.SpecialAmmo > 0)//si dispo en muni
                {
                    SpecialAttack();
                }

            }
   

            if (Input.GetKeyDown(KeyCode.V))
            {
                _closeAttack.AttackCloseCombat(_animator);
            }
        }
    }

    protected virtual void SpecialAttack() { }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_rb.velocity.y > -1 && !audioSource.isPlaying)  //base devant audio et fallsound  && !audioSource.isPlaying  --_rb.velocity.y > 3 || _rb.velocity.y < -8
        { 
            audioSource.PlayOneShot(fallSound); 
        }

        if (_falled && !_thisHero.Destroyed)
        {
            Dead(_thisHero); 
            HeroSDead(); 
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Chutteur") && !_thisHero.Destroyed)
        {
            Dead(_thisHero);
            HeroSDead();
        }
    }

    void HeroSDead() 
    {
        if (UIManager.Instance._playerHeroName!="Pet") 
        { 
            if (killedDelegate != null) 
            {
                killedDelegate();
            }
        }    
    }

    void Deplacement()
    {
        _xAxis = Input.GetAxis("Horizontal");
        if (!inWater)
        {
            _rb.velocity = new Vector2(_xAxis * speed, _rb.velocity.y);
            if (_xAxis != 0 && grounded)
            {
                _animator.SetBool("IsWalking", true);
                if (!poussiere.isEmitting && grounded)
                    poussiere.Play();

                if (!grounded && poussiere.isEmitting)
                {
                    poussiere.Clear();
                    poussiere.Stop();
                }
            }

            else
            {
                _animator.SetBool("IsWalking", false);
                if (grounded)
                    poussiere.Stop();
                if (!grounded)
                {
                    poussiere.Clear();
                    poussiere.Stop();
                }
            }
        }

        if (inWater)
        {
            _rb.velocity = new Vector2(_xAxis * speed, _rb.velocity.y);   
        }


        if (Input.GetKey(KeyCode.LeftShift) && grounded & !inWater)
        {
            _rb.velocity = new Vector2(_xAxis * runSpeed, _rb.velocity.y);
            if(_rb.velocity.x!=0)
                _animator.SetBool("IsRunning", true);
            _animator.SetBool("IsWalking", false);
            if (!poussiere.isEmitting && grounded)
                poussiere.Play();

            if (!grounded && poussiere.isEmitting)
            {
                poussiere.Clear();
                poussiere.Stop();
            }
        }
        else
        {
            _animator.SetBool("IsRunning", false);
            if (_xAxis == 0)
            {
                if (grounded)
                    poussiere.Stop();
                if (!grounded)
                {
                    poussiere.Clear();
                    poussiere.Stop();
                }
            }

        }


        if (_xAxis < 0 && !looksRight) // inversé par rapport au player contro
            Flip();

        else if (_xAxis > 0 && looksRight)
            Flip();
    }

    protected virtual void Attack() //Hero playedHero
    {
    }

    void Jump(float jumpF)//grounded truc
    {
        if (Input.GetKeyDown(KeyCode.Space) && grounded && !handsOnLadder)
        {
            audioSource.PlayOneShot(jumpSound);
            _rb.AddForce(Vector2.up * jumpF,ForceMode2D.Impulse);
        }

        else if (Input.GetKeyDown(KeyCode.Space) && inWater)
        {
            _rb.AddForce(Vector2.up * jumpF, ForceMode2D.Impulse);
            audioSource.PlayOneShot(jumpSound);
            grounded = false;
        }
    }

    protected virtual void Flip()
    {
        looksRight = !looksRight;

        Vector3 flipVector = transform.localScale;
        flipVector.x *= -1;
        flipVector.z = 1;

   

        transform.localScale = flipVector;
    }

    bool DamageFall()
    {
        if (_rb.velocity.y <= _killHeight && _rb.velocity.y < 0)
        {
         //   Debug.Log("Y velocity = " + _rb.velocity.y + "  kill heigt = "+ _killHeight);
            return true;
        }
            
        else return false;
    }
    
    public void PlaySplashSound() {

        audioSource.PlayOneShot(_splashSound);
    }

    public void SplashAnim()
    {
        splashGo.SetActive(true);
        splash.Play();
    }

    public void SplashAnimStop()
    {
        splash.Stop();
        splashGo.SetActive(false);
    }


    #region("Keys")
    public void AsKey(string keyName)
    {
        bool found = false;
        for(int i = 0; i < keys.Length;i++) 
        {
            if (!found && keys[i] == "")
            {
                keys[i] = keyName;
                Debug.Log("Found " + keyName);
                found = true;
            }
        }
    }

    public bool CheckKey(string doorName)
    {
        foreach (string k in keys)
        {
            if (k == doorName)
                return true;

        }
        return false;
    }
    #endregion

    public void DisableHiter()
    {
        _closeAttack.hiterGo.SetActive(false);
        _closeAttack.hasAttacked = false;
    }

    public void PlayFootStep()
    {
        audioSource.PlayOneShot(footSteps[Random.Range(0, footSteps.Length)]);
    }

    public void PlayLadderSound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    public void WoundedBlood()
    {
        bloodGo.SetActive(true);
        audioSource.PlayOneShot(woundedSound);
        if (!bloodParticle.IsAlive())
            bloodGo.SetActive(false);
    }

    public void PlayWoundedSound()
    {
        audioSource.PlayOneShot(woundedSound);
    }

    public void PlayDeathSound()
    {
        audioSource.PlayOneShot(deathSound);
    }

    #region("Debug")
    private void OnDrawGizmosSelected()
    {
        if (_groundCheck == null)
            return;
        Gizmos.DrawWireSphere(_groundCheck.position, groundRadius);
    }
   
    #endregion
}
