using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Hunts")]
[System.Serializable]
public class TargetOfHunt : ScriptableObject
{
    public string targetName;
    public List<Hint> hints;
 //   public List <string> placesToVisit;
    public bool targetKilled;
    public int bountyScore;
    public Boss thisHuntBoss;

    //public TargetOfHunt(string nom, List<Hint> hintsToFind, string[] possibleSpots, bool isHeDead, int scoreOnKill, Boss target)
    //{
    //    nom = this.targetName;
    //    hintsToFind = this.hints;
    //    possibleSpots = this.placesToVisit;
    //    isHeDead = this.targetKilled;
    //    scoreOnKill = this.bountyScore;
    //    target = this.thisHuntBoss;
    //}


}
