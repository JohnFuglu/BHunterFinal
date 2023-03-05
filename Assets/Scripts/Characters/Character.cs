using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Character : StandardObject
{
   
    [SerializeField] string _name;

    public string Name { get { return _name; } set { _name = value; } }

}
