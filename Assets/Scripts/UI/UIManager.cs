using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class UIManager : MonoBehaviour

{
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogWarning("UiManager est null");
            return _instance;
        }
    }


    //    [SerializeField] LevelHandler _levelHandler;
    Hero _invocator, _pet;

    [SerializeField] Text _displaySpecialAmmo, _displayAmmo, _timerDisplay, _scoreDisplay;
    [SerializeField] Image _displaySpecialAmmoIcon, _displayAmmoIcon, _displayFaceIcon, _displayLifeColor;

    [SerializeField] Slider _displayPlayerHp;
    [SerializeField] GameObject _escapePanel;
    [SerializeField] Transform _KeyScroll;
    [SerializeField] Image _keyPrefab;
    [SerializeField] Animator _fadeBlack;
    [SerializeField] Image[] keys = new Image[4];
    [SerializeField] Text[] _allUiTexts;
    [SerializeField] Color[] _colors;  // par ordre de création

    #region("Invocator special Hud")
    //[SerializeField] Image _petdisplaySpecialAmmoIcon, _petdisplayAmmoIcon, _petdisplayLifeColor, _petdisplayFaceIcon;
    #endregion
    bool escapePanelBoolOn = false;
    [SerializeField] GameObject _gameOverScreen, _victoryScreen;
    public string _playerHeroName = "";
    //float _playerHp;
    float _timeLeft;


    LanceFlamme _fuel;
    ShootSystem _heroAmmo; // pour les chargeurs de Royale ... sinon juste in int/float
    Pet _thePet;

    [SerializeField] TextMeshProUGUI afficheurTexte;


    private void Awake()
    {
        _instance = this;
    }
    private void Start()
    {
        if (GameObject.Find("Invocator") != null)
        {
            GameObject tempHero = GameObject.Find("Invocator");

            if (tempHero)
            {
                _heroAmmo = tempHero.GetComponent<ShootSystem>();
                _invocator = tempHero.GetComponent<Hero>();
                //Invocator.becomeThePet += ThisIsAPet;
            }
        }

        if (GameObject.Find("Jaznot") != null)
        {
            _fuel = GameObject.Find("LanceFlamme").GetComponent<LanceFlamme>();
        }



        _playerHeroName = Hero.Instance.name;
        _timeLeft = LevelHandler.Instance.timeToCompleteLevel;
        _displayPlayerHp.maxValue = Hero.Instance.Health;
        _displaySpecialAmmoIcon.sprite = Hero.Instance.specialAmmoIcon;
        _displayAmmoIcon.sprite = Hero.Instance.ammoIcon;
        _displayFaceIcon.sprite = Hero.Instance.faceIcon;
        _displayLifeColor.sprite = Hero.Instance.lifeColor;
        _scoreDisplay.text = "Score : " + PlayerPersistentDataHandler.Instance.PlayerScore.ToString();

        StartDisplay();

        PlayerPersistentDataHandler.endTheLevel += FadeToBlack;
        KeySimple.displayKey += DisplayKey;
    }

    void ThisIsAPet()
    {
        _playerHeroName = "Pet";
        CreateSpecialHud();
    }
    private void Update()
    {
        Timer(_timeLeft);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
                   _escapePanel.SetActive(true);
        }
        else if (Input.GetKeyUp(KeyCode.Escape))
            _escapePanel.SetActive(false);
    }
    private void FixedUpdate()
    {
        RefreshDisplay();
    }

    void RefreshDisplay()
    {
        _scoreDisplay.text = "Score : " + PlayerPersistentDataHandler.Instance.PlayerScore.ToString();
        switch (_playerHeroName)
        {
            case "Jaznot":
                _displayPlayerHp.value = Hero.Instance.Health;
                _displaySpecialAmmo.text = Hero.Instance.SpecialAmmo.ToString();
                _displayAmmo.text = _fuel.Fuel.ToString(); // lanceflamme.Fuel
                break;

            case "Invocator":

                _displayPlayerHp.value = _invocator.Health;
                _displaySpecialAmmo.text = _invocator.SpecialAmmo.ToString();
                _displayAmmo.text = _heroAmmo.ActualAmmoInClip.ToString();
                AssignColorToUi(_colors[1]);
                break;

            case "Pet":
                _displayPlayerHp.value = Pet.Instance.gameObject.GetComponent<Hero>().Health;
                _displaySpecialAmmo.text = Pet.Instance.gameObject.GetComponent<Hero>().SpecialAmmo.ToString();
                _displayAmmo.text = Pet.Instance.EvadeCharges.ToString();
                AssignColorToUi(_colors[1]);
                CreateSpecialHud();
                break;

        }
    }

    void StartDisplay()
    {
        _scoreDisplay.text = "Score : " + PlayerPersistentDataHandler.Instance.PlayerScore.ToString();
        switch (_playerHeroName)
        {
            case "Jaznot":
                _displayPlayerHp.maxValue = Hero.Instance.Health;
                _displayPlayerHp.value = Hero.Instance.Health;
                _displaySpecialAmmo.text = Hero.Instance.SpecialAmmo.ToString();
                _displayAmmo.text = _fuel.Fuel.ToString(); // lanceflamme.Fuel
                break;

            case "Invocator":

                _displayPlayerHp.maxValue = _invocator.Health;
                _displayPlayerHp.value = _invocator.Health;
                _displaySpecialAmmo.text = _invocator.SpecialAmmo.ToString();
                _displayAmmo.text = _heroAmmo.ActualAmmoInClip.ToString();
                AssignColorToUi(_colors[1]);
                break;

            case "Pet":
                _displayPlayerHp.maxValue = Pet.Instance.gameObject.GetComponent<Hero>().Health;
                _displaySpecialAmmo.text = Pet.Instance.gameObject.GetComponent<Hero>().SpecialAmmo.ToString();
                _displayAmmo.text = Pet.Instance.EvadeCharges.ToString();
                AssignColorToUi(_colors[2]);
                CreateSpecialHud();
                break;

        }
    }



    void AssignColorToUi(Color color)
    {
        foreach (var text in _allUiTexts)
        {
            text.color = color;
        }
    }

    void Timer(float timeToGetHints)
    {
        _timeLeft -= Time.deltaTime;
        _timerDisplay.text = "Time left :" + Mathf.Round(_timeLeft);
        if (_timeLeft < 0 && !_gameOverScreen.activeInHierarchy)
        {
            PlayerPersistentDataHandler.Instance.EndLevel();
        }

    }

    public void ExitAndSaveDataWithPersistenPlayer()
    {
        PlayerPersistentDataHandler.Instance.SaveProgression();
    }


    void FadeToBlack()
    {
        
        Debug.Log("FADE");
        string path = Application.persistentDataPath + "/ProgressionDatas.json";
     
        _fadeBlack.SetBool("SetBlack",true);
        
        if (path != null)
        {
            System.IO.File.Delete(path);
        }
    
        StartCoroutine(DeathScreen());
    }

    IEnumerator DeathScreen()
    {
        yield return new WaitForSeconds(1.3f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }




    void DisplayKey(Sprite sprite)
    {
        Debug.Log(sprite.name);
        bool spawned = false;
        foreach (Image g in keys)
        {
            if (g.sprite.name == sprite.name && !spawned)
            {
                Debug.Log("G = " + g.sprite.name);
                Image i = Instantiate(g)as Image;
                i.transform.SetParent(_KeyScroll, false);
                spawned = true;
            } 
        }
    }

    void DisplayPetHut(Pet pet)
    {
       // _playerHp = pet.GetComponent<Hero>().Health;
        _displayPlayerHp.value = pet.GetComponent<Hero>().Health;
        _displaySpecialAmmo.text = pet.TeleportationCharges.ToString();
        _displayAmmo.text = pet.EvadeCharges.ToString();
    }

    void CreateSpecialHud()
    {
           _displaySpecialAmmoIcon.sprite = Pet.Instance.gameObject.GetComponent<Hero>().specialAmmoIcon;
           _displayAmmoIcon.sprite = Pet.Instance.gameObject.GetComponent<Hero>().ammoIcon;
           _displayFaceIcon.sprite = Pet.Instance.gameObject.GetComponent<Hero>().faceIcon;
           _displayLifeColor.sprite = Pet.Instance.gameObject.GetComponent<Hero>().lifeColor;
    }
    public void DisplayBackInvocatorHud()
    {
        _displaySpecialAmmoIcon.sprite = Invocator.Instance.gameObject.GetComponent<Hero>().specialAmmoIcon;
        _displayAmmoIcon.sprite = Invocator.Instance.gameObject.GetComponent<Hero>().ammoIcon;
        _displayFaceIcon.sprite = Invocator.Instance.gameObject.GetComponent<Hero>().faceIcon;
        _displayLifeColor.sprite = Invocator.Instance.gameObject.GetComponent<Hero>().lifeColor;
    }

    public void AfficheTexte(string s) {
        afficheurTexte.text = s;
    }

    public void EffaceTexte()
    {
        afficheurTexte.text = " ";
    }
}

public class InvocatorStats 
{
    private float _hp;
    private int _ammo;
    private int _specialAmmo;
    private int _score;

    public InvocatorStats(float hp, int ammo,int specialAmmo,int score) 
    {
        _hp = hp;
        _ammo = ammo;
        _specialAmmo = specialAmmo;
        _score = score;
    }
 

}

