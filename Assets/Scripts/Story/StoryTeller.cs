using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StoryTeller : MonoBehaviour
{
    bool affiche = false; 
    string st = "";
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player") {
            foreach (Story s in GameObject.Find("GameHandler").GetComponent<PlayerPersistentDataHandler>()._currentHero.storyPieces) {
                if (!s.read && !affiche)
                {
                    st = s.text;
                    s.read = true;
                    affiche = true;
                }
        
        }
            GameObject.Find("MainCanvas").GetComponent<UIManager>().AfficheTexte(st);
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
