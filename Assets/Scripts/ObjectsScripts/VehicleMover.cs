using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleMover : MonoBehaviour
{
    [SerializeField] Vector3 startPos;
    [SerializeField] Vector2 deplacement;
    [SerializeField] bool gauche;
 
    // Start is called before the first frame update
    void Start()
    {
        startPos= GetComponent<Transform>().position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(deplacement);    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BordDuNiveauGauche")&&gauche) 
        {
                transform.position = startPos;
        }

        if (collision.CompareTag("BordDuNiveauDroit") && !gauche)
        {
            transform.position = startPos;
        }
    }

}
