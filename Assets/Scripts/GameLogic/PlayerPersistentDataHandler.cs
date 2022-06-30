using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;


[System.Serializable]
public class PlayerPersistentDataHandler : MonoBehaviour
{
    #region(Singleton)
    private static PlayerPersistentDataHandler _instance;
    public static PlayerPersistentDataHandler Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogWarning("PlayerPersistentDataHandler est null");
            return _instance;
        }
    }

    #endregion

    /*sauvegarder la progression du héro tant qu'il n'est pas mort.
     * progression = niveaux parcourus, indices récoltés, score, cible, hero choisi
     * S'il meurt recommencer une progression
     * A GARGER MEME MORT = les morceaux d'histoire, le score et nom du héro qui l'a fait, cibles débloquées, heros débloqués
     * 
     * créer un fichier en début de partie
     *  dans ce fichier mettre le nom du héro, la cible
     *      on arrive dans un niveau = on spawn le bon hero (NOM DU HERO), le score est celui qui a été sauvé
     *      quand on finit un niveau on ajoute l indice trouvé, on marque le niveau comme fini
     *      si on ne finit pas le niveau (temps) on ne fait que sauver le score et reprendre les infos d'arrivée
     *      si on meurt = on efface les données de progression, on sauve le score avec le héro correspondant
     
     
     
         
         
         */


    public delegate void OnCompletion();
    public static OnCompletion endTheLevel;
    public delegate void OnLoaded(Hero hero);
    public static OnLoaded loaded;


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
  
    public float defaultCamOrthoSize = 8;
    public void OnFinishedLoading(Scene scene, LoadSceneMode mode) {
        Debug.Log("Level Loaded"+ scene.name);
        _instance = this;
        _load = GetComponent<Load>();
        LoadCurrentProgression();
        AddHints(thisHunt);
        try
        {
            player= SpawnHero(_currentProgression.heroName);
            CheckHeroByName(player.name);
            thisHero = player.GetComponent<Hero>();
            LevelHandler.Instance.PlaceCollectables(thisHero);
            Debug.Log("----------" + "PlayerCreated"+"hero = "+thisHero.Name + "-----------");
        }
        catch (Exception e)
        {
            
            Debug.LogException(e);
        }

        foreach (var l in _currentProgression.visitedLevel)//à mettre dans le town map plutot pour éviter de venir dans le niveau
            if (l == SceneManager.GetActiveScene().name)
                foundAHintHere = true;
     
        PlayerController.killedDelegate += EndLevel;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnFinishedLoading;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnFinishedLoading;
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
        Debug.Log("json = "+ json);
            _currentProgression=_load.loadDataFromJson(json);
            Debug.Log("Load complete with hero " + _currentProgression.heroName);   
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
        ScoreSystem.Instance.SaveScoreList(ScoreSystem.Instance.UpdateScores(PlayerScore, thisHero.Name));

        if (thisHero.Destroyed) {

            if (foundAHintHere)
            {
                Debug.Log("Save avec un hint et hero est mort");
                foreach (Hint hint in collectedHints)
                {
                    if (hint == LevelHandler.Instance.thisLevelHint)
                    {
                        collectedHints.Remove(hint);
                    }
                }
                Save saveGame = new Save();
                saveGame.SaveData(_currentProgression);
                SceneManager.LoadScene("TownMap");
            }

            else
            {
                Debug.LogWarning("MORT");
                if (endTheLevel != null)
                {
                    endTheLevel();
                }
            }
        }

        
      

        if (foundAHintHere)
        {
            Save saveGame = new Save();
            saveGame.SaveData(_currentProgression);
            SceneManager.LoadScene("TownMap");
        }
    }
}


