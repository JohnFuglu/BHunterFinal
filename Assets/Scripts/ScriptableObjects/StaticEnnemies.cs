using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "", menuName = "StaticEnnemis")]
[System.Serializable]
public class StaticEnnemies : ScriptableObject
{
    public string nameEnnemi;
    [Header("Stats ennemis")]
    public float vie;
    public int munitions;
    public bool detruit;
    public int damages;
    public float dommagesRecus;
    public Projectile projectile;

    private void Awake()
    {
        detruit = false;
    }
}
