using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetPuzzler : MonoBehaviour
{
    [SerializeField] GameObject _toOpen;
    Transform _origin;

   void OnEnable()
    {

        ElectricFuse.onFuseDestroyedPet += OpenGate;

    }
    void OnDisable()
    {

        ElectricFuse.onFuseDestroyedPet -= OpenGate;

    }

   void OpenGate()
    {
        _origin = GetComponent<Transform>();
        _toOpen.transform.localScale = new Vector3(0.1f,1,1);
        _toOpen.transform.localPosition = new Vector3(-87, 1, 1);
    }

    void CloseGate() 
    {
        _toOpen.transform.position = _origin.position;
        _toOpen.transform.localScale = _origin.localScale;
    }

}
