using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetractBridge : MonoBehaviour, IActivable
{
    Transform origin;
    [SerializeField] bool retract = false;
    [SerializeField] float moveRate, scaleRate;
    Vector3 vector;

    private void Start()
    {
        origin = this.transform;
    }

   public void Action() {
        retract = true;
            transform.Translate(new Vector3(0, 1, 0) * moveRate);
            StartCoroutine(Retract());
            if (transform.localScale.y <= 0) retract = false;
        
    }

    public IEnumerator Retract() {
 
        transform.localScale = new Vector3(origin.localScale.x, transform.localScale.y - scaleRate, origin.transform.localScale.z);
        yield return new WaitForSeconds(1f);  
    }
}
