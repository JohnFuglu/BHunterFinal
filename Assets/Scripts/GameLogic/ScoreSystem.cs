using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;


[System.Serializable]
public class ScoreSystem : MonoBehaviour
{
    private static ScoreSystem _instance;
    public static ScoreSystem Instance 
    {
        get
        {
            if (_instance == null)
                Debug.LogError("Il n'y a pas de scoreSystem !!");
            return _instance;
        }
    }


    public List<ScoreData> oldScores = new List<ScoreData>();
   
    public TextMeshProUGUI _scoreDisplay;

    private void Awake()
    {
        _instance = this;
    }


    private void Start()
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "MainMenu" && 
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "HeroSelection"&& 
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "TargetSelection" && 
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "TownMap")
        {
            if (File.Exists(Application.persistentDataPath + "/Scores.json"))
            {
                string jSon = File.ReadAllText(Application.persistentDataPath + "/Scores.json");
                Debug.Log("Found a Json file");

                ScoreData score = JsonUtility.FromJson<ScoreData>(jSon);
                oldScores.Add(score);
                
                Debug.Log(jSon);
                if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Scores") 
                {
                   
                    DisplayScores();
                }
                   
            }
            else
            {
                Debug.LogWarning("No json file found ....");
            }
        }
    }
    public ScoreData UpdateScores(int score, string heroSName)//, DateTime dateAndTime
    {
        ScoreData scoreD = new ScoreData(score, heroSName);//, ,dateAndTime
        oldScores.Add(scoreD);
        return scoreD;
        
    }

    public void SaveScoreList(ScoreData score) 
    {
        string jSon = JsonUtility.ToJson(score, true) as string;
        File.WriteAllText(Application.persistentDataPath + "/Scores.json", jSon);
        Debug.Log("SaveGame completed..."+ jSon);

    }
    void DisplayScores() 
    {
        foreach(var s in oldScores)
            _scoreDisplay.text = s.ReturnScoreDatas(s) + "\n";
    }

    public void GoBack()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}

[System.Serializable]
public class ScoreData
{
    public int sToSave;
    public string heroPlayedWithScore;
    //   public DateTime dateTime;
    PlayerPrefs player;
    public ScoreData(int scoreToSave, string heroName) //, DateTime dateInfo
    {
        sToSave = scoreToSave;
        heroPlayedWithScore = heroName;
     //   dateTime = dateInfo;
    }

    public string ReturnScoreDatas(ScoreData scoreDatas) 
    {
        string toReturn = "You've gathered " + sToSave + " points" + " with" + heroPlayedWithScore + " !";//"On "+
        return toReturn;
    }
}
