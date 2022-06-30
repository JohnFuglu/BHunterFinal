using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class DoorPuzzle : DoorScript
{
    [SerializeField] int[] correctInput;
    [SerializeField] int[] inputs;
    [SerializeField] short arraySize;
    [SerializeField] Light2D[] colorCheck;

    [SerializeField] bool[] check = { false,false,false };
    /*
     tableau de lights met les lum à la bonne couleur qd reçoit un input
     
     */
    private void Start()
    {
        PuzzleDoorButton.openDoor += AddColor;
    }

    private void AddColor(int n,Color c) {
       
        
        if (arraySize <= correctInput.Length)
        {
            inputs[arraySize] = n; // ajoute le nombre aux inputs
            colorCheck[arraySize].color = c;  // met la lumière au checker
            if (inputs[arraySize] == correctInput[arraySize])//si ça correspond coche la croix 
            {   
                check[arraySize] = true;
                 Debug.Log("array size = " +arraySize);
            }
            
            arraySize++; 
        }
       
        
            if (check[0] == true && check[1] == true && check[2] == true)//si c'est bon ouvre
                DoorOpen();
        
        if (arraySize == correctInput.Length && !open)//si on a fait le tour et pas ouvert
        {
            arraySize = 0; // reset 
            for (int i = 0; i < check.Length; i++) { //remet à 0 les checker
                check[i] = false;
                colorCheck[i].color = Color.white;
            }
        }
    }

    public void DoorOpen() {
       
            open = true;
            audio.PlayOneShot(audio.clip);
            anim.SetBool("Open", true);
            PuzzleDoorButton.openDoor -= AddColor; 
    }
}
