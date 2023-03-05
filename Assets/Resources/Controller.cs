using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class Controller: MonoBehaviour
{
    protected bool _playerDead = false;
    protected Animator _animator;// virtual
    protected Rigidbody2D _rb;
    protected Collider2D _collider;
    #region("Sounds')
    [Header("Sounds")]
    protected AudioSource audioSource;
    
    [SerializeField] protected AudioClip deathSound,woundedSound, attackSound;
    #endregion

    [Header("Base class stats holder")]
    public StandardObject character;


    [Header("Shared combat and detection")]
    protected GameObject target;
    protected bool detectSomething=false;
    protected RaycastHit2D hit;
    protected ShootSystem _shoot;
    protected List<Rigidbody2D> multipleToPush;
    private string[] heroNames = { "Jaznot", "Royale" };          
    protected virtual void Start()
    {
        target = GameObject.FindWithTag("Player");
        audioSource = GetComponent<AudioSource>();
        _shoot = GetComponent<ShootSystem>();//va générer erreur si pas là switch av attackCAc
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _collider= GetComponent<Collider2D>();
       if(!gameObject.CompareTag("Player")) 
            PlayerController.killedDelegate += PlayerDead;
    }

    #region("Comportement General")




    protected void WoundCheck(StandardObject objectS)
    {
        if (objectS.Health < objectS.TempHp && objectS.Health > 0)
        {
            Wounded();
        }
        objectS.TempHp = objectS.Health;
    }

    protected virtual void DeathCheck(StandardObject objectS) 
    {
      if (objectS.Health <= 0 && !objectS.Destroyed)
      {
            character.Destroyed = true;
            _animator.SetTrigger("Dead");
            if (!heroNames.Contains(character.name)) 
            {
                UpdateScore();
            }           
           StartCoroutine(SetDeadInactive(4.0f));
      }
    }
    #endregion



    protected void PlayerDead()
    {
        _playerDead = true;
    }



    #region ("Animation")
    public void ShootAnimation()
    {
        _shoot.Shoot(target.transform.position);
    }

    public void ShootDumbAnimation()
    {
        _shoot.ShootDumb();
    }
    public void AttackPlaySound() 
    {
        audioSource.PlayOneShot(attackSound);
    }
    public void ShootPlaySound() 
    {audioSource.PlayOneShot(_shoot.shootSound);}
    public void ReloadPlaySound() 
    {
        audioSource.PlayOneShot(_shoot.reloadSound);
    }
    public void ShootDumbBurstAnimation()
    { 
        _shoot.ShootDumb();
    }
    public void ShootAuto() {
        _shoot.ShootAuto(target.transform.position);
    }

    public void ShootBurstAnimation() 
    {
        _shoot.ShootBurst(target.transform.position);
    }

    public void ReloadAnimation()
    {
        _shoot.Reload();  
    }

    public void HitAnimation()
    {
        GetComponent<HitAttack>().Push<SentinelControler>(GetComponent<HitAttack>().toPush);
    }


    #endregion
    public void Wounded() //ok
    {
        _animator.SetTrigger("Wounded");
    }
    public void Dead<T>(T obj) where T : StandardObject
    {
        if (!obj.Destroyed)
        {
            _animator.SetBool("DeadBool", true);
            audioSource.PlayOneShot(deathSound);
            _animator.SetTrigger("Dead");      
            obj.Destroyed = true;
        }

    }

    protected virtual bool Detection(Vector3 detecDirection)
    {

        hit = Physics2D.Raycast(_shoot.canon.position, detecDirection, _shoot.DistanceDetection);
        Debug.DrawRay(_shoot.canon.transform.position, detecDirection, Color.red);
        detectSomething = hit;

        if (detectSomething)
        {
            if (hit.collider != null && hit.transform.CompareTag("Player"))//à rendre modulable
            {
                Debug.Log("assigning" + target);
                target = hit.collider.gameObject;
                return true;
            }
      
            return false;
        }
        return false;
    }

   

    protected void AskForShot() 
    {
        _animator.SetTrigger("Shoot");
    }

    protected IEnumerator SetDeadInactive(float timer)
    {
        yield return new WaitForSeconds(timer);
        gameObject.SetActive(false);
    }

    void UpdateScore()
    {
        PlayerPersistentDataHandler.Instance.PlayerScore += character.Score;
    }
}
