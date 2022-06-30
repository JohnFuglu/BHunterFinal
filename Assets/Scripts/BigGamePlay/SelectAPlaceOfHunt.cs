using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class SelectAPlaceOfHunt : MonoBehaviour
{
    #region("Singleton")
    private static SelectAPlaceOfHunt _instance;
    public static SelectAPlaceOfHunt Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("No selectPlaceOfHunt");
            return _instance;
        }
    }    
    #endregion

    public bool allHints=false;
    private string _bossName;
    private Load _load;
    private Save _save;
    [SerializeField] Button _backButton;
    [SerializeField] private DataClass _dataLoaded;
    //private List<string> _visitedPlaces= new List<string>();
    [Header("Hints display")]
    public Hint[] collectedHints = new Hint[4];
    [SerializeField] Transform _hintDisplay;
    [SerializeField] Button _buttonPrefab;
    [SerializeField] Button bossButton;
    //  [SerializeField] Text _hintDescription;

    [SerializeField] TextMeshProUGUI movesLeft;
    [SerializeField] Text hintsDisplay;

    public RectTransform zoomedPanelTransform;
    
    public void HuntTheBoss()//Boss target
    {
        if (_dataLoaded.hintIds.Count<int>() == 4)
            allHints = true;
        if (allHints) 
        {
            bossButton.interactable=true;

            switch (_dataLoaded.currentTarget) {
                case "Canibalecter":
                    bossButton.GetComponentInChildren<Text>().text = "Canibalecter's lair";
                    
                break;
            
            }
           /* _bossName= target.name;
            switch (_bossName) 
            {
                case "Canibalecter":
                    UnityEngine.SceneManagement.SceneManager.LoadScene("Canibalecter");
                    break;

                    //autres cas !!!!
            }*/
        }    
    }

    private void Awake()
    {
        _instance = this;
       
    }

    
    private void Start()
    {
        _load = GetComponent<Load>();
        _dataLoaded=CheckForData();
        movesLeft.text = "You've got " + _dataLoaded.movesLeft + " movements left.";
        LoadHintsFromResources();
        HuntTheBoss();
    }

    public DataClass CheckForData()
    {
        //load le fichier de save et charge la scene du dernier level
        _dataLoaded= new DataClass();
        string path = Application.persistentDataPath + "/ProgressionDatas.json";
        var json = System.IO.File.ReadAllText(path);
        if (json != null)//second test à faire pour check si les indices sont réunis
        {
            _dataLoaded = _load.loadDataFromJson(json);

            if (_dataLoaded.heroName != null)
            {
                _backButton.interactable = false;
            }
            return _dataLoaded;
        }
        Debug.LogWarning("Null json!!");
        hintsDisplay.text = "You've found " + _dataLoaded.hintIds.Count + " hint(s)";
        return _dataLoaded;
    }

    void LoadHintsFromResources() 
    {   
        collectedHints = Resources.LoadAll<Hint>("ScriptableObjects/Hints/"+_dataLoaded.currentTarget);
        DisplayCollectedHints();
    }


    void CheckForVisitedPlaces(DataClass data) 
    { 
        foreach(string places in data.visitedLevel) // foreach(string places in _visitedPlaces) 
        {
            UnityEngine.UI.Button button = GameObject.Find(places).GetComponent<UnityEngine.UI.Button>();
            button.interactable = false;
        }
    }

    public void DisplayCollectedHints() 
    {
      //  int i = 0;
     int y = 0;
            foreach(var id in _dataLoaded.hintIds) 
            {
               Button hint = Instantiate(_buttonPrefab)as Button;
               HintButton hBut= hint.GetComponent<HintButton>();
                foreach (Hint ht in collectedHints) 
                {
                   if (id == ht.hintNumber)
                    {
                        hint.image.sprite = ht.descriptionImage;
                        hBut.imageOfHint = ht.descriptionImage;
                        hBut.descritpionTextHint = ht.description;
                    }
                 }

           
                hint.GetComponentInChildren<Text>().text = collectedHints[y].nameOfHint;
                 if (y < collectedHints.Length)
                 y++;
               // hint.GetComponent<HintButton>().buttonNumber = i;
              //  i++;
                hint.transform.SetParent(_hintDisplay);  
            }
        
    }

    public void ChooseAPlaceToGo(string place)
    {
        _dataLoaded.movesLeft--;
        Save save = new Save();
        save.SaveData(_dataLoaded);
        UnityEngine.SceneManagement.SceneManager.LoadScene(place);
    }

    public void GoBack()
    {
        if (_dataLoaded.heroName == null)
             UnityEngine.SceneManagement.SceneManager.LoadScene("TargetSelection");
    }
}

//par clic afficher dans le cadre prévu une carte d'indice
//image
//description textuelle
