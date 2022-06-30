using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class DataClass 
{
    public List <int> hintIds = new List<int>();//déclaré ou pas ?
    public List<string> visitedLevel = new List<string>();
    public string heroName, currentLevel,currentTarget;
   // public List<string> storiesSeen = new List<string>();
    public int score;
    public bool huntEnded=false;
    public short movesLeft;


    public void ReturnHintIds(List<Hint> listPlayerPers, List<int> listToBeSaved)
    {
        foreach (Hint hint in listPlayerPers)
        {
            //int foundHintId = hint.hintNumber;
            listToBeSaved.Add(hint.hintNumber);
        }
    }


    public string CurrentHunt(TargetOfHunt targetOfHunt) 
    {
        string toReturn = targetOfHunt.targetName;
        return toReturn;
    }

}

