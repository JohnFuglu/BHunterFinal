using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "StoryPiece", menuName = "Story")]
public class Story : ScriptableObject
{
    [TextArea]
    public string text;
    public string heroName;
    public string pieceId;
    public bool read = false;
    //public int pieceNumber;   
}

