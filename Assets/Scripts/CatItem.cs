using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CatItem : MonoBehaviour
{
    [SerializeField]
    private InventoryControl _inventoryControl;

    [SerializeField]
    private Cats _cat;

    [SerializeField]
    private bool _catChecked;
    [SerializeField]
    private Button _bt;
    [SerializeField]
    private Image _img;
    [SerializeField]
    private TextMeshProUGUI _catName;

    public bool hasCat { get; private set; }

    public bool selected;

    public Cats GetCat()
    {
        return _cat;
    }

    private void Start()
    {
        if (_catChecked)
        {
            hasCat = true;
        }
        _catName.text = _cat.catName;
    }

    public void ActivateCat()
    {
        _img.gameObject.SetActive(true);
        _catName.gameObject.SetActive(true);
        _bt.interactable = true;
        hasCat = true;
    }

    public void OnClick()
    {
        if (selected)
        {
            Debug.Log("Cat already selected");
            return;
        }
        _inventoryControl.SelectCat(_cat, gameObject);
        UIManager.instance.UIButtonSound();
    }

}
