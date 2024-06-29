using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GravityObject : MonoBehaviour
{
    [SerializeField]Light2D lght;
    [SerializeField]bool actif=false;
    Rigidbody2D rb;
   [SerializeField] float secs=5;
    void Start(){
        rb= GetComponent<Rigidbody2D>();
    }
    void OnMouseDown(){
        if(!actif){
            StartCoroutine(WaitToFall(secs));
        }
    }
    IEnumerator WaitToFall(float secs){
        lght.color=Color.green;
        rb.gravityScale = -2;
        actif=true;
        yield return new WaitForSeconds(secs);
        actif=false;
        lght.color=Color.red;
        rb.gravityScale = 1;
        actif=false;
    }
}
