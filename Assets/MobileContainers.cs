using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileContainers : MonoBehaviour
{
    [SerializeField] ConstantForce2D[] containers;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")){
            if (containers.Length == 0)
                return;
            foreach (ConstantForce2D g in containers) {
                g.force = new Vector2(0, 220);
            } 
        }
    }
}
