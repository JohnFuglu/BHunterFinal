using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Runtime.Serialization;


[System.Serializable]
public class PlayerPersistentDataHandler : MonoBehaviour
{
    [SerializeField]   private Cinemachine.CinemachineVirtualCamera _camera;

    public int PlayerScore { get; set; }
    //public HuntManager progressionOfHunts;  récupérer les infos ici
    //public  int enmiesKilled;
    public Boss[] bossKilled;
    public string placesVisited;
    //[SerializeField] TargetOfHunt _actualTarget;
    public TargetOfHunt thisHunt;
    //  public TargetOfHunt[] allTargetsInGame, allReadyHunted;

    [Header("ManageHints")]
    public List<Hint> collectedHints;// = new List<Hint>()
    List<Hint> tempHintCollection;
    public List<Hint> hintsToFind;//= new List<Hint>()
    bool _huntCompleted = false;
    public bool foundAHintHere = false;

    [Header("Load and Save System")]
    [SerializeField] private DataClass _currentProgression = new DataClass();
  //  [SerializeField] private PersistentSave _currentStories = new PersistentSave();
    public HeroSc _currentHero;
    public GameObject player;
    public Hero thisHero { get; private set; }
   
    Load _load;
    public List<string> storyPieces;
    [SerializeField] HeroSpawnList _herosList;
    [SerializeField] Transform _spawnPlayer;
    public Invocator masterInvoc;
    public float defaultCamOrthoSize = 8;
    public void LoadDataFromPlayer() {
        _load = GetComponent<Load>();
        LoadCurrentProgression();
        AddHints(thisHunt);
        try
        {
            player= SpawnHero(_currentProgression.heroName);
            if(_currentProgression.heroName == "Invocator")
                masterInvoc = player.GetComponent<Invocator>();
            CheckHeroByName(player.name);
            thisHero = player.GetComponent<Hero>();
            GetComponent<LevelHandler>().PlaceCollectables(thisHero);
        }
        catch (Exception e)
        {
            
            Debug.LogException(e);
        }

        foreach (var l in _currentProgression.visitedLevel)//à mettre dans le town map plutot pour éviter de venir dans le niveau
            if (l == SceneManager.GetActiveScene().name)
                foundAHintHere = true;
     
    }


    GameObject SpawnHero(string name) 
    {
        GameObject temp;
        foreach(var h in _herosList.Heroes) 
        {
            if (h.name == name) 
            {
                temp = Instantiate(h) as GameObject;
                temp.transform.position = _spawnPlayer.position;
                temp.name = name;
                return temp;
            }
        }
        Debug.LogError("There is no hero with that name !");
        return null;
    }



    public void CollectHint(Hint hintIamCollectingNow)
    {
        if (collectedHints.Count <= 0) 
        {
            collectedHints.Add(hintIamCollectingNow);
            if(!_currentHero.collectedHints.Contains(hintIamCollectingNow))
                _currentHero.collectedHints.Add(hintIamCollectingNow);
            hintsToFind.Remove(hintIamCollectingNow);
        }
        else 
        {
            foreach (Hint hint in collectedHints)
            {
                if (hintIamCollectingNow.hintNumber != hint.hintNumber)
                {
                    collectedHints.Add(hintIamCollectingNow);
                    if (!_currentHero.collectedHints.Contains(hintIamCollectingNow)) 
                    {
                        _currentHero.collectedHints.Add(hintIamCollectingNow);
                        tempHintCollection.Add(hintIamCollectingNow);
                    }
                        
                    hintsToFind.Remove(hintIamCollectingNow);
                }
            }
        }
        foundAHintHere = true;

    }

    public void AddHints(TargetOfHunt target)
    {
        foreach(Hint hints in target.hints) 
        {
            hintsToFind.Add(hints);
        }
    }

    HeroSc CheckHeroByName(string PlayerName) 
    {
      
        switch (PlayerName) 
        {
            case "Jaznot":
                _currentHero = player.GetComponent<Jaznot>().jaznot;
                return _currentHero;
           
            case "Invocator":
                _currentHero = player.GetComponent<Invocator>().invocatorSc;
                return _currentHero;
              
        }
        return null;
    }

    public void AssignDataToSave(DataClass data,HeroSc hero) 
    {
        
            data.ReturnHintIds(collectedHints, data.hintIds);
            data.heroName = player.name;
            data.currentLevel = SceneManager.GetActiveScene().name;

        if (foundAHintHere)
            data.visitedLevel.Add(data.currentLevel);
            
   
            data.currentTarget = thisHunt.name;
           
            data.score = PlayerScore;
            data.huntEnded = _huntCompleted;
    
    }

    void LoadCurrentProgression()
    {
        Debug.Log("Loading......");
        string path = Application.persistentDataPath + "/ProgressionDatas.json";
        var json = System.IO.File.ReadAllText(path);
        _currentProgression=_load.loadDataFromJson(json);
    }


    public void SaveProgression()
    {
        Debug.Log("quitte");
        AssignDataToSave(_currentProgression, _currentHero);
        Save saveGame = new Save();
        saveGame.SaveData(_currentProgression);
        SceneManager.LoadScene("MainMenu");
    }


    public void EndLevel()
    {
     UIManager ui = GameObject.Find("Canvas").GetComponent<UIManager>();
     ui.FadeToBlack(); 
     if(ui.black){
        /*Sauve le niveau en cours
        sauver le score mis à jour
        */
        _currentProgression.currentLevel=SceneManager.GetActiveScene().name;
        int score = ui.GetScore();
        Debug.Log("Score = "+score);
        GetComponent<ScoreSystem>().UpdateScores(score,_currentHero.name);
        GetComponent<ScoreSystem>().SaveScoreList();
     /*5 cas : 
     -le hero est mort
     -le hero n'a plus de temps
     -le hero a fini sans hint
     -le hero a fini avec un hint #avec mvt restants #sans mvt restants
     -le joueur a sauvé et quitté*/
     if(thisHero.Destroyed){
           
            SceneManager.LoadScene("TownMap",LoadSceneMode.Single); 
     }
     if(ui._timeLeft <= 0){
       
        SceneManager.LoadScene("TownMap",LoadSceneMode.Single);
     }
     if(ui.quit){
        SaveProgression();
    }
     if(!thisHero.Destroyed && ui._timeLeft > 0){
            if(_currentProgression.movesLeft > 0){
                if(collectedHints.Count > 0){
                    foreach (Hint hint in collectedHints)
                    {
                        if (hint == GetComponent<LevelHandler>().thisLevelHint)
                        {
                            collectedHints.Remove(hint);
                        }
                    }
                    Save saveGame = new Save();
                    saveGame.SaveData(_currentProgression);
                    SceneManager.LoadScene("TownMap",LoadSceneMode.Single);
                    
                }
                else {
                    SceneManager.LoadScene("TownMap",LoadSceneMode.Single);
                }
        }
        SceneManager.LoadScene("MainMenu",LoadSceneMode.Single);  

    }
    }
    
}}



