using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invocator : PlayerController
{
   
    [Header("Invocator spécials")]
    [SerializeField] AudioClip _invocSound;
    AudioListener _listner;
    [SerializeField] ParticleSystem _invocFx;
    [Header("InvocationSpecial")]
    [SerializeField] GameObject _petPrefab;
    [SerializeField] float petLifeTime;
    public Transform heroPosition;
    public Transform petPosition;
    public bool petIsHere = false;
    [SerializeField] float _invocCost;
    public Pet _pet;

    Camera _mainCamera;
    public InvoqSc invocatorSc;

    private Hero _hero;
 
    protected override void Start()
    {
        base.Start();
        _hero = GetComponent<Hero>();
        _listner = GetComponent<AudioListener>();
    }
    protected override void FixedUpdate()
    {
        if (!petIsHere)
        {
            base.FixedUpdate();
            if (Input.GetKeyDown(KeyCode.LeftControl) && _shoot.ActualAmmoInClip>0)
                _animator.SetTrigger("Shoot");     
        } 
    }

    public void InvocatorShootAnimation() 
    {
        _shoot.ShootHero(looksRight);
    }

    protected override void SpecialAttack() 
    {
        if (_thisHero.Health > 0 + _invocCost && !petIsHere)
        {  
            _animator.SetTrigger("Invocation");
            _animator.SetBool("OutOfBody", true);
            this.tag = "OutOfGame";
            petIsHere = true;
            _thisHero.Health -= _invocCost;
            GetComponent<Hero>().SpecialAmmo--;
        }
    }

    public void InvocAnimation() 
    { 
        StartCoroutine(InvocTimer(petLifeTime));
        _listner.enabled = false;
        GameObject pet = Instantiate(_petPrefab);
        pet.GetComponent<Pet>().invocator = _thisHero;
        pet.name = "Pet";
        pet.transform.position = petPosition.position;
        AssignCamera("Pet");
        _rb.simulated=false;
        GameObject.Find("Canvas").GetComponent<UIManager>()._playerHeroName = "Pet";
        _hero.enabled = false;
        _pet = pet.GetComponent<Pet>();
        _pet.invoc = GetComponent<Invocator>();
    }
    public IEnumerator InvocTimer(float timer) {
        yield return new WaitForSeconds(timer);
        InvocationEnd(GameObject.Find("Pet"));
    }
    void Heal(float healthKit) 
    {
        _thisHero.Health += healthKit;
    }
    public void InvocationEnd(GameObject pet) 
    {
        petIsHere = false;
        this.tag = "Player";
        _listner.enabled = true;
       _invocFx.Play();
        _animator.SetTrigger("BackToBody");
        _animator.SetBool("OutOfBody",false);
        GameObject.Find("Canvas").GetComponent<UIManager>()._playerHeroName = "Invocator";
        GameObject.Find("Canvas").GetComponent<UIManager>().DisplayBackInvocatorHud();
        AssignCamera("Invocator");
        _rb.constraints = RigidbodyConstraints2D.FreezePositionY;
        _rb.simulated = true; 
        _rb.constraints = RigidbodyConstraints2D.None;
        _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        keys = _pet.keys;
        numKeys = _pet.numKeys;
        Destroy(pet,1f);
        
        _hero.enabled = true;
    }
}
