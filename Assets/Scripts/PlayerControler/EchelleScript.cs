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
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")){
            GameObject playerGo = GameObject.FindWithTag("Player");
            playerRb = playerGo.GetComponent<Rigidbody2D>();
            playerAnim = playerGo.GetComponent<Animator>();
            cControler = playerGo.GetComponent<PlayerController>();
            if (collision.name == "Invocator") 
        {
            playerAnim.SetTrigger("DeployWings");
        }
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")&&cControler.handsOnLadder)
        {
            playerAnim.SetBool("Ladder", true);
            if (Input.GetKey(KeyCode.Space))
            {
                Vector2 jumpLadder = new Vector2(collision.attachedRigidbody.velocity.x, 1);
                playerRb.AddForce(jumpLadder * 5f, ForceMode2D.Impulse);
            }

            if (Input.GetKey(KeyCode.UpArrow))
            {
                playerAnim.SetBool("Ladder", false);
                playerAnim.SetBool("Climbing", true);
                playerRb.velocity = new Vector2(0, speedClimb);
            }

            else if (Input.GetKey(KeyCode.DownArrow))
            {
                playerAnim.SetBool("Ladder", false);
                playerAnim.SetBool("Climbing", true);
                playerRb.velocity = new Vector2(0, -speedClimb);
            }

        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
            if (collision.CompareTag("Player")){
                playerAnim.SetBool("Ladder", false);
                playerAnim.SetBool("Climbing", false);
                if (collision.name == "Invocator")
                    playerAnim.SetTrigger("DeployWings");
            }
    }

}