using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pet : PlayerController
{
    [SerializeField] ParticleSystem particle;
    private static Pet _instance;
    public static Pet Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("No Pet !!!!");
            return _instance;
        }
    }



    [Header("PetSpecials")]
    [SerializeField] AudioClip _petSendBackSound;
    [SerializeField] float _evadeJumpForce, _hpLeach;
    [SerializeField] int _evadeExposer = 10;
    [SerializeField] ParticleSystem _leachFxPrefab;
    ParticleSystem _leachFx;
    bool _fx;
    Hero _thisPetHero;
    HitAttack _hitAttack;


    public Hero invocator;
    public int EvadeCharges { get { return _evadeExposer; }protected set { value = _evadeExposer; } }
    public int TeleportationCharges
    {
        get
        {
            return 1;
        }
    }
    Invocator _player;
    
    
    private void OnEnable()
    {
        _instance = this; 
        base.Start();
        _player = GameObject.Find("Invocator").GetComponent<Invocator>();
        _hitAttack = GetComponent<HitAttack>();
        _thisPetHero  =GetComponent<Hero>();
    }
    protected override void FixedUpdate() 
    {
        if (_player.petIsHere && !_thisPetHero.Destroyed)
        {
            base.FixedUpdate();
            if (Input.GetKeyDown(KeyCode.LeftControl) && EvadeCharges > 0)
                _animator.SetTrigger("Shoot");
            if (_thisPetHero.Health <= 0 && !_animator.GetBool("DeadBool") || Input.GetKeyDown(KeyCode.J) && !_animator.GetBool("DeadBool"))
            {
              
                DeathPet();
            }  
        }
    }

    protected override void SpecialAttack()
    {
        _animator.SetTrigger("Teleportation");
    }

    void TeleportationAnimation() 
    {
        Invocator.Instance.transform.position = gameObject.transform.position;
        particle.Play();
        Invocator.Instance.InvocationEnd(this.gameObject);
    }

    void DeathPet() 
    {
         _animator.SetTrigger("Dead");
         _animator.SetBool("DeadBool", true);
         audioSource.PlayOneShot(deathSound);
    }

    public void DeathPetAnimation() { Invocator.Instance.InvocationEnd(this.gameObject); }
    void Evade(bool looksRight) 
    {
        _evadeExposer--;
        if (!looksRight)
            _rb.AddForce(Vector2.right *_evadeJumpForce);
        else
            _rb.AddForce(Vector2.left * _evadeJumpForce);
    }

    public void PlayTpSound() {
        audioSource.PlayOneShot(_petSendBackSound);
    }

    public void EvadeInvulnStart() 
    {
        EvadeFx(true);
        gameObject.layer = 22;
    }
    public void EvadeAnimation() 
    {
        Evade(looksRight);
    }
    public void EvadeInvulnStop() 
    {
        EvadeFx(false);
        gameObject.layer = 10;
    }

    void EvadeFx(bool evadeFxNeeded) 
    {
        
    }

    public void HpLeach() 
    {
        if (_hitAttack.toPush.CompareTag("Ennemis"))
        {
            int rd = Random.Range(1, 6);
            if (rd == 2)
            {
                HpLeachFx();
                invocator.Health += _hpLeach;
            }
        }
       
    }
    void HpLeachFx() 
    {
        if (!_fx) 
        {
            _leachFx = Instantiate(_leachFxPrefab);
            _leachFx.transform.position = new Vector2(this.transform.position.x, this.transform.position.y+5);
            _leachFx.Play();
            _fx = true;
        }
    }
}
