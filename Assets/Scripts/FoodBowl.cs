using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class FoodBowl : MonoBehaviour //controla as 2 tigelas
{

    public bool _hasFood { get; private set; }
    public bool _hasWater { get; private set; }
    public float _timeCounterFood { get; private set; }
    public float _timeCounterWater { get; private set; }
    public float _timeCounterWithoutFood { get; private set; }
    public float _timeCounterWithoutWater { get; private set; }
    [SerializeField]
    private float _timeCounterFoodMax,
                  _timeCounterWaterMax,
                  _timeCounterWithoutFoodMax,
                  _timeCounterWithoutWaterMax;

    [SerializeField]
    private Item _itemFood,
                 _itemWater;

    [SerializeField]
    private SpriteRenderer _sprFood,
                           _sprWater;

    [SerializeField]
    private GameObject _fillFoodBT,
                       _fillWaterBT;

    [SerializeField]
    private TextMeshProUGUI _foodBowlTime,
                            _waterBowlTime;

    [SerializeField]
    private int _loveToAdd,
                _hungerToAdd,
                _loveToRemove,
                _hungerToRemove,
                _pointsToAdd,
                _loveToAddFood2,
                _loveToAddWater2,
                _hungerToAddFood2,
                _hungerToAddWater2,
                _loveToAddWater3,
                _hungerToAddWater3;

    public bool _foodAndWaterBowlActive { get; private set; } //checa se o comedouro está ativo

    [SerializeField]
    private SpriteRenderer _foodBowlSpr, //tigela de comida
                           _waterBowlSpr, //tigela de água
                           _foodContainerSpr, //parte de comida no comedouro
                           _waterContainerSpr; //parte de água no comedouro

    [SerializeField]
    private GameObject _FoodAndWaterContainer; //comedouro
    //[SerializeField]
    //private int _cost;
    [SerializeField]
    public float cameraFOV { get; private set; }

    private void Awake()
    {
        _timeCounterWithoutFood = _timeCounterWithoutFoodMax;
        _timeCounterWithoutWater = _timeCounterWithoutWaterMax;
    }

    void FixedUpdate()
    {
        if (_hasFood)
        {
            _timeCounterFood -= Time.deltaTime;
            float h = TimeSpan.FromSeconds(_timeCounterFood).Hours;
            float min = TimeSpan.FromSeconds(_timeCounterFood).Minutes;
            float sec = TimeSpan.FromSeconds(_timeCounterFood).Seconds;
            _foodBowlTime.text = h + ":" + min + ":" + sec;
            if (_timeCounterFood <= 0)
            {
                UpdateSpriteFood(false);
                _fillFoodBT.GetComponent<Button>().interactable = true;
                _hasFood = false;
            }
            //uiManager show counter
        }
        else
        {
            _timeCounterWithoutFood -= Time.deltaTime;
            if(_timeCounterWithoutFood <= 0)
            {
                _timeCounterWithoutFood = _timeCounterWithoutFoodMax;
                //reduz amor e comida
                GameManager.instance.AddRemoveLove(_loveToRemove, false);
                GameManager.instance.AddRemoveHunger(_hungerToRemove, false);
            }
        }

        if (_hasWater)
        {
            _timeCounterWater -= Time.deltaTime;
            float h = TimeSpan.FromSeconds(_timeCounterWater).Hours;
            float min = TimeSpan.FromSeconds(_timeCounterWater).Minutes;
            float sec = TimeSpan.FromSeconds(_timeCounterWater).Seconds;
            _waterBowlTime.text = h + ":" + min + ":" + sec;
            if (_timeCounterWater <= 0)
            {
                UpdateSpriteWater(false);
                _fillWaterBT.GetComponent<Button>().interactable = true;
                _hasWater = false;
            }
        }
        else
        {
            _timeCounterWithoutWater -= Time.deltaTime;
            if(_timeCounterWithoutWater <= 0)
            {
                _timeCounterWithoutWater = _timeCounterWithoutWaterMax;
                //reduz amor e comida
                GameManager.instance.AddRemoveLove(_loveToRemove, false);
                GameManager.instance.AddRemoveHunger(_hungerToRemove, false);
            }
        }
    }

    public void FillFood()
    {
        //if(GameManager.instance.points >= _cost)
        //{
        _timeCounterFood = _timeCounterFoodMax;
        _fillFoodBT.GetComponent<Button>().interactable = false;
        //aumenta amor e comida
        if (_foodAndWaterBowlActive)
        {
            GameManager.instance.AddRemoveLove(_loveToAddFood2, true);
            GameManager.instance.AddRemoveHunger(_hungerToAddFood2, true);
            GameManager.instance.AddPoints(_pointsToAdd);
        }
        else
        {
            GameManager.instance.AddRemoveLove(_loveToAdd, true);
            GameManager.instance.AddRemoveHunger(_hungerToAdd, true);
            GameManager.instance.AddPoints(_pointsToAdd);
        }
        //ganha amor e comida
        _hasFood = true;
        UpdateSpriteFood(true);
        SoundManager.instance.PlaySFX("Food");
        //}
    }

    public void FillWater()
    {
 
        _timeCounterWater = _timeCounterWaterMax;
        _fillWaterBT.GetComponent<Button>().interactable = false;
        //aumenta amor e comida
        if (_foodAndWaterBowlActive)
        {
            GameManager.instance.AddRemoveLove(_loveToAddWater3, true);
            GameManager.instance.AddRemoveHunger(_hungerToAddWater3, true);
            GameManager.instance.AddPoints(_pointsToAdd);
        }
        else if (_itemWater.itemID == 7 || _itemWater.itemID == 8 || _itemWater.itemID == 9)
        {
            GameManager.instance.AddRemoveLove(_loveToAddWater2, true);
            GameManager.instance.AddRemoveHunger(_hungerToAdd, true);
            GameManager.instance.AddPoints(_pointsToAdd);
        }
        else
        {
            GameManager.instance.AddRemoveLove(_loveToAdd, true);
            GameManager.instance.AddRemoveHunger(_hungerToAdd, true);
            GameManager.instance.AddPoints(_pointsToAdd);
        }
        _hasWater = true;
        UpdateSpriteWater(true);
        SoundManager.instance.PlaySFX("Water");
    }

    public void UpdateSpriteFood(bool full) //0 - vazio, 1 - cheio
    {
        if (_foodAndWaterBowlActive)
        {
            if (full)
            {
                _sprFood.gameObject.SetActive(true);
            }
            else
            {
                _sprFood.gameObject.SetActive(false);
            }

            return;
        }

        if (_itemFood != null)
        {
            if(!full)
            {
                _sprFood.sprite = _itemFood.emptySpr;
            }
            else
            {
                _sprFood.sprite = _itemFood.spr;
            }
        }
        else
        {
            Debug.LogError("Error FoodBowl.UpdateSpriteFood(full), var _itemFood = null");
        }
    }

    public void UpdateSpriteWater(bool full)
    {
        if (_foodAndWaterBowlActive)
        {
            if (full)
            {
                _sprWater.gameObject.SetActive(true);
            }
            else
            {
                _sprWater.gameObject.SetActive(false);
            }

            return;
        }        

        if (_itemWater != null)
        {
            if (!full)
            {
                _sprWater.sprite = _itemWater.emptySpr;
            }
            else
            {
                _sprWater.sprite = _itemWater.spr;
            }
        }
        else
        {
            Debug.LogError("Error FoodBowl.UpdateSpriteWater(full), var _itemWater = null");
        }
    }

    public void UpdateItemFood(Item item)
    {
        if(_itemFood.itemID == 6) //se o item equipado antes for o comedouro
        {
            ActivateDeactivateContainer(false);
        }

        _itemFood = item;
        if(_itemFood.itemID == 6) //se o item para equipar for o comedouro(o que tem comida e água)
        {
            ActivateDeactivateContainer(true);
            return;
        }

        if (_hasFood)
        {
            _sprFood.sprite = _itemFood.spr;
        }
        else
        {
            _sprFood.sprite = _itemFood.emptySpr;
        }
    }

    public void UpdateItemWater(Item item)
    {

        _itemWater = item;
        if (_foodAndWaterBowlActive)
        {
            return;
        }

        if (_hasWater)
        {
            _sprWater.sprite = _itemWater.spr;
        }
        else
        {
            _sprWater.sprite = _itemWater.emptySpr;
        }
    }

    public void ActivateDeactivateContainer(bool activate)
    {
        if (activate)
        {
            _foodAndWaterBowlActive = true;
            _sprFood.gameObject.SetActive(false);
            _sprFood = _foodContainerSpr;
            _sprWater.gameObject.SetActive(false);
            _sprWater = _waterContainerSpr;
            _FoodAndWaterContainer.SetActive(true);
            UpdateSpriteFood(_hasFood);
            UpdateSpriteWater(_hasWater);

        }
        else
        {
            _foodAndWaterBowlActive = false;
            _FoodAndWaterContainer.SetActive(false);
            _sprFood.gameObject.SetActive(false);
            _sprFood = _foodBowlSpr;
            _sprFood.gameObject.SetActive(true);
            _sprWater.gameObject.SetActive(false);
            _sprWater = _waterBowlSpr;
            _sprWater.gameObject.SetActive(true);
            UpdateSpriteFood(_hasFood);
            UpdateSpriteWater(_hasWater);
        }
    }

    public void LoadBowls(float timeFood, float timeWater, float timeWithoutFood, float timeWithoutWater, float timePassed, bool hasFood, bool hasWater, bool foodWaterActive)
    {
        _foodAndWaterBowlActive = foodWaterActive;
        //tigela de comida
        _hasFood = hasFood;
        float timePassedFood = timePassed;
        if (hasFood)
        {
            UpdateSpriteFood(true);
            _timeCounterFood = timeFood - timePassedFood;
            timePassedFood -= timeFood;
            if(_timeCounterFood <= 0)
            {
                _hasFood = false;
                UpdateSpriteFood(false);
                _timeCounterFood = 0;
            }
        }

        _fillFoodBT.GetComponent<Button>().interactable = !_hasFood;

        while (timePassedFood > 0)
        {
            _timeCounterWithoutFood = timeWithoutFood - timePassedFood;
            timePassedFood -= timeWithoutFood;
            if(_timeCounterWithoutFood <= 0)
            {
                GameManager.instance.AddRemoveLove(_loveToRemove, false);
                GameManager.instance.AddRemoveHunger(_hungerToRemove, false);
                timeWithoutFood = _timeCounterWithoutFoodMax;
            }
        }

        //tigela de água
        _hasWater = hasWater;
        float timePassedWater = timePassed;
        if (hasWater)
        {
            UpdateSpriteWater(true);
            _timeCounterWater = timeWater - timePassedWater;
            timePassedWater -= timeWater;
            if (_timeCounterWater <= 0)
            {
                _hasWater = false;
                UpdateSpriteWater(false);
                _timeCounterWater = 0;
            }
        }

        _fillWaterBT.GetComponent<Button>().interactable = !_hasWater;

        while (timePassedWater > 0)
        {
            _timeCounterWithoutWater = timeWithoutWater - timePassedWater;
            timePassedWater -= timeWithoutWater;
            if (_timeCounterWithoutWater <= 0)
            {
                GameManager.instance.AddRemoveLove(_loveToRemove, false);
                GameManager.instance.AddRemoveHunger(_hungerToRemove, false);
                timeWithoutWater = _timeCounterWithoutWaterMax;
            }
        }

        Debug.Log("BowlsLoaded");
    }
}
