using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeySimple : MonoBehaviour
{

    public delegate void OnKeyCatch(Sprite sprite);
    public static OnKeyCatch displayKey;

    private PlayerController[] players =new PlayerController[2];
    [SerializeField] string keyName;
    [SerializeField] ParticleSystem _fx;
    Sprite _thisSprite;
    bool _picked;
    private void Start()
    {
        _thisSprite = GetComponent<SpriteRenderer>().sprite;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !_picked)
        {   
            players = GameObject.FindObjectsOfType<PlayerController>();
            if (GameObject.Find("Invocator"))
            {
                foreach (var p in players)
                {
                    Debug.Log("Dans la boucle");
                    p.AsKey(keyName);
                }
            }
            else
                players[0].AsKey(keyName);
            _fx.Play(); 
             if (displayKey != null)
                displayKey(_thisSprite);           
            _fx.transform.SetParent(collision.transform);
            _picked = true;
            Destroy(gameObject, 0.25f);
        }
    }

    IEnumerator WaitForSetInactive()
    {   
        gameObject.SetActive(false);  
         yield return new WaitForSeconds(0.5f);
       
    }
  
}

//public class KeyHud
//{
//    public Sprite[] keySprites;

//    public KeyHud() 
//    {
//        keySprites = Resources.LoadAll<Sprite>("Resources/Keys");      
//    }
//}
