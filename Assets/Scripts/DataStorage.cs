using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

public class DataStorage : MonoBehaviour
{
    public static DataStorage instance;

    public List<Item> obtainedItems = new List<Item>();
    public List<GameObject> obtainedPhotos = new List<GameObject>();
    public List<Cats> obtainedCats = new List<Cats>();
    public int points;
    public int currentLove,
               currentHunger,
               currentHealth;

    public Item selectedFoodItem,
                selectedWaterItem,
                selectedRugItem,
                selectedHouseItem;

    public Cats selectedCat;

    public float timeCounterFood,
                 timeCounterWater,
                 timeCounterWithoutFood,
                 timeCounterWithoutWater,
                 timeToFillSandBox,
                 timeToHurtSandbox;

    public bool isFoodFull,
                isWaterFull,
                isSandboxFull,
                isFoodWaterBowlActive,
                haveHouse;

    public bool tutoDone;

    public float sfxVolume,
                 musicVolume;

    private DateTime _currentTimeAndDate,
                     _lastSavedTimeAndDate;
    public string lastSavedTimeAndDateStr;

    private TimeSpan _timeDiff;
    private float _timeDiffInSeconds;

    
    [SerializeField]
    private FoodBowl _bowls;
    [SerializeField]
    private SandBox _sandBox;
    [SerializeField]
    private ItemControl _itemControl;
    [SerializeField]
    private GachaControl _gachaControl;
    [SerializeField]
    private ConfigControl _config;
    [SerializeField]
    private List<GameObject> _itemSlots = new List<GameObject>();
    [SerializeField]
    private List<GameObject> _catSlots = new List<GameObject>();
    [SerializeField]
    private GameObject _loadingScreen;
    [SerializeField]
    private List<Item> _itemsList = new List<Item>();
    [SerializeField]
    private List<Cats> _catsList = new List<Cats>();
    [SerializeField]
    private List<GameObject> _photosList = new List<GameObject>();
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("Another instance of DataStorage has been destroyed");
            Destroy(gameObject);
        }
    }

    public void SaveGame()
    {
        timeCounterFood = _bowls._timeCounterFood;
        timeCounterWater = _bowls._timeCounterWater;
        timeCounterWithoutFood = _bowls._timeCounterWithoutFood;
        timeCounterWithoutWater = _bowls._timeCounterWithoutWater;
        timeToFillSandBox = _sandBox._timeToFill;
        timeToHurtSandbox = _sandBox._timeUntilHurts;
        isFoodFull = _bowls._hasFood;
        isWaterFull = _bowls._hasWater;
        isFoodWaterBowlActive = _bowls._foodAndWaterBowlActive;
        isSandboxFull = _sandBox._isFull;
        lastSavedTimeAndDateStr = DateTime.UtcNow.ToString();
        haveHouse = _itemControl.haveHouse;
        sfxVolume = _config.GetSFXValue();
        musicVolume = _config.GetMusicValue();
        SaveSystem.SaveGame(this);
    }


    public IEnumerator<float> Loading()
    {
        //Time.timeScale = 0;
        _loadingScreen.SetActive(true);
        yield return Timing.WaitUntilDone(Timing.RunCoroutine(LoadGame().CancelWith(gameObject)));

    }

    IEnumerator<float> LoadGame()
    {
        //parte que recebe os dados
        SaveData data = SaveSystem.LoadGame();

        points = data.points;

        currentHealth = data.currentHealth;
        currentHunger = data.currentHunger;
        currentLove = data.currentLove;

        foreach(Item item in _itemsList)
        {
            if(item.itemID == data.selectedFoodItem)
            {
                selectedFoodItem = item;
                Debug.Log("selectedFoodItem found");
            }
            else if (item.itemID == data.selectedWaterItem)
            {
                selectedWaterItem = item;
                Debug.Log("selectedWaterItem found");
            }
            else if (item.itemID == data.selectedRugItem)
            {
                selectedRugItem = item;
                Debug.Log("selectedRugItem found");
            }
            else if (item.itemID == data.selectedHouseItem)
            {
                selectedHouseItem = item;
                Debug.Log("selectedHouseItem found");
            }
        }

        foreach(Cats cat in _catsList)
        {
            if(cat.catID == data.selectedCat)
            {
                selectedCat = cat;
            }
        }
        timeCounterFood = data.timeCounterFood;
        timeCounterWater = data.timeCounterWater;
        timeCounterWithoutFood = data.timeCounterWithoutFood;
        timeCounterWithoutWater = data.timeCounterWithoutWater;
        isFoodFull = data.isFoodFull;
        isWaterFull = data.isWaterFull;
        isFoodWaterBowlActive = data.isFoodWaterBowlActive;

        timeToFillSandBox = data.timeToFillSandBox;
        timeToHurtSandbox = data.timeToHurtSandbox;
        isSandboxFull = data.isSandBoxFull;

        tutoDone = data.tutoDone;

        sfxVolume = data.sfxVolume;
        musicVolume = data.musicVolume;

        foreach (int i in data.obtainedItems)
        {
            Debug.Log("debug10");
            obtainedItems.Add(_itemsList[i]);
            yield return Timing.WaitForSeconds(0.1f);
        }

        foreach(int i in data.obtainedCats)
        {
            obtainedCats.Add(_catsList[i]);
            yield return Timing.WaitForSeconds(0.1f);
        }

        foreach(string name in data.obtainedPhotos)
        {
            foreach(GameObject photo in _photosList)
            {
                if(photo.name == name)
                {
                    obtainedPhotos.Add(photo);
                    yield return Timing.WaitForSeconds(0.1f);
                }
            }
        }

        lastSavedTimeAndDateStr = data.lastSavedTimeAndDate;
        _lastSavedTimeAndDate = DateTime.Parse(lastSavedTimeAndDateStr);
        _currentTimeAndDate = DateTime.UtcNow;

        _timeDiff = _currentTimeAndDate - _lastSavedTimeAndDate;
        _timeDiffInSeconds = (float)_timeDiff.TotalSeconds;

        //parte que envia pro game os dados
        GameManager _gm = GameManager.instance;

        TutoManager.instance.LoadTuto(tutoDone);

        _gm.LoadPoints(points);
        _gm.LoadCurrentHealth(currentHealth);
        _gm.LoadCurrentHunger(currentHunger);
        _gm.LoadCurrentLove(currentLove);
        Debug.Log("debug01");
        foreach (Item item in obtainedItems)
        {
            _gm.LoadItem(item);
        }
        foreach(Cats cat in obtainedCats)
        {
            _gm.LoadCat(cat);
        }

        foreach (GameObject photo in obtainedPhotos)
        {
            _gm.LoadPhotos(photo);
        }

        _config.LoadConfigs(musicVolume, sfxVolume);

        haveHouse = data.haveHouse;
        _itemControl.haveHouse = haveHouse;

        foreach(GameObject slot in _itemSlots)
        {
            Item item = slot.GetComponent<InventoryItem>().GetItem();
            if (selectedFoodItem != null && item == selectedFoodItem)
            {
                slot.GetComponent<InventoryItem>().OnClick();
            }
            else if (selectedWaterItem != null && item == selectedWaterItem)
            {
                slot.GetComponent<InventoryItem>().OnClick();
            }
            else if (selectedRugItem != null && item == selectedRugItem)
            {
                slot.GetComponent<InventoryItem>().OnClick();

            }
            else if (haveHouse && selectedHouseItem != null && item == selectedHouseItem)
            {
                slot.GetComponent<InventoryItem>().OnClick();
            }
        }

        foreach(GameObject slot in _catSlots)
        {
            Cats cat = slot.GetComponent<CatItem>().GetCat();
            if(selectedCat != null && selectedCat == cat)
            {
                slot.GetComponent<CatItem>().OnClick();              
            }
        }

        foreach(Cats cat in obtainedCats)
        {
            _gachaControl.LoadCatsToPull(cat);
            yield return Timing.WaitForSeconds(0.1f);
        }
        //if(selectedWaterItem != null)
        //{
        //    _itemControl.ChangeWaterItem(selectedWaterItem);
        //}

        //if(selectedRugItem != null)
        //{
        //    _itemControl.ChangeRugItem(selectedRugItem);
        //}

        //if(selectedHouseItem != null && haveHouse)
        //{
        //    _itemControl.ChangeHouseItem(selectedHouseItem);
        //}

        Debug.Log("Time Diff in Seconds: " + _timeDiffInSeconds);
        if (tutoDone)
        {
            _bowls.LoadBowls(timeCounterFood, timeCounterWater, timeCounterWithoutFood, timeCounterWithoutWater, _timeDiffInSeconds, isFoodFull, isWaterFull, isFoodWaterBowlActive);
            _sandBox.LoadTime(timeToFillSandBox, timeToHurtSandbox, _timeDiffInSeconds, isSandboxFull);
        }
        yield return Timing.WaitForSeconds(0.5f);
        _loadingScreen.SetActive(false);
        //Time.timeScale = 1;

        yield return Timing.WaitForOneFrame;
    }
}
