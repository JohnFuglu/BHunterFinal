using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Characters/Heros")]
[System.Serializable]
public class HeroSc : ScriptableObject
{
    [Header("SharedHeroStats")]
//public float vie;
    public string namePlayer;
    //public float damage;
    [Header("UI")]
    public Sprite faceIcon;
    public Sprite ammoIcon;
    public Sprite specialAmmoIcon;
    public Sprite lifeColor;
 
    [Header("ManageHints")]
    public List <Hint> collectedHints;
    TargetOfHunt hintsToFind;
    [Header("Story")]
    public List<Story> storyPieces;
    public void CollectHint(Hint hintIamCollectingNow)
    {
        foreach(Hint hints in hintsToFind.hints) 
        { 
            if(hints.hintNumber== hintIamCollectingNow.hintNumber) 
            {
                collectedHints.Add(hintIamCollectingNow);
                hintsToFind.hints.Remove(hints);
            }
        }
        
    }
}
