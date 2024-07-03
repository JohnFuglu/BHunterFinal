using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
[System.Serializable]
public class HintAsset : MonoBehaviour
{
    public Hint hintToFind;
    [SerializeField]PlayerPersistentDataHandler _playerData;
    [SerializeField] ParticleSystem _fxOnContact;
    // [SerializeField] TextMeshPro _textToSpawn;
   
    private void Start()
    {
        _playerData = GameObject.FindObjectOfType<PlayerPersistentDataHandler>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Vector2 textPos = new Vector2(collision.transform.position.x, collision.transform.position.y);
            //_fxOnContact.transform.position = textPos;
            //affiche.text="You've found the Hint number " + hintToFind.hintNumber.ToString() + " "+hintToFind.nameOfHint;
            GameObject.Find("MainCanvas").GetComponent<UIManager>().AfficheTexte("You've found the Hint number " + hintToFind.hintNumber.ToString() + " " + hintToFind.nameOfHint);
            
            _fxOnContact.Play();
            
            StartCoroutine(WaitForSetInactive()); 
            _playerData.CollectHint(hintToFind);
        }
    }

    IEnumerator WaitForSetInactive() 
    { 
        yield return new WaitForSeconds(_fxOnContact.duration);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        StartCoroutine(WaitToKillText());
    }

    IEnumerator WaitToKillText()
    {
         yield return new WaitForSeconds(2f);
            gameObject.SetActive(false);
        GameObject.Find("MainCanvas").GetComponent<UIManager>().EffaceTexte();
    }
}

