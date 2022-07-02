﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class MainMenu : MonoBehaviour
{
    //private Save _save;
    public DataClass data = new DataClass();
    [SerializeField] bool _dataEmpty = true;
    [SerializeField] UnityEngine.UI.Button button;
    [SerializeField] StoriesList slist;
    [SerializeField] GameObject gameOverPanel;
    private void Start()
    {
        button.interactable = false;
        if (File.Exists(Application.persistentDataPath + "/ProgressionDatas.json"))
        {   
            button.interactable = true;
        }
     }
          
        
           

    public void ContinueGame()
    {

        if (File.Exists(Application.persistentDataPath + "/ProgressionDatas.json"))
        {
            button.interactable = true;
            _dataEmpty = false;
            string json = File.ReadAllText(Application.persistentDataPath + "/ProgressionDatas.json");
            data = Load.Instance.loadDataFromJson(json);
            Debug.Log("Found a Json file");
            Debug.Log(json);
            if (data.movesLeft == 0)
            {
                gameOverPanel.SetActive(true);
                StartCoroutine(GameOver());
                button.interactable = false;
            }
        }
        if (_dataEmpty == false) 
        {
          
            //si le current level n'est pas dans la liste des trucs visités (car hint trouvé)
            foreach (var v in data.visitedLevel) 
            {
                if(data.currentLevel!=v)    
                    SceneManager.LoadScene(data.currentLevel); 
            }
                SceneManager.LoadScene("TownMap");
        }
    }

    public void NewGame()
    {
        if (File.Exists(Application.persistentDataPath + "/ProgressionDatas.json"))
        {
            _dataEmpty = false;
            string json = File.ReadAllText(Application.persistentDataPath + "/ProgressionDatas.json");
            data = Load.Instance.loadDataFromJson(json);
            HeroSc hsc = (HeroSc)Resources.Load("ScriptableObjects/Heroes/"+data.heroName);

            //si nouveau jeu delete le fichier de save
            string path = Application.persistentDataPath + "/ProgressionDatas.json";
            hsc.collectedHints.Clear();
            File.Delete(path);
            _dataEmpty = true;
        }
        foreach (var v in slist.stories) {
            v.read = false;
        }
      
        SceneManager.LoadScene("HeroSelection");
    }

    void ClearHeroScriptObject(HeroSc h) { 

    }

    public void QuitGame()
    {
        Debug.Log("Je quitte...");
        Application.Quit();
    }

    public void Story() 
    {
        SceneManager.LoadScene("Stories");
    }

    public void Scores()
    {

        SceneManager.LoadScene("Scores");
    }

    IEnumerator GameOver() {
        yield return new WaitForSeconds(1.5f);
        gameOverPanel.SetActive(false);
    }
}
