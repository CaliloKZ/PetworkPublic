using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemControl : MonoBehaviour
{
    private GameManager _gameManager;
    private DataStorage _data;

    [SerializeField]
    private GameObject _bowls,
                       _rug,
                       _houseBox,
                       _house2,
                       _cat;

    public bool haveHouse;



    private void Start()
    {
        _gameManager = GameManager.instance;
        _data = DataStorage.instance;
    }
    public void ChangeFoodItem(Item item)
    {
        _bowls.GetComponent<FoodBowl>().UpdateItemFood(item);
        _data.selectedFoodItem = item;
        _gameManager.selectedFoodItem = item;
    }

    public void ChangeWaterItem(Item item)
    {
        _bowls.GetComponent<FoodBowl>().UpdateItemWater(item);
        _data.selectedWaterItem = item;
        _gameManager.selectedWaterItem = item;
    }

    public void ChangeRugItem(Item item)
    {
        _rug.GetComponent<SpriteRenderer>().sprite = item.spr;
        _data.selectedRugItem = item;
        _gameManager.selectedRugItem = item;
    }

    public void ChangeHouseItem(Item item)
    {
        if(item.itemID == 14)
        {
            _house2.SetActive(false);
            _houseBox.SetActive(true);
            UIManager.instance.ChangeCatPos(2);
        }
        else
        {
            _houseBox.SetActive(false);
            _house2.SetActive(true);
            UIManager.instance.ChangeCatPos(1);
        }
        //_house.GetComponent<SpriteRenderer>().sprite = item.spr;
        _data.selectedHouseItem = item;
        _gameManager.selectedHouseItem = item;
    }
}
