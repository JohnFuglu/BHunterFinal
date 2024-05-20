using System;
using UnityEngine;
[Serializable]
public class ScoreData
{
    public int sToSave;
    public string heroPlayedWithScore;
    public string dateTime;


    public ScoreData(int scoreToSave, string heroName)
    {
        sToSave = scoreToSave;
        heroPlayedWithScore = heroName;
        dateTime = DateTime.Now.ToShortDateString();
    }

    public ScoreData(int scoreToSave, string heroName, string date){
        sToSave = scoreToSave;
        heroPlayedWithScore = heroName;
        dateTime = date;
    }
    public string ReturnScoreDatas() 
    {
        string toReturn = "On the "+dateTime+", you've gathered " + sToSave + " points" + " with" + heroPlayedWithScore + " !";//"On "+
        return toReturn;
    }
    public string SaveToString(){
        return sToSave+"!"+heroPlayedWithScore+"!"+dateTime+"!";
    }
}
