using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using System;


[System.Serializable]
public class ScoreSystem : MonoBehaviour
{

    public List<ScoreData> oldScores = new List<ScoreData>();
   
    public TextMeshProUGUI _scoreDisplay;

    void Start(){
        if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Scores"){
        if (File.Exists(Application.persistentDataPath + "/Scores.txt"))
        {
                Debug.Log("Found a score textfile");
                FileToScoreData();
                if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Scores") 
                {
                    DisplayScores();
                }    
        }
        }
    }
    public int MaxScore(string hero){
        int tmp=0;
        foreach(ScoreData sD in oldScores){
            if(sD.sToSave>tmp)
                tmp=sD.sToSave;
        }
        return tmp;
    }
    public void SetScore()
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "MainMenu" && 
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "HeroSelection"&& 
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "TargetSelection" && 
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "TownMap")
        {
            if (File.Exists(Application.persistentDataPath + "/Scores.txt"))
            {
                string str = File.ReadAllText(Application.persistentDataPath + "/Scores.txt");
                Debug.Log("Found a score textfile");
                FileToScoreData();
                UIManager ui = GameObject.Find("Canvas").GetComponent<UIManager>();
                //TODO ajouter set score dans UI, prob du perso joué etc
            }
            else
            {
                Debug.LogWarning("No Score textfile found ....");
            }
        }
    }
    public void UpdateScores(int score, string heroSName)
    {
        ScoreData scoreD = new ScoreData(score, heroSName);
        oldScores.Add(scoreD);
    }
    private void FileToScoreData(){
        string path =Application.persistentDataPath + "/Scores.txt";
        Debug.Log(path);
        
                string[] s = File.ReadAllLines(path);
                List<string> scores = new List<string>();
                foreach(string score in s){
                    scores.Add(score);
                }
                foreach(string st in scores){
                    string[]tmp = st.Split('!');
                    ScoreData score = new ScoreData(int.Parse(tmp[0]),tmp[1],tmp[2]);
                    oldScores.Add(score);
                }
    }
    public void SaveScoreList() 
    {
        string s1 = "";
        foreach(ScoreData s in oldScores){
            s1+=s.SaveToString()+'#'+'\n';
        }
        File.WriteAllText(Application.persistentDataPath + "/Scores.txt", s1);
        Debug.Log("SaveGame completed..."+ s1);

    }
    void DisplayScores() 
    {
        foreach(var s in oldScores)
            _scoreDisplay.text += s.ReturnScoreDatas() + "\n";
    }

    public void GoBack()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
