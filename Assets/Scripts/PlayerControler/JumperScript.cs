using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperScript : MonoBehaviour
{
    [SerializeField] float puissanceJumper;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        other.GetComponent<Rigidbody>().AddForce(Vector3.up * puissanceJumper, ForceMode.Impulse);
    }
}
