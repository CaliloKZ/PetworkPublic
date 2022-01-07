using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DetectCatPlay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDropHandler, IPointerDownHandler
{
    [SerializeField]
    private CatPlayControl _control;

    private bool _inside;
    private bool _happy;
    [SerializeField]
    private float _timeToHappyMax; //tempo que demora para o gato ficar satisfeito com o carinho 

    private float _timeTHappyCount;
    
    private GameObject _cat;

    void FixedUpdate()
    {
        if (_inside)
        {
            _timeTHappyCount -= Time.deltaTime;
            if(_timeTHappyCount <= 0)
            {
                Debug.Log("Happy");
                _control.SetHappy(true);
                UIManager.instance.FinishPlay();
                _inside = false;
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("OnEnter, isDragging = " + _control._isDragging + ", happy = " + _happy + ", TimeHappyCount: " + _timeTHappyCount);

        if (_control._isDragging && !_control._isHappy)
        {
            SoundManager.instance.PlaySFX("Hand");
            SoundManager.instance.PlaySFX("Purr");
            _timeTHappyCount = _timeToHappyMax;
            _cat = UIManager.instance._activeCat;
            //Debug.Log("ActiveCat: " + _cat.name);
            _cat.transform.Find("ClosedEyes").gameObject.SetActive(true);
           // Debug.Log("CatClosedEyesFound");
            _inside = true;
            //Debug.Log("TimeHappyCount: " + _timeTHappyCount + "inside: " + _inside);
        }
            
    }

    
    public void OnPointerExit(PointerEventData eventData)
    {
        SoundManager.instance.StopSFX("Hand");
        SoundManager.instance.StopSFX("Purr");
        Debug.Log("OnExit");
        _cat = UIManager.instance._activeCat;
        _cat.transform.Find("ClosedEyes").gameObject.SetActive(false);
        _inside = false;
    }

    public void OnDrop(PointerEventData eventData)
    {
        _cat = UIManager.instance._activeCat;
        _cat.transform.Find("ClosedEyes").gameObject.SetActive(false);
        _inside = false;
        Debug.Log("OnDrop");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!_control._isHappy)
        {
            SoundManager.instance.PlaySFX("Hand");
            SoundManager.instance.PlaySFX("Purr");
            _timeTHappyCount = _timeToHappyMax;
            _cat = UIManager.instance._activeCat;
            //Debug.Log("ActiveCat: " + _cat.name);
            _cat.transform.Find("ClosedEyes").gameObject.SetActive(true);
            // Debug.Log("CatClosedEyesFound");
            _inside = true;
            //Debug.Log("TimeHappyCount: " + _timeTHappyCount + "inside: " + _inside);
        }
    }
}
