using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "StoriesList", menuName = "Story")]
public class StoriesList : ScriptableObject
{
    public Story[] stories;
}