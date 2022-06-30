using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField] GameObject waterSpash;
    [SerializeField] BuoyancyEffector2D water;
    [SerializeField] float playerCoule;

    private GameObject player;
    private Animator playerAnim;
    //  CharacterControler playerCC;
    PlayerController playerCC;
    private float moving;
    private float waterDensity;

//    private ObjectPooler poolWaterSplash;
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerAnim = player.GetComponent<Animator>();
        //playerCC=player.GetComponent<CharacterControler>();
        playerCC = player.GetComponent<PlayerController>();
        waterDensity = water.density;
    
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
        {

             playerCC.inWater = true;
             playerAnim.SetBool("InWater", true);

            moving = (Input.GetAxis("Horizontal"));
            if (moving == 0)//pour couler si on avance pas
            {
                water.density= playerCoule;
            }
            else water.density = waterDensity;
        }
       
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
    
      //  WaterSplash(collision);
        if (collision.CompareTag("Player")) {
            StartCoroutine(OnLand(0.3f));
        }
     
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
         //   WaterSplash(collision);
            playerAnim.SetBool("InWater", true);
        }
       
    }
    private IEnumerator OnLand(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        playerCC.inWater = false;
        playerAnim.SetBool("InWater", false);
    }

    void WaterSplash(Collider2D collision)
    {
      //  poolWaterSplash.SpawnFromPool("SplashWater", collision.transform.position, Quaternion.identity,Vector2.zero,0f,0);
    }

}
