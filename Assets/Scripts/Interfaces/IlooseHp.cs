using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILooseHp
{
    int Health { get; set; }

    void TakeDamage(int damageAmount);
}
