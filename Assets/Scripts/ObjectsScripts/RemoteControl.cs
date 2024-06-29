using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RemoteControl : MonoBehaviour
{
    //lié à un objet controlé
    //4 boutons qui rendent chacune un string direction
    //effet lumineux quand on clique
    [SerializeField] CraneScript objectToControl;
    [SerializeField] InSceneButton[] _buttons;


    private void Update()
    {
        
    }
}
