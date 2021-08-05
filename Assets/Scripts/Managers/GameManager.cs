using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private UIManager _uiManager;


    private int _health;
    private int _love;
    private int _hunger;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("Another instance of GameManager has been destroyed");
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _uiManager = UIManager.instance;
    }

    public void AddRemoveHealth(int amount, bool add)
    {
        if (add)
        {
            _health += amount;
        }
        else
        {
            _health -= amount;
        }
        _uiManager.UpdateHealth(_health);
    }

    public void AddRemoveLove(int amount, bool add)
    {
        if (add)
        {
            _love += amount;
        }
        else
        {
            _love -= amount;
        }
        _uiManager.UpdateLove(_love);
    }

    public void AddRemoveHunger(int amount, bool add)
    {
        if (add)
        {
            _hunger += amount;
        }
        else
        {
            _hunger -= amount;
        }
        _uiManager.UpdateHunger(_hunger);
    }
}
