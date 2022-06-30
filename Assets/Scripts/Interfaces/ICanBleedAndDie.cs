using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICanBleedAndDie
{
     ParticleSystem bloodParticle { get; set; }
     GameObject bloodGo { get; set; }
     void WoundedBlood();
     void PlayWoundedSound();
     void PlayDeathSound();
}
