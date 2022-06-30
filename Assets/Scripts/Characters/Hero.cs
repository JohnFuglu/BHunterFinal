using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Character
{
    private static Hero _instance;
    public static Hero Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("Hero missing !");
            return _instance;
        }
    }

    [SerializeField] GameObject _collectable;
    private void Awake()
    {
        _instance = this;
    }

    [SerializeField] int _specialAmmo;

    public Sprite ammoIcon, specialAmmoIcon, faceIcon, lifeColor;
    public int SpecialAmmo { get {return _specialAmmo; }set { _specialAmmo = value; } }
    public GameObject Collectable { get { return _collectable; } }
 
}
