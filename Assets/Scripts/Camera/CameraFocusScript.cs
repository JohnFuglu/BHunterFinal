using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraFocusScript : MonoBehaviour
{
    //de zoom et cadre sur un point pour les sauts dans le vide
    //lerp à tester, smooth à ajouter
    [SerializeField] CinemachineVirtualCamera cam;
    [SerializeField] Transform focusPoint;
    [SerializeField] float camOrthoSize = 15;
    private Transform player;
   
    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //cam.LookAt = focusPoint;
            //cam.Follow = focusPoint;
            cam.m_Lens.OrthographicSize = camOrthoSize;
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //cam.LookAt = player;
            //cam.Follow = player;
            cam.m_Lens.OrthographicSize = 8;
        }
       
    }
}
