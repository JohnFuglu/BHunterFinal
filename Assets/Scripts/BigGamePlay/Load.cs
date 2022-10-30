using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Load : MonoBehaviour
{
    #region Explications
    /*
      récupérer le nom de la cible
    trouver le scriptable object qui correspond

    récupérer le nom du hero pour en faire un string qui va servir à instancier le bon hero
    faire un foreach dans les <hint> et sortir ceux dont les id sont présent le json

    récup string pour faire sceneManager get at scene by name
    récup le score en un int
    vérifier que le bool huntEnd est bien false, si c'est pas le cas prévoir un retour au menu, griser continuer et delete json.
     */
    #endregion
    #region(Singleton)

    private static Load _instance;
    public static Load Instance 
    { 
        get 
        {
            if (_instance == null)
                Debug.LogError("No Load singleton");
            return _instance;
        } 
    }
    #endregion

    string[] niveaux = {"DownTown","UnderGround","Docks","Laboratory","SpatioPort","Canibalecter","Zoo"};


    private void Awake()
    {
        _instance = this;
 //       _launchHunt = GetComponent<LaunchHunt>();
    }

  //  LaunchHunt _launchHunt;


    public DataClass loadDataFromJson(string jSon) 
    {

        DataClass dataLoaded = new DataClass();
        dataLoaded = JsonUtility.FromJson<DataClass>(jSon);

        if (!dataLoaded.huntEnded) 
        {

            switch (dataLoaded.heroName) 
            {
                case "Jaznot":
                    JaznotSc hero = Resources.Load<JaznotSc>("ScriptableObjects/Heroes/" + dataLoaded.heroName);
                    DesTrucs(dataLoaded, hero);

                    break;
                case "Invocator":
                    InvoqSc  hero2 = Resources.Load<InvoqSc>("ScriptableObjects/Heroes/" + dataLoaded.heroName);
                    DesTrucs(dataLoaded, hero2);
                    break;
                    default:
                    Debug.LogWarning("Hero s missing !");
                    break;
            }




            if (SceneManager.GetActiveScene().name != "Stories" && SceneManager.GetActiveScene().name!="MainMenu" && SceneManager.GetActiveScene().name != "TownMap" && SceneManager.GetActiveScene().name != "TargetSelection" && SceneManager.GetActiveScene().name != "HeroSelection") 
            {
                 PlayerPersistentDataHandler.Instance.PlayerScore = dataLoaded.score;
             //    PlayerPersistentDataHandler.Instance.storyPieces = dataLoaded.storiesSeen;
            }
             
            return dataLoaded;
        }
        else
        {
            SceneManager.LoadScene("MainMenu");
        }
        return dataLoaded;
    }

    void DesTrucs(DataClass data, HeroSc hero) 
    {

        TargetOfHunt targetOfThisHunt = Resources.Load<TargetOfHunt>("ScriptableObjects/Hunts/" + data.currentTarget);

        if (targetOfThisHunt != null)//autre niveaux aussi
        {
            foreach (string s in niveaux)
                if (SceneManager.GetActiveScene().name == s)
                {
                    Debug.Log("targetOfThisHunt" + targetOfThisHunt.name);
                    PlayerPersistentDataHandler.Instance.thisHunt = targetOfThisHunt;
                }
        }
        else Debug.LogWarning("targetOfThisHunt s missing !");



        foreach (int id in data.hintIds)
        {
         //   if (id != null)
         //   {
                foreach (Hint hint in targetOfThisHunt.hints)
                {
                    if (hint.hintNumber == id)
                        hero.collectedHints.Add(hint);
                }
      //      }
        }
    }


}
