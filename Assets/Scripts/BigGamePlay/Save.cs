using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


[System.Serializable]
public class Save
{
   
    //sauver les items collectés
    //pouvoir retrouver quel héro est joué par le joueur = par le nom
    //trouver quel level est en cours pour le lancer si on fait continuer
    //quel est la cible, d'après le targetOfHunt
    //sauver le score tel qu'il est SAUF si on meurt dans le combat final
    //prévoir une bool pour savoir si la chasse est en court ou pas(voir au dessus)

    public void SaveData(DataClass data) 
    {
        string jSon = JsonUtility.ToJson(data,true);
        File.WriteAllText(Application.persistentDataPath + "/ProgressionDatas.json", jSon);
        Debug.Log("SaveGame completed...");
    }
}


