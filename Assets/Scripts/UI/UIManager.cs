using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class UIManager : MonoBehaviour

{

    //    [SerializeField] LevelHandler _levelHandler;
    Hero _invocator, _pet;

    [SerializeField] Text _displaySpecialAmmo, _displayAmmo, _timerDisplay, _scoreDisplay;
    [SerializeField] Image _displaySpecialAmmoIcon, _displayAmmoIcon, _displayFaceIcon, _displayLifeColor;

    [SerializeField] Slider _displayPlayerHp;
    [SerializeField] GameObject _escapePanel;
    [SerializeField] Transform _KeyScroll;
    [SerializeField] Image _keyPrefab;
    Animator _fadeBlack;
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

    [SerializeField] Text afficheurTexte;
    PlayerPersistentDataHandler gameHander;
    public void SetUI(Hero hero)
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
        gameHander = GameObject.Find("GameHandler").GetComponent<PlayerPersistentDataHandler>();
        _playerHeroName = hero.name;
        _timeLeft = gameHander.GetComponent<LevelHandler>().timeToCompleteLevel;
        _displayPlayerHp.maxValue = hero.Health;
        _displaySpecialAmmoIcon.sprite = hero.specialAmmoIcon;
        _displayAmmoIcon.sprite = hero.ammoIcon;
        _displayFaceIcon.sprite = hero.faceIcon;
        _displayLifeColor.sprite = hero.lifeColor;
        _scoreDisplay.text = "Score : " + 
        GameObject.Find("GameHandler").GetComponent<PlayerPersistentDataHandler>().PlayerScore.ToString();

        StartDisplay();
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
        _scoreDisplay.text = "Score : " + 
        GameObject.Find("GameHandler").GetComponent<PlayerPersistentDataHandler>().PlayerScore.ToString();
        switch (_playerHeroName)
        {
            case "Jaznot":
                _displayPlayerHp.value =  gameHander.thisHero.Health;
                _displaySpecialAmmo.text = gameHander.thisHero.SpecialAmmo.ToString();
                _displayAmmo.text = _fuel.Fuel.ToString(); // lanceflamme.Fuel
                break;

            case "Invocator":

                _displayPlayerHp.value = _invocator.Health;
                _displaySpecialAmmo.text = _invocator.SpecialAmmo.ToString();
                _displayAmmo.text = _heroAmmo.ActualAmmoInClip.ToString();
                AssignColorToUi(_colors[1]);
                break;

            case "Pet":
                _displayPlayerHp.value = gameHander.masterInvoc.gameObject.GetComponent<Hero>().Health;
                _displaySpecialAmmo.text = gameHander.masterInvoc.gameObject.GetComponent<Hero>().SpecialAmmo.ToString();
                _displayAmmo.text = gameHander.masterInvoc._pet.EvadeCharges.ToString();
                AssignColorToUi(_colors[1]);
                CreateSpecialHud();
                break;

        }
    }

    void StartDisplay()
    {
        _scoreDisplay.text = "Score : " + 
        GameObject.Find("GameHandler").GetComponent<PlayerPersistentDataHandler>().PlayerScore.ToString();
        switch (_playerHeroName)
        {
            case "Jaznot":
                _displayPlayerHp.maxValue = gameHander.thisHero.Health;
                _displayPlayerHp.value = gameHander.thisHero.Health;
                _displaySpecialAmmo.text = gameHander.thisHero.SpecialAmmo.ToString();
                _displayAmmo.text = _fuel.Fuel.ToString(); // lanceflamme.Fuel
                break;

            case "Invocator":

                _displayPlayerHp.maxValue = gameHander.thisHero.Health;
                _displayPlayerHp.value = gameHander.thisHero.Health;
                _displaySpecialAmmo.text = gameHander.thisHero.SpecialAmmo.ToString();
                _displayAmmo.text = gameHander.thisHero.GetComponent<ShootSystem>().ActualAmmoInClip.ToString();
                AssignColorToUi(_colors[1]);
                break;

            case "Pet":
                _displayPlayerHp.maxValue = gameHander.masterInvoc._pet.gameObject.GetComponent<Hero>().Health;
                _displaySpecialAmmo.text = gameHander.masterInvoc._pet.GetComponent<Hero>().SpecialAmmo.ToString();
                _displayAmmo.text = gameHander.masterInvoc._pet.EvadeCharges.ToString();
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
            GameObject.Find("GameHandler").GetComponent<PlayerPersistentDataHandler>().EndLevel();
        

    }

    public void ExitAndSaveDataWithPersistenPlayer()
    {
        GameObject.Find("GameHandler").GetComponent<PlayerPersistentDataHandler>().SaveProgression();
    }


    void FadeToBlack()
    {
        
        Debug.Log("FADE");
        string path = Application.persistentDataPath + "/ProgressionDatas.json";
        GameObject.Find("FadeBlck").GetComponent<Animator>().SetBool("SetBlack",true);
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
           _displaySpecialAmmoIcon.sprite = gameHander.masterInvoc._pet.gameObject.GetComponent<Hero>().specialAmmoIcon;
           _displayAmmoIcon.sprite = gameHander.masterInvoc._pet.gameObject.GetComponent<Hero>().ammoIcon;
           _displayFaceIcon.sprite = gameHander.masterInvoc._pet.gameObject.GetComponent<Hero>().faceIcon;
           _displayLifeColor.sprite = gameHander.masterInvoc._pet.gameObject.GetComponent<Hero>().lifeColor;
    }
    public void DisplayBackInvocatorHud()
    {
        _displaySpecialAmmoIcon.sprite = gameHander.masterInvoc.gameObject.GetComponent<Hero>().specialAmmoIcon;
        _displayAmmoIcon.sprite = gameHander.masterInvoc.gameObject.GetComponent<Hero>().ammoIcon;
        _displayFaceIcon.sprite = gameHander.masterInvoc.gameObject.GetComponent<Hero>().faceIcon;
        _displayLifeColor.sprite = gameHander.masterInvoc.gameObject.GetComponent<Hero>().lifeColor;
    }

    public void AfficheTexte(string s) {
        afficheurTexte.text = s;
    }

    public void EffaceTexte()
    {
        afficheurTexte.text = " ";
    }
}

