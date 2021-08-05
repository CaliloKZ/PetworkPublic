using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandBox : MonoBehaviour
{

    [SerializeField][Tooltip("Seconds")]
    private float _timeToFillMax;
    private float _timeToFill;
    [SerializeField][Tooltip("Seconds")]
    private float _timeUntilHurtsMax;
    private float _timeUntilHurts;

    private SpriteRenderer _spr;
    [SerializeField]
    private Sprite[] _sandBoxSprites; //0 - vazia, 1 - cheia

    private bool _isFull;

    private void Awake()
    {
        _spr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        if (GameManager.instance.firstStart)
        {
            _timeToFill = _timeToFillMax;
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
                _isFull = true;
            }
        }
        else
        {
            _timeUntilHurts -= Time.fixedDeltaTime;
            if(_timeUntilHurts <= 0)
            {
                Debug.Log("SandBox is still full, Love and Health reduced");
                _timeUntilHurts = _timeUntilHurtsMax;
            }
        }
    }

}
