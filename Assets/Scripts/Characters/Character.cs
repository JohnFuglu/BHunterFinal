using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Character : StandardObject
{
    [SerializeField] int _damage;
    [SerializeField] string _name;
   
    public int Damage { get { return _damage; } set { _damage = value; } }
    public string Name { get { return _name; } set { _name = value; } }

}
