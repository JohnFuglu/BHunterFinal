using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Character
{
    [SerializeField] GameObject _collectable;
    [SerializeField] int _specialAmmo;

    public Sprite ammoIcon, specialAmmoIcon, faceIcon, lifeColor;
    public int SpecialAmmo { get {return _specialAmmo; }set { _specialAmmo = value; } }
    public GameObject Collectable { get { return _collectable; } }
 
}
