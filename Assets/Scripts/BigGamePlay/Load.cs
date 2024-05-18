using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Load : MonoBehaviour
{
    string[] niveaux = {"DownTown","UnderGround","Docks","Laboratory","SpatioPort","Canibalecter","Zoo"};


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
                 GameObject.Find("GameHandler").GetComponent<PlayerPersistentDataHandler>().PlayerScore = dataLoaded.score;
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
                    GameObject.Find("GameHandler").GetComponent<PlayerPersistentDataHandler>().thisHunt = targetOfThisHunt;
                }
        }
        else 
            Debug.LogWarning("targetOfThisHunt s missing !");



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
