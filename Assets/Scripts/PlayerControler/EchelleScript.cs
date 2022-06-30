using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchelleScript : MonoBehaviour
{
    private Rigidbody2D playerRb;
    private float bouncinessDefault, frictionDefault;
    private Animator playerAnim;
    private const float speedClimb=7.5f;
    private PlayerController cControler;  //  private CharacterControler cControler;
    private void Start()
    {
        GameObject playerGo = GameObject.FindWithTag("Player");
        playerRb = playerGo.GetComponent<Rigidbody2D>();
        playerAnim = playerGo.GetComponent<Animator>();
        cControler = playerGo.GetComponent<PlayerController>();
        bouncinessDefault = playerRb.sharedMaterial.bounciness;
        frictionDefault = playerRb.sharedMaterial.friction;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Invocator") 
        {
            playerAnim.SetTrigger("DeployWings");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")&&cControler.handsOnLadder)
        {
            playerAnim.SetBool("Ladder", true);
            playerRb.sharedMaterial.bounciness = 0;
            playerRb.sharedMaterial.friction = 0;
            playerRb.velocity = new Vector2(0, 1.5f);// 0,0 pr tomber


            if (Input.GetKey(KeyCode.Space) && Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
            {
                Vector2 jumpLadder = new Vector2(collision.attachedRigidbody.velocity.x, 1);
                playerRb.AddForce(jumpLadder * 50f, ForceMode2D.Impulse);
            }

            if (Input.GetKey(KeyCode.UpArrow))
            {
                playerAnim.SetBool("Ladder", false);
                playerAnim.SetBool("Climbing", true);
                playerRb.sharedMaterial.bounciness = 0;
                playerRb.sharedMaterial.friction = 0;
                playerRb.velocity = new Vector2(0, speedClimb);
            }

            else if (Input.GetKey(KeyCode.DownArrow))
            {
                playerAnim.SetBool("Ladder", false);
                playerAnim.SetBool("Climbing", true);
                playerRb.sharedMaterial.bounciness = 0;
                playerRb.sharedMaterial.friction = 0;
                playerRb.velocity = new Vector2(0, -speedClimb);
            }

            else if(Input.GetKey(KeyCode.LeftArrow)|| Input.GetKey(KeyCode.RightArrow))
            {
                playerAnim.SetBool("Ladder", false);
                playerAnim.SetBool("Climbing", false);
            }
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        playerAnim.SetBool("Ladder", false);
        playerAnim.SetBool("Climbing", false);
        if (collision.name == "Invocator")
        {
            playerAnim.SetTrigger("DeployWings");
        }
        playerRb.sharedMaterial.friction = frictionDefault;
        playerRb.sharedMaterial.bounciness = bouncinessDefault;
    }

}
/*
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")&&Input.GetKey(KeyCode.UpArrow))
        {
            playerAnim.SetBool("Ladder", false);
            playerAnim.SetBool("Climbing", true);
            playerRb.sharedMaterial.bounciness = 0;
            playerRb.sharedMaterial.friction = 0;
            playerRb.velocity = new Vector2(0, speedClimb);
        }
        else if (collision.CompareTag("Player") && Input.GetKey(KeyCode.DownArrow))
        {
            playerAnim.SetBool("Ladder", false);
            playerAnim.SetBool("Climbing", true);
            playerRb.sharedMaterial.bounciness = 0;
            playerRb.sharedMaterial.friction = 0;
            playerRb.velocity = new Vector2(0, -speedClimb);

        }
        else if (collision.CompareTag("Player"))
        {
            playerAnim.SetBool("Ladder", true);
            playerRb.sharedMaterial.bounciness = 0;
            playerRb.sharedMaterial.friction = 0;
            playerRb.velocity = new Vector2(0, 0);// 0,0 pr tomber
        }
    }
    
     

     
     */
