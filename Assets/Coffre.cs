using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coffre : MonoBehaviour
{
    [SerializeField] SpriteRenderer renderer;
    [SerializeField] ParticleSystem fx;
    [SerializeField] Sprite open;
    [SerializeField] GameObject puzzle;

    private void Start()
    {
        PuzzleSerrure.open += OpenCoffre;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            puzzle.SetActive(true);

        }
    }
    public void OpenCoffre()
    {
        renderer.sprite = open;
        //fx.Play();
        PuzzleSerrure.open -= OpenCoffre;
        Destroy(puzzle);
    }
}
