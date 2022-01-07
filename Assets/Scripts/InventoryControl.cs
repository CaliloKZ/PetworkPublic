using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MEC;
using TMPro;

public class InventoryControl : MonoBehaviour
{
    #region old
    //[SerializeField]
    //private GameObject[] _slotContainers,
    //                     _foodBowls,
    //                     _waterBowls,
    //                     _rugs,
    //                     _beds,
    //                     _slots; //0 - ComidaAzul, 1 - ÁguaAzul, 2 - ComidaVermelho, 3 - ÁguaVermelho
    //                              //4 - ComidaVerde, 5 - ÁguaVerde, 6 - BancadaComida, 7 - ÁguaFonteAzul
    //                              //8 - ÁguaFonteVermelho, 9 - ÁguaFonteVerde, 10 - TapeteAmarelo, 11 -TapeteVermelho
    //                              //12 - TapeteAzul, 13 - TapeteVerde, 14 - CaixaPapelão, 15 - BrinquedoCasa


    //[SerializeField]
    //private Button _backBT,
    //               _rightBT,
    //               _leftBT;

    //[SerializeField]
    //private Sprite[] _slotSprites; //0 - não selecionado, 1 - selecionado

    //private List<GameObject[]> _itemTypes = new List<GameObject[]>(); //0 - comida, 1 - água, 2 - tapetes, 3 - camas

    //private void Awake()
    //{
    //    _itemTypes.Add(_foodBowls);
    //    _itemTypes.Add(_waterBowls);
    //    _itemTypes.Add(_rugs);
    //    _itemTypes.Add(_beds);
    //}

    //public void RightBT()
    //{
    //    _slotContainers[1].SetActive(true);
    //    _slotContainers[0].SetActive(false);
    //    _rightBT.gameObject.SetActive(false);
    //    _leftBT.gameObject.SetActive(true);
    //}

    //public void LeftBT()
    //{
    //    _slotContainers[0].SetActive(true);
    //    _slotContainers[1].SetActive(false);
    //    _rightBT.gameObject.SetActive(true);
    //    _leftBT.gameObject.SetActive(false);
    //}

    //public void ItemBought(int index)
    //{
    //    _slots[index].GetComponent<Button>().interactable = true;
    //    _slots[index].GetComponent<InventoryItem>().ActivateImage();
    //}

    //public void SelectItem(int type, int index)
    //{
    //    GameObject[] selectedArray = _itemTypes[type];
    //    foreach(GameObject slot in selectedArray) //reseta a sprite de todos os objs do tipo para não selecionada
    //    {
    //        slot.GetComponent<Image>().sprite = _slotSprites[0];
    //    }
    //    selectedArray[index].GetComponent<Image>().sprite = _slotSprites[1];
    //    switch (type)
    //    {
    //        case 0:
    //            //GameManager.instance.selectedFoodItem = 
    //            break;
    //        case 1:
    //            break;
    //        case 2:
    //            break;
    //        case 3:
    //            break;
    //        default:
    //            Debug.LogError("Error InventoryControl.SelectItem(type, index), int type = " + type);
    //            break;
    //    }
    //}

    //public void UpdateItems()
    //{
    //    foreach(Item item in GameManager.instance._obtainedItems)
    //    {
    //        _slots[item.itemID].SetActive(true);
    //    }
    //}
    #endregion
    [SerializeField]
    private GameObject[] _slotContainers,
                         _foodSlots,
                         _waterSlots,
                         _rugSlots,
                         _houseSlots,
                         _slots, // 0 - comida azul, 1 - água azul, 2 - comida vermelha, 3 - água vermelha,
                                 // 4 - comida verde, 5 - água verde, 6 - comedouro, 7 - água 2 azul,
                                 // 8 - água 2 vermelho, 9 - água 2 verde, 10 - tapete amarelo, 11 - tapete vermelho
                                 // 12 - tapete azul, 13 - tapete verde, 14 - caixa, 15 - casinha
                                 //(igual ao ItemID)
                         _catSlots,
                         _catSlotsContainer;

    private bool _isCat;


    [SerializeField]
    private Button _backBT,
                   _rightBT,
                   _leftBT,
                   _itemsBT,
                   _catsBT;

    [SerializeField]
    private Sprite[] _slotSprites; //0 - não selecionado, 1 - selecionado

    [SerializeField]
    private ItemControl _itemControl;
    [SerializeField]
    private CatsControl _catControl;

    [SerializeField]
    private GameObject _panelCats;

    private void Awake()
    {
        
    }

    public void RightBT()
    {
        if (!_isCat)
        {
            _slotContainers[1].SetActive(true);
            _slotContainers[0].SetActive(false);
            _rightBT.gameObject.SetActive(false);
            _leftBT.gameObject.SetActive(true);
            UIManager.instance.UIButtonSound();
        }
        else
        {
            _catSlotsContainer[0].SetActive(false);
            _catSlotsContainer[1].SetActive(true);
            _rightBT.gameObject.SetActive(false);
            _leftBT.gameObject.SetActive(true);
            UIManager.instance.UIButtonSound();
        }

    }

    public void LeftBT()
    {
        if (!_isCat)
        {
            _slotContainers[0].SetActive(true);
            _slotContainers[1].SetActive(false);
            _rightBT.gameObject.SetActive(true);
            _leftBT.gameObject.SetActive(false);
            UIManager.instance.UIButtonSound();
        }
        else
        {
            _catSlotsContainer[0].SetActive(true);
            _catSlotsContainer[1].SetActive(false);
            _rightBT.gameObject.SetActive(true);
            _leftBT.gameObject.SetActive(false);
            UIManager.instance.UIButtonSound();
        }
    }

    public void ShowCats()
    {
        _isCat = true;
        if (_slotContainers[0].activeInHierarchy)
        {
            _catSlotsContainer[0].SetActive(true);
            _catSlotsContainer[1].SetActive(false);
            _rightBT.gameObject.SetActive(true);
            _leftBT.gameObject.SetActive(false);
        }
        else if (_slotContainers[1].activeInHierarchy)
        {
            _catSlotsContainer[0].SetActive(false);
            _catSlotsContainer[1].SetActive(true);
            _rightBT.gameObject.SetActive(false);
            _leftBT.gameObject.SetActive(true);
        }
        _itemsBT.interactable = true;
        _catsBT.interactable = false;
        _slotContainers[1].SetActive(false);
        _slotContainers[0].SetActive(false);
        _panelCats.SetActive(true);
    }

    public void ShowItems()
    {
        _isCat = false;
        if (_catSlotsContainer[0].activeInHierarchy)
        {
            _slotContainers[0].SetActive(true);
            _slotContainers[1].SetActive(false);
            _rightBT.gameObject.SetActive(true);
            _leftBT.gameObject.SetActive(false);
        }
        else if (_catSlotsContainer[1].activeInHierarchy)
        {
            _slotContainers[0].SetActive(false);
            _slotContainers[1].SetActive(true);
            _rightBT.gameObject.SetActive(false);
            _leftBT.gameObject.SetActive(true);
        }
        _catsBT.interactable = true;
        _itemsBT.interactable = false;
        _panelCats.SetActive(false);
    }

    public void UpdateInventory(Item item)
    {
        _slots[item.itemID].GetComponent<InventoryItem>().ActivateItem();
    }
    
    public void UpdateCatInventory(Cats cat)
    {
       _catSlots[cat.catID].GetComponent<CatItem>().ActivateCat();
    }

    public void SelectItem(Item item, GameObject selectedSlot)
    {
        switch (item.itemTypeID)
        {
            case 0:
                _itemControl.ChangeFoodItem(item);
                //if(item.itemID == 6)
                //{

                //}
                foreach (GameObject slot in _foodSlots)
                {
                    if (slot.GetComponent<InventoryItem>().hasItem)
                    {
                        slot.GetComponent<Image>().sprite = _slotSprites[0];
                        slot.GetComponent<InventoryItem>().selected = false;
                    }
                }
                break;
            case 1:
                _itemControl.ChangeWaterItem(item);
                foreach (GameObject slot in _waterSlots)
                {
                    if (slot.GetComponent<InventoryItem>().hasItem)
                    {
                        slot.GetComponent<Image>().sprite = _slotSprites[0];
                        slot.GetComponent<InventoryItem>().selected = false;
                    }
                }
                break;
            case 2:
                _itemControl.ChangeRugItem(item);
                foreach (GameObject slot in _rugSlots)
                {
                    if (slot.GetComponent<InventoryItem>().hasItem)
                    {
                        slot.GetComponent<Image>().sprite = _slotSprites[0];
                        slot.GetComponent<InventoryItem>().selected = false;
                    }
                }
                break;
            case 3:
                _itemControl.ChangeHouseItem(item);
                foreach (GameObject slot in _houseSlots)
                {
                    if (slot.GetComponent<InventoryItem>().hasItem)
                    {
                        slot.GetComponent<Image>().sprite = _slotSprites[0];
                        slot.GetComponent<InventoryItem>().selected = false;
                    }
                }
                _itemControl.haveHouse = true;
                break;
            default:
                Debug.LogError("Error InventoryControl.SelectItem(item), int item.itemTypeID = " + item.itemTypeID);
                break;
        }

        selectedSlot.GetComponent<Image>().sprite = _slotSprites[1];
        selectedSlot.GetComponent<InventoryItem>().selected = true;
    }

    public void SelectCat(Cats cat, GameObject selectedSlot)
    {
        _catControl.ChangeCat(cat);
        foreach(GameObject slot in _catSlots)
        {
            //if (slot.GetComponent<CatItem>().hasCat)
            //{
                slot.GetComponent<Image>().sprite = _slotSprites[0];
                slot.GetComponent<CatItem>().selected = false;
            //}
        }

        selectedSlot.GetComponent<Image>().sprite = _slotSprites[1];
        selectedSlot.GetComponent<CatItem>().selected = true;
    }

}
