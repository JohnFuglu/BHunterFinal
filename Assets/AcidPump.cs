using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidPump : ActionOnDestroy
{
    [SerializeField]Transform[] sprites;
    [SerializeField]AcidWater[] acidSpots;
    [SerializeField]BuoyancyEffector2D[] effectors;
    [SerializeField]Collider2D[] colls;
    [SerializeField]bool bouton=false;
    void Start(){
        acidSpots= new AcidWater[sprites.Length];
        effectors= new BuoyancyEffector2D[sprites.Length];
        colls= new Collider2D[sprites.Length];
        for(int i =0;i<sprites.Length;i++){
           acidSpots[i]= sprites[i].GetComponent<AcidWater>();
           effectors[i]= sprites[i].GetComponent<BuoyancyEffector2D>();
           colls[i]= sprites[i].GetComponent<Collider2D>();
        }
    }
    void Pump(){
        foreach(Transform s in sprites){
           float y = s.localScale.y;
           y -= (y *90)/ 100;
           s.localScale = new Vector2(s.localScale.x,y);
           Vector2 pos = new Vector2(0,s.localPosition.y-3.5f);
           s.Translate(pos);
        }
    }
    public override void Action()
    {
       Pump();
    }
    IEnumerator Do(){
        yield return new WaitForSeconds(3f);
    }
}
