using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField] GameObject waterSpash;
    [SerializeField] BuoyancyEffector2D water;
    [SerializeField] float playerCoule;
    [SerializeField] float massFactor = 0.5f;

    private GameObject player;
    private Animator playerAnim;
    //  CharacterControler playerCC;
    PlayerController playerCC;
    private float moving;
    private float waterDensity;

//    private ObjectPooler poolWaterSplash;
    protected void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerAnim = player.GetComponent<Animator>();
        //playerCC=player.GetComponent<CharacterControler>();
        playerCC = player.GetComponent<PlayerController>();
        waterDensity = GetComponent<BuoyancyEffector2D>().density;
    
    }
    protected virtual void OnTriggerStay2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
        {

             playerCC.inWater = true;
             playerAnim.SetBool("InWater", true);

            moving = (Input.GetAxis("Horizontal"));
            /*pour couler si on avance pas
            if (moving == 0)
            {
                water.density= playerCoule;
            }
            else water.density = waterDensity;*/
        }
       
    }
    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
    
      //  WaterSplash(collision);
        if (collision.CompareTag("Player")) {
            StartCoroutine(OnLand(0.3f));
            collision.GetComponent<Rigidbody2D>().mass=  playerCoule;
        }
     
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerCoule = collision.GetComponent<Rigidbody2D>().mass;
            collision.GetComponent<Rigidbody2D>().mass *= massFactor;
         //   WaterSplash(collision);
            playerAnim.SetBool("InWater", true);
        }
       
    }
    protected IEnumerator OnLand(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        playerCC.inWater = false;
        playerAnim.SetBool("InWater", false);
    }

    protected void WaterSplash(Collider2D collision)
    {
      //  poolWaterSplash.SpawnFromPool("SplashWater", collision.transform.position, Quaternion.identity,Vector2.zero,0f,0);
    }

}
