using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWalk 
{
    AudioClip[] footSteps { get; set; }
    AudioClip fallSound { get; set; }
    float speed { get; set; }
    float runSpeed { get; set; }
    
    void PlayFootStep();
}
