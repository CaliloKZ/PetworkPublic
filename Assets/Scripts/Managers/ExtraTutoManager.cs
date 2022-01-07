using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExtraTutoManager : MonoBehaviour
{
    [SerializeField]
    private FoodBowl _bowls;
    [SerializeField]
    private SandBox _sandbox;

    [SerializeField]
    private GameObject _tutoFoodBT,
                       _tutoWaterBT,
                       _bowlsTutoBT,
                       _bowlsArrowRight,
                       _bowlsArrowLeft,
                       _tutoCleanSandboxBT,
                       _sandboxTutoBT,
                       _catTutoBT;



    [SerializeField]
    private TextMeshProUGUI _bowlsText,
                            _sandboxText;

    [SerializeField][TextArea]
    private string[] _bowlsTutoTexts,
                     _sandboxTutoTexts;

    private bool _firstClickBowl = false;

    #region bowlsTuto
    public void TutoFoodBT()
    {
        _tutoFoodBT.GetComponent<Button>().interactable = false;
        _bowls.FillFood();
        if (_firstClickBowl)
        {
            NextBowlsTuto();
        }
        else
        {
            _firstClickBowl = true;
        }
    }

    public void TutoWaterBT()
    {
        _tutoWaterBT.GetComponent<Button>().interactable = false;
        _bowls.FillWater();
        if (_firstClickBowl)
        {
            NextBowlsTuto();
        }
        else
        {
            _firstClickBowl = true;
        }
    }

    public void NextBowlsTuto()
    {
        _bowlsText.text = _bowlsTutoTexts[1];
        _bowlsArrowRight.SetActive(false);
        _bowlsArrowLeft.SetActive(false);
        _bowlsTutoBT.GetComponent<Button>().interactable = true;
    }

    public void CloseBowlsTuto()
    {
        _bowlsTutoBT.SetActive(false);
    }

    #endregion

    #region sandboxTuto
    public void SandboxTutoBT()
    {
        _tutoCleanSandboxBT.GetComponent<Button>().interactable = false;
        _sandbox.ClearBox();
        NextSandboxTuto();
    }

    public void NextSandboxTuto()
    {
        _sandboxTutoBT.GetComponent<Button>().interactable = true;
        _sandboxText.text = _sandboxTutoTexts[1];
    }

    public void CloseSandboxTuto()
    {
        _sandboxTutoBT.SetActive(false);
    }

    #endregion
    public void CloseCatTuto()
    {
        _catTutoBT.SetActive(false);
    }
}
