using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MEC;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField]
    private Slider _loveSlider, 
                   _healthSlider, 
                   _hungerSlider;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("Another instance of UIManager has been destroyed");
            Destroy(gameObject);
        }
    }

    public void UpdateHunger(int value)
    {
        _hungerSlider.value = value;
    }

    public void UpdateHealth(int value)
    {
        _healthSlider.value = value;
    }

    public void UpdateLove(int value)
    {
        _loveSlider.value = value;
    }
}
