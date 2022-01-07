using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    #region old
    //[SerializeField][Tooltip("//0 - comida, 1 - água, 2 - tapetes, 3 - camas")]
    //private int _type;//0 - comida, 1 - água, 2 - tapetes, 3 - camas
    //[SerializeField]
    //private int _index;

    //private InventoryControl _inventoryControl;

    //[SerializeField]
    //private GameObject _objImage;
    //private Button _slotBT;

    //public bool hasItem { get; private set; }
    //private bool _selected;

    //private void Awake()
    //{
    //    _inventoryControl = GetComponentInParent<InventoryControl>();
    //}

    //private void Start()
    //{
    //    if (hasItem)
    //    {
    //        _objImage.SetActive(true);
    //        _slotBT.interactable = true;
    //    }
    //}

    //public void ActivateImage()
    //{
    //    _objImage.SetActive(true);
    //}

    //public void OnClick()
    //{
    //    //_inventoryControl.SelectItem(_type, _index);
    //}
    #endregion

    [SerializeField]
    private InventoryControl _inventoryControl;

    [SerializeField]
    private bool _itemChecked;

    [SerializeField]
    private Item _item;

    [SerializeField]
    private Image _img;
    [SerializeField]
    private Button _bt;

    public bool hasItem { get; private set; }
    public int itemTypeID { get; private set; }

    //[HideInInspector]
    public bool selected;

    public Item GetItem()
    {
        return _item;
    }

    private void Start()
    {
        itemTypeID = _item.itemTypeID;
        if (_itemChecked)
        {
            hasItem = true;
        }
    }

    public void ActivateItem()
    {
        _img.gameObject.SetActive(true);
        _bt.interactable = true;
        hasItem = true;
    }

    public void OnClick()
    {
        if (selected)
        {
            return;
        }
        _inventoryControl.SelectItem(_item, gameObject);
        UIManager.instance.UIButtonSound();
    }


}
