using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Jaznot",menuName ="Characters")]
[System.Serializable]
public class JaznotSc : HeroSc //: ScriptableObject
{

   [Header ("SpecialJaznotStats")]
   public float fuel;
   public int grenefs;
   public float fuelConso;
   public float fuelReload;
    
}
