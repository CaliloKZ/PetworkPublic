using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CatPlayControl : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private GameObject[] _objPrefabs;//0 = hand, 1 = brush, 2 = feather
    private GameObject _selectedObj;
    private int _selectedObjIndex;
    public bool _isDragging { get; private set; }

    public bool _isHappy { get; private set;}


    public void SelectObj(int index)
    {
        _selectedObjIndex = index;
        SetHappy(false);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _selectedObj = Instantiate(_objPrefabs[_selectedObjIndex], transform.position, Quaternion.identity);
        _selectedObj.transform.localScale = Vector3.one;
        _selectedObj.transform.SetParent(UIManager.instance.gameObject.transform);
        _isDragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _selectedObj.transform.position = Input.mousePosition;

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Destroy(_selectedObj);
        _isDragging = false;
    }

    public void OnClose()
    {
        SoundManager.instance.StopSFX("Hand");
        SoundManager.instance.StopSFX("Purr");
        Destroy(_selectedObj);
        _isDragging = false;
    }

    public void SetHappy(bool happy)
    {
        _isHappy = happy;
    }
}
