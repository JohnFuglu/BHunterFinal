using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectPooling {
public interface IPoollable 
{
   string NameInPool { get; set; }

    void Hide();
}
 }