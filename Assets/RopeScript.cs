using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeScript : MonoBehaviour
{
  
    
    [SerializeField] Transform _t;
    float _f=0;
    void Update()
    {
        _f+=1f;
        _t.localScale = new Vector3(_t.localScale.x,_f,1);
    }
}
