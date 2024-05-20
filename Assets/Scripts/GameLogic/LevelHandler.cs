using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[System.Serializable]
public class LevelHandler : MonoBehaviour
{
    public float timeToCompleteLevel = 240;

    [SerializeField] GameObject[] potentialHintsHideOuts;
    [SerializeField] Transform[] collectablesSpawns;
    public Hint thisLevelHint;
    private PlayerPersistentDataHandler playData;


    void Awake()
    {
        playData= GameObject.Find("GameHandler").GetComponent<PlayerPersistentDataHandler>();
        playData.LoadDataFromPlayer();
        if (!playData.foundAHintHere) 
        { 
            thisLevelHint=ChooseMissingHint();
            AddHintInOneGameObject(thisLevelHint);
            GameObject.Find("Canvas").GetComponent<UIManager>().SetUI(playData.thisHero);
            playData.GetComponent<ScoreSystem>().SetScore();
        }
    }

    Hint ChooseMissingHint()
    {
        /*Intervales de random
         * 0-4 Jaznot
         * 5-9 Invoq
         * 10-14 Royale
         * 15-19 Assassin
         * 
         * couleurs des gameObject
         * Jaznot Orange
         * Invoq Vert
         * Royale Bleu
         * Ass Rouge
         
        
        int id=0;
        string nom = playData.thisHero.Name;
        switch (nom) {
            case "Jaznot":
                id = Random.Range(1, 4);
                break;
            case "Invocator":
                id = Random.Range(5, 9);
                break;
            case "Royale":
                id = Random.Range(10, 14);
                break;
            case "Assassin":
                 id = Random.Range(15, 19);
                break;
        }
 */
        int id=Random.Range(0,3);
        foreach(Hint hints in playData.hintsToFind) 
        {
            if (hints.hintNumber == id) 
            {
                Hint toReturn = hints;
                return toReturn;
            }
        }
         return null;
    }

    void AddHintInOneGameObject(Hint hintToAdd) 
    {
        int randomNumber = Random.Range(0, potentialHintsHideOuts.Length);//est ce que ca inclut le dernier item du tableau?
        HintAllocator _hintAllocator = potentialHintsHideOuts[randomNumber].AddComponent<HintAllocator>();
        Debug.Log("HintAllocator "+_hintAllocator +'\n' );
        try{
            _hintAllocator.hint = thisLevelHint; 
            Debug.Log("LevelHint "+ thisLevelHint.name +'\n' );
            _hintAllocator.InitHints();
    
        }catch{
            Debug.LogError("Pas de Hint");
        }
    }  
       
        

     public void PlaceCollectables(Hero hero) 
    {
        int random = Random.Range(0, collectablesSpawns.Length);
        GameObject go= Instantiate(hero.Collectable);
        go.transform.position = collectablesSpawns[random].position;
    }

}
