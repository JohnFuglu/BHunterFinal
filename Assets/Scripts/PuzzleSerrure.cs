using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSerrure : MonoBehaviour
{
    public delegate void OpenCoffre();
    public static OpenCoffre open;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (open != null)
            open();
    }
}
