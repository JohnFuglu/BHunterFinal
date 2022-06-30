using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
public class StoryMenu : MonoBehaviour
{
    [SerializeField] HeroSc[] _heroesSc;
    [SerializeField] Text _storyText;
   // [SerializeField] PersistentSave _story;
    [SerializeField] List<string> story;
    [SerializeField] StoriesList slist;
    bool affiche = false;

    public void DisplayStoryText(string buttonName) 
    {
        switch (buttonName) 
        {
            case "Jaznot":
                if(affiche==false)
                    _storyText.text=GatherStoryPieces(buttonName);
                affiche = true;
            break;
        }
    }


    string GatherStoryPieces(string herosName) 
    {
        switch (herosName) 
        {
            case "Jaznot":
                foreach(Story s in slist.stories)
                {
                    if (s.heroName == herosName && s.read)
                        story.Add(s.text +'\n');
                }
            return string.Join("\n", story);
               
            default:
                return "No story";   
        }
    }

    public void GoBack() 
    {
        SceneManager.LoadScene("MainMenu");
    }
}


    //dans la scène on veut load juste le json en question et récupérer un StoryData
    //qui contiendra toutes les stories trouvées
    //poiur plus tard syst de comparaison avec un tableau de stories pour afficher en gris des morceaux non trouvés

  