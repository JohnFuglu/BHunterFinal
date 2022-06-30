using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
public class Jaznot : PlayerController
{
    //temp
    public JaznotSc jaznot;

    [Header("LanceFlamme")]
    Transform _particleTransform;
    public LanceFlamme lanceFlamme;
    [SerializeField] ParticleSystem _particle;
    [SerializeField] AudioClip _flameThSound;
    bool _shooting;
    [SerializeField] Light2D _light;

    [Header("Grenef")]   //------------------>non général
    [SerializeField] GameObject grenef;
    [SerializeField] Transform grenefLaunch;
    [SerializeField] float throwForce = 40f;

    private Hero _hero ;
    protected override void Start()
    {
        base.Start();
        _particleTransform = _particle.GetComponent<Transform>();
        _particle.Pause();
        lanceFlamme = _particle.GetComponent<LanceFlamme>();
        _hero = GetComponent<Hero>();
    }
    protected override void FixedUpdate()
    {
        if(!_thisHero.Destroyed)//&& !_shooting
            base.FixedUpdate();
        if (!_thisHero.Destroyed)
        {
            FlameThrowerAttack();
        }
    }

    protected override void Flip() //à passer sur Controller car utilisé par plusieurs
    {
        looksRight = !looksRight;

        Vector3 flipVector = transform.localScale;//Vector3 flipVector = transform.localScale;
        Vector2 flipVectorParticle = _particleTransform.localScale;
        Quaternion flipLight = _light.transform.localRotation;

        flipVector.x *= -1;//.x *= -1;
        flipVector.z = 1;

        flipVectorParticle.x *= -1;
        flipLight.z *= -1;

        transform.localScale = flipVector;
        _particleTransform.localScale = flipVectorParticle;
        _light.transform.localRotation = flipLight;
    }

    public void FlameThrowerAttack()
    {
        if (!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
        {
            if (Input.GetKey(KeyCode.LeftControl))
         {
            
                if (lanceFlamme.Fuel >= 0 + lanceFlamme.FuelConso)
                {
                    _animator.SetBool("Shoot", true);
                    _light.enabled = true;
                    lanceFlamme.LanceFlammeAttack();
                }
            }
           }
            else
            {
                StopShoot();
                _animator.SetBool("Shoot", false);
                _light.enabled = false;
            }

    }
    


    public void Shoot()
    {
        _shooting = true;
        _particle.gameObject.SetActive(true);
        _particle.Play();
        PlaySoundFlammeTh();
    }

    public void StopShoot()
    {   _shooting = false;
        _particle.Stop();
        _particle.gameObject.SetActive(false);
       
    }

    //Sounds
    public void PlaySoundFlammeTh()
    {
        if (!audioSource.isPlaying)
            audioSource.PlayOneShot(_flameThSound);
    }

    protected override void SpecialAttack()
    {
        _animator.SetTrigger("Grenef");
    }


    public void Grenef()
    {
        if (_hero.SpecialAmmo > 0)
        {
            GameObject gr = Instantiate(grenef, grenefLaunch.position, Quaternion.identity);
            Rigidbody2D grRb = gr.GetComponent<Rigidbody2D>();

            if (looksRight)
                grRb.AddForce(Vector2.right * throwForce);
            else
                grRb.AddForce(Vector2.left * throwForce);
            _hero.SpecialAmmo --;
        }

    }
}
