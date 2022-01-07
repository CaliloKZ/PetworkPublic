using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatsControl : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _cat,
                           _boxCat,
                           _houseCat,
                           _catClosedEyes,
                           _boxCatClosedEyes,
                           _houseCatClosedEyes;

    private Cats _selectedCat;

    public void ChangeCat(Cats cat)
    {
        _selectedCat = cat;
        GameManager.instance.selectedCat = _selectedCat;
        DataStorage.instance.selectedCat = _selectedCat;
        _cat.sprite = _selectedCat.spr;
        _catClosedEyes.sprite = _selectedCat.closedEyesSpr;
        _boxCat.sprite = _selectedCat.boxSprite;
        _boxCatClosedEyes.sprite = _selectedCat.closedEyesSpr;
        _houseCat.sprite = _selectedCat.houseSprite;
        _houseCatClosedEyes.sprite = _selectedCat.closedEyesSpr;
    }
}
