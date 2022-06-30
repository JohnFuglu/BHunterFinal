using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[System.Serializable]
public class LevelHandler : MonoBehaviour
{
    private static LevelHandler _instance;
    public static LevelHandler Instance {
        get{
            if(_instance==null)Debug.LogError("Pas de levelHandler");
                        return _instance;
        }
    }
    public float timeToCompleteLevel = 240;

    [SerializeField] GameObject[] potentialHintsHideOuts;
    [SerializeField] Transform[] collectablesSpawns;
    public Hint thisLevelHint;

    string t = " ";


    private void Awake()
    {
        _instance = this; 
     
        
    }

    void Start()
    {
     
        if (!PlayerPersistentDataHandler.Instance.foundAHintHere) 
        { 
            thisLevelHint=ChooseMissingHint();
            AddHintInOneGameObject(thisLevelHint);
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
         
         */
        int id=0;
        string nom = PlayerPersistentDataHandler.Instance.thisHero.Name;
        switch (nom) {
            case "Jaznot":
                id = Random.Range(0, 4);
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

       
        foreach(Hint hints in PlayerPersistentDataHandler.Instance.hintsToFind) 
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
        _hintAllocator.hint = thisLevelHint;
    }

     public void PlaceCollectables(Hero hero) 
    {
        int random = Random.Range(0, collectablesSpawns.Length);
        GameObject go= Instantiate(hero.Collectable);
        go.transform.position = collectablesSpawns[random].position;
    }

}
