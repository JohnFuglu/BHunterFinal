using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroSelection : MonoBehaviour
{
    //trouver le nom du hero
    //creer le fichier json avec le nom
    //changer de scene

    public void SelectAHeroAndMoveAhead(string heroName) 
    {
       
        DataClass data = new DataClass();
        data.heroName = heroName;
        data.movesLeft = 5;
        Save saveName = new Save();
        saveName.SaveData(data);
        
        UnityEngine.SceneManagement.SceneManager.LoadScene("TargetSelection");
    }

    public void GoBack()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
