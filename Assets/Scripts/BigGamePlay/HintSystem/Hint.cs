using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName ="Hints")]
[System.Serializable]
public class Hint : ScriptableObject
{
    public int hintNumber;
    //public string hintName;
    //public string targetOfHunt;
    public Sprite descriptionImage;
    [TextArea]
    public string description;
    public string nameOfHint;
    public GameObject hintPrefabToSpawn;
}
