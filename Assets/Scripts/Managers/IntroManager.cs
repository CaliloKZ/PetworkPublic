using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using MEC;

public class IntroManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _introImages,
                         _introTextBoxes;

    [SerializeField]
    private Image _img;

    private bool _firstStart = false;

    private Animator _anim;
    private Animation _introAnim;

    [SerializeField]
    private float _animationSpeed;

    private int _currentScene = -1;

    private bool _clickedOnce = false;

    private void Start()
    {
        _firstStart = PlayerPrefs.GetInt("IntroFStart") == 1 ? true : false;
        if (!_firstStart)
        {
            Debug.Log("firstStart");
        }
        else
        {
            SceneManager.LoadScene("MainScene");
        }

    }


    public void StopAnim()
    {
        //_currentScene++;
        _anim.speed = 0;
    }

    public void SkipScene()
    { 
        if (!_clickedOnce)
        {
            _currentScene++;
            _clickedOnce = true;
            _introTextBoxes[_currentScene].SetActive(false);
            StartCoroutine(FadeOut(_introImages[_currentScene]));
        }
        else
        {
            _introImages[_currentScene].SetActive(false);
            _clickedOnce = false;
        }
    }

    IEnumerator FadeOut(GameObject screen)
    {
        Image img = screen.GetComponent<Image>();
        for (float i = 1; i >= 0; i -= Time.deltaTime)
        {
            img.color = new Color(1, 1, 1, i);
            yield return null;
        }
        _clickedOnce = false;
        if(_currentScene == 5)
        {
            _firstStart = true;
            PlayerPrefs.SetInt("IntroFStart", _firstStart ? 1 : 0);
            SceneManager.LoadScene("MainScene");
        }
    }

    public void Meow()
    {
        SoundManager.instance.PlaySFX("Meow");
    }

}





