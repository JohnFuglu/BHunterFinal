using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanceFlamme : MonoBehaviour
{
    //#region("Singleton")
    //private static LanceFlamme _instance;
    //public static LanceFlamme Instance
    //{
    //    get
    //    {
    //        if (_instance == null)
    //            Debug.LogError("LanceFlamme is NULL !");
    //        return _instance;


    //    }
    //}
   
    //#endregion



    [SerializeField] private float _fuelChecker, _fuelConso;
    public float Fuel { get { return _fuelChecker;} set { _fuelChecker = value; } }
    public float FuelConso { get { return _fuelConso; } set { _fuelConso = value; } }

    [SerializeField] ParticleSystem _flames;
    public AudioClip flameThSound;


    private void OnParticleCollision(GameObject other)
    {
        FireSpawner fire = new FireSpawner(other, _flames);

        //if(other.CompareTag("Destructibles")|| other.CompareTag("Ennemis") && !other.GetComponent<StandardObject>().Destroyed) 
        //{
        //    if (!other.GetComponentInChildren<FireDamage>())
        //    {
        //        ParticleSystem flames = Instantiate(_flames) as ParticleSystem;
        //        flames.Pause();
        //        flames.transform.SetParent(other.transform);
        //        flames.main.customSimulationSpace.localScale = other.transform.localScale;
        //        flames.transform.position = other.transform.position;
        //        flames.Play();   
        //    }
        //}
       
    }
    public void LanceFlammeAttack() 
    {
        Fuel -= FuelConso;
    }
}
#region("avant")
////[SerializeField] float puissanceFeu;
//bool detruit;
//// [SerializeField] LayerMask layer;
//private void OnParticleCollision(GameObject other)
//{
//    Debug.Log("Particules collide avec : " + other.name);
//    if (other.CompareTag("Destructibles"))
//    {
//        Destroy(other);
//    }
//}
#endregion