using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public List<int> obtainedItems = new List<int>();
    public List<int> obtainedCats = new List<int>();
    public List<string> obtainedPhotos = new List<string>();
    public int points;
    public int currentLove,
               currentHunger,
               currentHealth;

    public int selectedFoodItem,
                  selectedWaterItem,
                  selectedRugItem,
                  selectedHouseItem,
                  selectedCat;

    public float timeCounterFood,
                 timeCounterWater,
                 timeCounterWithoutFood,
                 timeCounterWithoutWater,
                 timeToFillSandBox,
                 timeToHurtSandbox;

    public bool isFoodFull,
                isWaterFull,
                isSandBoxFull,
                isFoodWaterBowlActive,
                haveHouse;

    public bool tutoDone;

    public float sfxVolume,
                 musicVolume;

    public string lastSavedTimeAndDate;

    public SaveData (DataStorage data)
    {
        points = data.points;

        currentHealth = data.currentHealth;
        currentHunger = data.currentHunger;
        currentLove = data.currentLove;

        if(data.selectedFoodItem != null)
        {
            selectedFoodItem = data.selectedFoodItem.itemID;
        }

        if(data.selectedWaterItem != null)
        {
            selectedWaterItem = data.selectedWaterItem.itemID;
        }

        if (data.selectedRugItem != null)
        {
            selectedRugItem = data.selectedRugItem.itemID;
        }

        if (data.selectedHouseItem != null)
        {
            selectedHouseItem = data.selectedHouseItem.itemID;
        }

        if(data.selectedCat != null)
        {
            selectedCat = data.selectedCat.catID;
        }

        timeCounterFood = data.timeCounterFood;
        timeCounterWater = data.timeCounterWater;
        timeCounterWithoutFood = data.timeCounterWithoutFood;
        timeCounterWithoutWater = data.timeCounterWithoutWater;
        isFoodFull = data.isFoodFull;
        isWaterFull = data.isWaterFull;
        isFoodWaterBowlActive = data.isFoodWaterBowlActive;
        haveHouse = data.haveHouse;

        timeToFillSandBox = data.timeToFillSandBox;
        timeToHurtSandbox = data.timeToHurtSandbox;
        isSandBoxFull = data.isSandboxFull;

        tutoDone = data.tutoDone;

        sfxVolume = data.sfxVolume;
        musicVolume = data.musicVolume;

        foreach(Item item in data.obtainedItems)
        {
            obtainedItems.Add(item.itemID);
        }

        foreach(Cats cat in data.obtainedCats)
        {
            obtainedCats.Add(cat.catID);
        }

        foreach(GameObject photo in data.obtainedPhotos)
        {
            obtainedPhotos.Add(photo.name);
        }

        lastSavedTimeAndDate = data.lastSavedTimeAndDateStr;
    }
}
