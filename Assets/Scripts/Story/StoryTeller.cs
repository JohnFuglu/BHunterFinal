using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



// savoir ce qui a été lu et en garder trace
// enregistrer le string "pieceId" correspondant à la story piece ex : "A1", "A2"..
// afficher le bon texte quand on passe sur une clef usb

// rendre accessible les textes dans un menu
//


public class StoryTeller : MonoBehaviour
{
    bool affiche = false; 
    string st = "";
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player") {
            foreach (Story s in PlayerPersistentDataHandler.Instance._currentHero.storyPieces) {
                if (!s.read && !affiche)
                {
                    st = s.text;
                    s.read = true;
                    affiche = true;
                }
        
        }
            UIManager.Instance.AfficheTexte(st);
            Destroy(gameObject);
        }
    }
   
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            affiche = false;
            st = "";
        }
    }

    
}
