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
    [SerializeField] List<string> story;
    [SerializeField] StoriesList slist;
    [SerializeField]bool affiche = false;

    public void DisplayStoryText(string buttonName) 
    {
        switch (buttonName) 
        {
            case "Jaznot":
                if(affiche==false)
                    _storyText.text = GatherStoryPieces(buttonName);
                    StartCoroutine(Affiche(buttonName));
            break;
            case "Invocator":
                if(affiche==false)
                    _storyText.text = GatherStoryPieces(buttonName);
                    StartCoroutine(Affiche(buttonName));
            break;
        }
    }
    IEnumerator Affiche(string buttonName){
        affiche=false;
        yield return new WaitForSeconds(3);
    }

    string GatherStoryPieces(string herosName) 
    {
        story.Clear();
        foreach(Story s in slist.stories)
        {
            if (s.heroName == herosName && s.read)
                story.Add(s.text +'\n');
        }
        if(story.Count>0){
            affiche=true;
            return string.Join("\n", story);
        }
           
        else
        return "No story";   
        
    }  
    
    public void GoBack() 
    {
        SceneManager.LoadScene("MainMenu");
    }
}


    //dans la scène on veut load juste le json en question et récupérer un StoryData
    //qui contiendra toutes les stories trouvées
    //poiur plus tard syst de comparaison avec un tableau de stories pour afficher en gris des morceaux non trouvés

  