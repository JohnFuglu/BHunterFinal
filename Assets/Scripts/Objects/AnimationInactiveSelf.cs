using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationInactiveSelf : MonoBehaviour
{
    public bool finishedAnim = false;
   public void SetInactive() 
   {
        gameObject.SetActive(false);
        finishedAnim = true;
   }
}
