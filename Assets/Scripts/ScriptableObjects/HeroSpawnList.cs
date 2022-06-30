using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="HerosList")]

public class HeroSpawnList : ScriptableObject
{
    public GameObject[] Heroes;
}
