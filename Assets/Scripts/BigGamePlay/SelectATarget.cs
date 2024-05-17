using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectATarget : MonoBehaviour
{
   
    Load _load;
    public DataClass _previousData;

    private void Awake()
    {
        _load = GetComponent<Load>();
       
    }
 
    public void SelectATargetAndMoveAhead(string target) 
   {
        Save saveTarget = new Save();
        _previousData = LoadCurrentProgression(_previousData);
        _previousData.currentTarget = target; 
        saveTarget.SaveData(_previousData);
        UnityEngine.SceneManagement.SceneManager.LoadScene("TownMap");
       // StartCoroutine(Delay(1f));
       
   }

    public void GoBack()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

   DataClass LoadCurrentProgression(DataClass previous)
    {
        string path = Application.persistentDataPath + "/ProgressionDatas.json";
        var json = System.IO.File.ReadAllText(path);
        previous = _load.loadDataFromJson(json);
        return previous;
    }
   
    IEnumerator Delay(float tps) 
    {
        yield return new WaitForSeconds(tps);
        UnityEngine.SceneManagement.SceneManager.LoadScene("TownMap");
    }
}
