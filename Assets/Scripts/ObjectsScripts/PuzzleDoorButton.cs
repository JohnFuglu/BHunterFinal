using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleDoorButton : InSceneButton
{
    public delegate void OnButtonClicked(int n,Color i);
    public static OnButtonClicked openDoor;
    Color buttonColor;
    [SerializeField] int n;
    protected override void Start()
    {
        base.Start();
        buttonColor = GetComponent<SpriteRenderer>().color;
    }


    protected override void OnMouseDown()
    {
        base.OnMouseDown();
        if (openDoor != null)
            openDoor(n,buttonColor);
    }
    protected override void OnMouseUp()
    {
        base.OnMouseUp();
    }
}
