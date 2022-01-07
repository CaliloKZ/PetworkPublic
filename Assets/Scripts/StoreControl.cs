using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreControl : MonoBehaviour
{
    [SerializeField]
    private GameObject _panelFood,
                       _panelRugs,
                       _panelBeds;

    [SerializeField]
    private GameObject _confirmWindow;
    [SerializeField]
    private Image _confirmImg;
    [SerializeField]
    private TextMeshProUGUI _confirmDesc;

    private Item _itemToBuy;
    private StoreItem _currentStoreItem;


    public void FoodBT()
    {
        _panelFood.SetActive(true);
        _panelRugs.SetActive(false);
        _panelBeds.SetActive(false);
    }
    public void RugsBT()
    {
        _panelFood.SetActive(false);
        _panelRugs.SetActive(true);
        _panelBeds.SetActive(false);
    }
    public void BedsBT()
    {
        _panelFood.SetActive(false);
        _panelRugs.SetActive(false);
        _panelBeds.SetActive(true);
    }

    public void OpenConfirmWindow(Item item, StoreItem sItem)
    {
        _itemToBuy = item;
        _currentStoreItem = sItem;
        _confirmImg.sprite = _itemToBuy.spr;
        _confirmDesc.text = _itemToBuy.itemDesc;
        _confirmWindow.SetActive(true);
    }

    public void CloseConfirmWindow()
    {
        _confirmWindow.SetActive(false);
    }

    public void BuyItem()
    {
        if(GameManager.instance.points >= _itemToBuy.price)
        {
            _itemToBuy.bought = true;
            _currentStoreItem.DeactivateBT();
            GameManager.instance.AddItem(_itemToBuy);
            GameManager.instance.RemovePoints(_itemToBuy.price);
            SoundManager.instance.PlaySFX("Buy");
        }
        else
        {
            UIManager.instance.OpenCloseNotEnoughWindow(true);
        }
        _confirmWindow.SetActive(false);

        GameManager.instance.SaveGame();
    }
}
