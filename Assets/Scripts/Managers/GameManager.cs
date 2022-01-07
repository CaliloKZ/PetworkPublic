using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MEC;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private DataStorage _dataStorage;

    [SerializeField]
    private Camera _zoomCam;
    [SerializeField]
    private Transform _bowlsCamPos,
                      _sandboxCamPos,
                      _catCamPos,
                      _catBoxCamPos;
    private UIManager _uiManager;

    [SerializeField]
    private CatPlayControl _catPlayControl;

    public bool firstStart { get; private set; } = false;

    public bool restart { get; private set; } = false;

    public bool firstTimeBowls,
                firstTimeSandbox,
                firstTimeCat;

    private int _health = 10; //mudou para higiene
    private int _love;
    private int _hunger = 10; //mudou para comida

    [SerializeField]
    private int _healthMaxValue,
                _loveMaxValue,
                _hungerMaxValue,
                _healthStartValue,
                _loveStartValue,
                _hungerStartValue;

    public int points { get; private set; }
    [SerializeField]
    private int _vetCost;

    [SerializeField]
    private List<Item> _itemsList = new List<Item>();
    [SerializeField]
    private List<Cats> _catsList = new List<Cats>();
    public List<Item> _obtainedItems { get; private set; } = new List<Item>();
    public List<Cats> _obtainedCats { get; private set; } = new List<Cats>();
    [SerializeField]
    private InventoryControl _inventoryControl;
    [SerializeField]
    private GachaControl _gachaControl;

    [HideInInspector]
    public Item selectedFoodItem,
                selectedWaterItem,
                selectedRugItem,
                selectedHouseItem;

    [HideInInspector]
    public Cats selectedCat;

    [SerializeField]
    private List<GameObject> _photos = new List<GameObject>();
    private List<GameObject> _obtainedPhotos = new List<GameObject>();

    private float _catChangePosTime;
    [SerializeField]
    private float _catChangePosTimeMax;

    private bool _isCatHappy = true;

    [SerializeField]
    private int _minHealthForHappiness, //higiene e comida minima para o gato ficar feliz
                _minHungerForHappiness;

    [SerializeField]
    private float _snackCooldownMax,
                  _handCooldownMax,
                  _brushCooldownMax,
                  _featherCooldownMax;

    //private float _snackCooldown,
    //              _handCooldown,
    //              _brushCooldown,
    //              _featherCooldown;

    private float _catCooldowns;
    private bool _catCDOn;

    //private bool _snackCDOn,
    //             _handCDOn,
    //             _brushCDOn,
    //             _featherCDOn;

    [SerializeField]
    private int _snackHungerAdd,
                _snackHealthRemove,
                _handLoveAdd,
                _handHealthRemove,
                _brushLoveAdd,
                _brushHealthAdd,
                _featherLoveAdd;

    public GameObject gameover;

    private bool _canPlayAddStats = true; //define se as brincadeiras com o gato mudam o status

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("Another instance of GameManager has been destroyed");
            Destroy(gameObject);
        }

    }

    private void Start()
    {
        _uiManager = UIManager.instance;
        _dataStorage = DataStorage.instance;
        firstStart = PlayerPrefs.GetInt("FirstStart") == 1 ? true : false;
        restart = PlayerPrefs.GetInt("Restart") == 1 ? true : false;
        firstTimeBowls = PlayerPrefs.GetInt("BowlsFirst") == 1 ? true : false;
        firstTimeSandbox = PlayerPrefs.GetInt("SandboxFirst") == 1 ? true : false;
        firstTimeCat = PlayerPrefs.GetInt("CatFirst") == 1 ? true : false;

        if (!firstStart)
        {
            Debug.Log("FirstStart");
            _health = _healthStartValue;
            _hunger = _hungerStartValue;
            _love = _loveStartValue;
            _uiManager.UpdateHealth(_health);
            _uiManager.UpdateHunger(_hunger);
            _uiManager.UpdateLove(_love);
            AddPoints(1000);
            firstStart = true;
            PlayerPrefs.SetInt("FirstStart", firstStart ? 1 : 0);
            GameManager.instance.SaveGame();
            //starttutorial
        }
        else if(restart)
        {
            Debug.Log("Restart");
            TutoManager.instance.LoadTuto(true);
            _health = _healthMaxValue;
            _hunger = _hungerMaxValue;
            _love = 0;
            _uiManager.UpdateHealth(_health);
            _uiManager.UpdateHunger(_hunger);
            _uiManager.UpdateLove(_love);
            restart = false;
            PlayerPrefs.SetInt("Restart", restart ? 1 : 0);
            GameManager.instance.SaveGame();
        }
        else
        {
            Timing.RunCoroutine(DataStorage.instance.Loading().CancelWith(gameObject));
        }
        SoundManager.instance.PlayMusic("Background");
        _zoomCam.transform.position = _catBoxCamPos.position;
    }

    private void FixedUpdate()
    {
        _catChangePosTime -= Time.deltaTime;
        if(_catChangePosTime <= 0)
        {
            int r = UnityEngine.Random.Range(0, 2);
            UIManager.instance.ChangeCatPos(r);
            _catChangePosTime = _catChangePosTimeMax;
            Debug.Log("Test: " + r);
        }
        #region oldCooldowns

        //if (_snackCDOn)
        //{
        //    _snackCooldown -= Time.deltaTime;
        //    if(_snackCooldown <= 0)
        //    {

        //    }
        //}

        //if (_handCDOn)
        //{
        //    _handCooldown -= Time.deltaTime;
        //    if (_handCooldown <= 0)
        //    {

        //    }
        //}

        //if (_brushCDOn)
        //{
        //    _brushCooldown -= Time.deltaTime;
        //    if (_brushCooldown <= 0)
        //    {

        //    }
        //}

        //if (_featherCDOn)
        //{
        //    _featherCooldown -= Time.deltaTime;
        //    if (_featherCooldown <= 0)
        //    {

        //    }
        //}
        #endregion

        if (_catCDOn)
        {
            _catCooldowns -= Time.deltaTime;
            if (_catCooldowns <= 0)
            {
                _canPlayAddStats = true;
                //_uiManager.EnableDisablePlayBTs(true);
                _catCDOn = false;
            }
        }
    }

    public void AddRemoveHealth(int amount, bool add)
    {
        if (add)
        {
            _health += amount;
            if(_health > _healthMaxValue)
            {
                _health = _healthMaxValue;
            }

            if (!_isCatHappy && _health >= _minHealthForHappiness && _hunger >= _minHungerForHappiness)
            {
                SetHappiness(true);
            }
        }
        else
        {
            _health -= amount;
            if(_health < 3 && _hunger < 3)
            {
                SetHappiness(false);
            }
            
            if(_health <= 0)
            {
                _health = 0;
                SetHappiness(false);
                if (_hunger == 0)
                {
                    GameOver();
                    Debug.Log("Health and Food = 0, Game Over");
                }
            }
        }
        Timing.RunCoroutine(_uiManager.SpawnSoapStatus(add).CancelWith(gameObject));
        _dataStorage.currentHealth = _health;
        _uiManager.UpdateHealth(_health);
        Debug.Log("HealthUpdated: " + _health);
    }

    public void AddRemoveLove(int amount, bool add)
    {
        if (add && _isCatHappy)
        {
            _love += amount;
            if(_love >= _loveMaxValue)
            {
                _love = 0;
                AddPhoto();
            }
        }
        else
        {
            _love -= amount;
            if(_love < 0)
            {
                _love = 0;
            }
        }
        Timing.RunCoroutine(_uiManager.SpawnLoveStatus(add).CancelWith(gameObject));
        _dataStorage.currentLove = _love;
        _uiManager.UpdateLove(_love);
        Debug.Log("LoveUpdated: " + _love);
    }

    public void AddRemoveHunger(int amount, bool add)
    {
        if (add)
        {
            _hunger += amount;
            if(_hunger > _hungerMaxValue)
            {
                _hunger = _hungerMaxValue;
            }
            if (!_isCatHappy && _hunger >= _minHungerForHappiness && _health >= _minHealthForHappiness)
            {
                SetHappiness(true);
            }
        }
        else
        {
            _hunger -= amount;
            if(_hunger <= 3 && _health < 3)
            {
                SetHappiness(false);
            }

            if(_hunger <= 0)
            {
                _hunger = 0;
                SetHappiness(false);
                if (_health == 0)
                {
                    GameOver();
                    Debug.Log("Health and Food = 0, Game Over");
                }
            }
        }
        Timing.RunCoroutine(_uiManager.SpawnFoodStatus(add).CancelWith(gameObject));
        _dataStorage.currentHunger = _hunger;
        _uiManager.UpdateHunger(_hunger);
        Debug.Log("HungerUpdated: " + _hunger);
    }

    void SetHappiness(bool happy)
    {
        _isCatHappy = happy;
        _uiManager.SetHappiness(happy);
        _uiManager.VetBT(!happy);
    }

    public void GoToVetBT(bool yes)
    {
        if (yes)
        {
            if(points >= _vetCost)
            {
                RemovePoints(_vetCost);
                AddRemoveHunger(_hungerMaxValue, true);
                AddRemoveHealth(_healthMaxValue, true);
                _uiManager.OpenCloseVetPanel(false);
                _uiManager.VetBT(false);
            }
            else
            {
                _uiManager.OpenCloseNotEnoughWindow(true);
            }
        }
        else
        {
            _uiManager.OpenCloseVetPanel(false);
        }
    }

    public void AddItem(Item item)
    {
        _obtainedItems.Add(item);
        _dataStorage.obtainedItems.Add(item);
        _inventoryControl.UpdateInventory(item);
    }

    public void AddPoints(int amount)
    {
        points += amount;
        _dataStorage.points = points;
        _uiManager.UpdatePointsText(points);
    }

    public void RemovePoints(int amount)
    {
        points -= amount;
        if(points < 0)
        {
            points = 0;
        }
        _dataStorage.points = points;
        _uiManager.UpdatePointsText(points);
    }

    void AddPhoto()
    {
        if(_photos.Count > 0)
        {
            int r = UnityEngine.Random.Range(0, _photos.Count);
            _photos[r].SetActive(true);
            _obtainedPhotos.Add(_photos[r]);
            _dataStorage.obtainedPhotos.Add(_photos[r]);
            _uiManager.NewPhoto(_photos[r]);
            _photos.RemoveAt(r);
            SaveGame();
        }
        else
        {
            Debug.Log("No more photos to add");
            RollCat();
        }
    }

    public void RollCat()
    {
        _gachaControl.Roll();
    }

    public void AddCat(Cats cat)
    {
        _obtainedCats.Add(cat);
        _dataStorage.obtainedCats.Add(cat);
        _uiManager.NewCat(cat);
        _inventoryControl.UpdateCatInventory(cat);
        SaveGame();
    }

    #region loadArea
    public void LoadPoints(int amount)
    {
        points = amount;
        _uiManager.UpdatePointsText(points);
    }

    public void LoadCurrentHealth(int amount)
    {
        _health = amount;
        if(_health < 0)
        {
            _health = 0;
        }
        _uiManager.UpdateHealth(_health);
    }

    public void LoadCurrentHunger(int amount)
    {
        _hunger = amount;
        if (_hunger < 0)
        {
            _hunger = 0;
        }
        _uiManager.UpdateHunger(_hunger);
    }
    public void LoadCurrentLove(int amount)
    {
        _love = amount;
        if (_love < 0)
        {
            _love = 0;
        }
        _uiManager.UpdateLove(_love);
    }

    public void LoadPhotos(GameObject photo)
    {
        photo.SetActive(true);
        _obtainedPhotos.Add(photo);    
        _photos.Remove(photo);
    }

    public void LoadItem(Item item)
    {
        item.bought = true;
        //_obtainedItems.Add(item);
        _inventoryControl.UpdateInventory(item);
    }
    
    public void LoadCat(Cats cat)
    {
        cat.obtained = true;
        _inventoryControl.UpdateCatInventory(cat);
    }
    #endregion

    public void GiveSnack() //petisco
    {
        _uiManager.GiveSnack();
        //_uiManager.EnableDisablePlayBTs(false);
        if (_canPlayAddStats)
        {
            _canPlayAddStats = false;
            AddRemoveHunger(_snackHungerAdd, true);
            AddRemoveHealth(_snackHealthRemove, false);
            _catCooldowns = _snackCooldownMax;
            _catCDOn = true;
        }
        //_catCDTimer.SetActive(true);
    }

    public void HandBT()
    {
        _catPlayControl.SelectObj(0);
        _uiManager.SelectPlayObj(0);
    }

    public void UseBrush()
    {
        _catPlayControl.SelectObj(1);
        _uiManager.SelectPlayObj(1);
    }

    public void UseFeather()
    {
        _catPlayControl.SelectObj(2);
        _uiManager.SelectPlayObj(2);
    }

    public void FinishPlay(int objIndex)
    {

        if (!_canPlayAddStats)
        {
            return;
        }

        switch (objIndex) //0 = hand, 1 = brush, 2 = feather
        {
            case 0:
                 AddRemoveLove(_handLoveAdd, true);
                 AddRemoveHealth(_handHealthRemove, false);
                _catCooldowns = _handCooldownMax;
                break;
            case 1:
                AddRemoveLove(_brushLoveAdd, true);
                AddRemoveHealth(_brushHealthAdd, true);
                _catCooldowns = _brushCooldownMax;
                break;
            case 2:
                AddRemoveLove(_featherLoveAdd, true);
                _catCooldowns = _featherCooldownMax;
                break;
        }
        _canPlayAddStats = false;
        _catCDOn = true;
    }

    public void GameOver()
    {
        gameover.SetActive(true);
    }

    public void ResetScene()
    {
        PlayerPrefs.SetInt("Restart", 1);
        foreach(Item item in _itemsList)
        {
            item.bought = false;
        }
        foreach(Cats cat in _catsList)
        {
            cat.obtained = false;
        }
        SceneManager.LoadScene("MainScene");
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    public void SaveGame()
    {
        DataStorage.instance.SaveGame();
    }
    private void OnApplicationQuit()
    {
        Debug.Log("Quit");
        DataStorage.instance.SaveGame();
    }

}
