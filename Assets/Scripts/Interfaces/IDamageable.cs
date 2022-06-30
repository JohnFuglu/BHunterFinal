using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{ 
    float Health { get; set; }

    void TakeDamage(int damageAmount);//faire surchgage float
    void TakeDamage(float damageAmount);
}
