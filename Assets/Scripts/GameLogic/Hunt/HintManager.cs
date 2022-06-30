using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public static class HintManager 
{
   // public static TargetOfHunt target;
    public static bool huntCompleted;
    [Header("ManageHints")]
    public static List<Hint> collectedHints;
    public static List<Hint> hintsToFind;
   

}
