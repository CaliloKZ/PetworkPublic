using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FoodBowl : MonoBehaviour
{

    private bool _hasFood;
    private float _timeCounter;
    [SerializeField]
    private float _timeCounterMax;

    void FixedUpdate()
    {
        if (_hasFood)
        {
            _timeCounter -= Time.deltaTime;
            //uiManager show counter
        }
    }

    public void OnClick()
    {
        //se n�o tiver comida: colocar comida e setar contador.
    }

}
