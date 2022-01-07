using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreItem : MonoBehaviour
{
    [SerializeField]
    private Item _item;

    [SerializeField]
    private StoreControl _storeControl;

    [SerializeField]
    private TextMeshProUGUI _priceText;
    [SerializeField]
    private Image _img;
    private Button _bt;
    private int _itemID;
    private int _price;

    private void Awake()
    {
        _bt = GetComponent<Button>();
    }
    private void Start()
    {
        if (_item.bought)
        {
            DeactivateBT();
        }

        _img.sprite = _item.spr;
        _price = _item.price;
        _priceText.text = _price.ToString();
        _itemID = _item.itemID;
    }

    public void OnClick()
    {
        _storeControl.OpenConfirmWindow(_item, this);
        UIManager.instance.UIButtonSound();
    }

    public void DeactivateBT()
    {
        _bt.interactable = false;
    }

}
