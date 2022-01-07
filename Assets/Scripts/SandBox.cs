using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SandBox : MonoBehaviour
{

    [SerializeField][Tooltip("Seconds")]
    private float _timeToFillMax;
    public float _timeToFill { get; private set; }
    [SerializeField][Tooltip("Seconds")]
    private float _timeUntilHurtsMax;
    public float _timeUntilHurts { get; private set; }

    private SpriteRenderer _spr;
    [SerializeField]
    private Sprite[] _sandBoxSprites; //0 - vazia, 1 - cheia

    [SerializeField]
    private GameObject _cleanBT;

    [SerializeField]
    private int _loveToAdd,
                _healthToAdd,
                _loveToRemove,
                _healthToRemove,
                _pointsToAdd;
    public bool _isFull { get; private set; }

    private void Awake()
    {
        _spr = GetComponent<SpriteRenderer>();
        _timeToFill = _timeToFillMax;
        _timeUntilHurts = _timeUntilHurtsMax;
    }

    private void Start()
    {
        if (GameManager.instance.firstStart)
        {
            _isFull = false;
            _timeToFill = 1;
            _timeUntilHurts = _timeUntilHurtsMax;
        }
    }
    private void FixedUpdate()
    {
        if (!_isFull)
        {
            _timeToFill -= Time.fixedDeltaTime;
            if(_timeToFill <= 0)
            {
                Debug.Log("Full");
                _timeUntilHurts = _timeUntilHurtsMax;
                _spr.sprite = _sandBoxSprites[1];
                _cleanBT.GetComponent<Button>().interactable = true;
                _timeToFill = 0;
                _isFull = true;
            }
        }
        else
        {
            _timeUntilHurts -= Time.fixedDeltaTime;
            if(_timeUntilHurts <= 0)
            {
                GameManager.instance.AddRemoveLove(_loveToRemove, false);
                GameManager.instance.AddRemoveHealth(_healthToRemove, false);
                Debug.Log("SandBox is still full, Love and Health reduced");
                _timeUntilHurts = _timeUntilHurtsMax;
            }
        }
    }

    public void ClearBox()
    {
        _spr.sprite = _sandBoxSprites[0];
        GameManager.instance.AddRemoveLove(_loveToAdd, true);
        GameManager.instance.AddRemoveHealth(_healthToAdd, true);
        GameManager.instance.AddPoints(_pointsToAdd);
        _timeToFill = _timeToFillMax;
        _timeUntilHurts = _timeUntilHurtsMax;
        _isFull = false;
        _cleanBT.GetComponent<Button>().interactable = false;
        SoundManager.instance.PlaySFX("Sandbox");
    }

    public void LoadTime(float timeToFill, float timeUntilHurts, float timePassed, bool isFull)
    {
        float timePassedCalc = timePassed;
        if (!isFull)
        {
            _timeToFill = timeToFill - timePassed;
            timePassedCalc -= timeToFill;
            if (_timeToFill <= 0)
            {
                _spr.sprite = _sandBoxSprites[1];
                _cleanBT.GetComponent<Button>().interactable = true;
                _isFull = true;
                _timeToFill = 0;
            }
        }

        while(timePassedCalc > 0)
        {
            _timeUntilHurts = timeUntilHurts - timePassedCalc;
            timePassedCalc -= timeUntilHurts;
            if (_timeUntilHurts <= 0)
            {
                GameManager.instance.AddRemoveLove(_loveToRemove, false);
                GameManager.instance.AddRemoveHealth(_healthToRemove, false);
                timeUntilHurts = _timeUntilHurtsMax;                   
            }
        }

        Debug.Log("SandBoxLoaded. TimeUntilhurts = " + _timeUntilHurts + ", TimeToFill = " +_timeToFill);

    }
}
